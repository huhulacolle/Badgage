using Badgage.Infrastructure;
using Badgage.Services;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerDocument();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors();

string ConnectionString = builder.Configuration.GetConnectionString("MySQL");

builder.Services.AddSingleton(new DefaultSqlConnectionFactory(ConnectionString));

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddScoped(provider =>
{
    var context = provider.GetRequiredService<IHttpContextAccessor>();
    return context.HttpContext.User;
});

// Configure le middleware pour le token JWT
string JwtKey = builder.Configuration["JwtKey"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey))
    };
});

// Configure la migration 
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(config => config
        .AddMySql5()
        .WithGlobalConnectionString(ConnectionString)
        .ScanIn(Assembly.GetExecutingAssembly()).For.All())
    .AddLogging(lb => lb.AddFluentMigratorConsole()
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

// Lance la migration
using var scope = app.Services.CreateScope();
var migrator = scope.ServiceProvider.GetService<IMigrationRunner>()!;
migrator.MigrateUp();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
