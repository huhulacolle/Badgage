import { IsSignedInGuard } from './guards/is-signed-in.guard';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { API_BASE_URL, AuthBadgageClient } from './client/badgageClient';
import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, CanActivate } from '@angular/router';

import { AppComponent } from './app.component';
import { ApiUrlService, apiUrlServiceFactory } from './services/api-url.service';
import { TestloginComponent } from './components/testlogin/testlogin.component';
import { TokenInterceptorService } from './services/token-interceptor.service';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    TestloginComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '',   redirectTo: '/login', pathMatch: 'full' },
      // { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'test', component: TestloginComponent, canActivate: [ IsSignedInGuard ]}
    ])
  ],
  providers: [
    AuthBadgageClient,
    {
			provide: APP_INITIALIZER,
			useFactory: apiUrlServiceFactory,
			deps: [ApiUrlService],
			multi: true,
    },
    { provide: API_BASE_URL,
      useFactory: (service: ApiUrlService) => service.apiUrl.url,
      deps: [ApiUrlService]
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
