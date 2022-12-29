import { IsSignedInGuard } from './guards/is-signed-in.guard';
import { RegisterComponent } from './components/register/register.component';
import { API_BASE_URL, AuthBadgageClient } from './client/badgageClient';
import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, CanActivate } from '@angular/router';
import { AppComponent } from './app.component';
import { ApiUrlService, apiUrlServiceFactory } from './services/api-url.service'
import { TokenInterceptorService } from './services/token-interceptor.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './components/home/home.component';
import { MaterialsModule } from 'src/material.module';
import { FormsModule} from '@angular/forms';
import { ProjetsComponent } from './components/home/projets/projets.component';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FlatpickrModule } from 'angularx-flatpickr';
import { TicketsUserComponent } from './components/tickets-user/tickets-user.component';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    HomeComponent,
    ProjetsComponent,
    TicketsUserComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialsModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '',   redirectTo: '/register', pathMatch: 'full' },
      // { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'register', component: RegisterComponent },
      { path: 'home', component: HomeComponent, canActivate: [IsSignedInGuard] }
    ]),
    CalendarModule.forRoot({ provide: DateAdapter, useFactory: adapterFactory }),
    FlatpickrModule.forRoot(),
    NgbModule,
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
      useFactory: (service: ApiUrlService) => service.apiUrl,
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
