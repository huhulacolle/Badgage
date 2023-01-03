import { IsSignedInGuard } from './guards/is-signed-in.guard';
import { RegisterComponent } from './components/register/register.component';
import { API_BASE_URL, AuthBadgageClient, ProjectBadgageClient, TaskBadgageClient, TeamBadgageClient, UserBadgageClient } from './client/badgageClient';
import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, CanActivate } from '@angular/router';
import { AppComponent } from './app.component';
import { ApiUrlService, apiUrlServiceFactory } from './services/api-url.service'
import { TokenInterceptorService } from './services/token-interceptor.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialsModule } from 'src/material.module';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { ProjetsComponent } from './components/projets/projets.component';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FlatpickrModule } from 'angularx-flatpickr';
import { TicketsUserComponent } from './components/tickets-user/tickets-user.component';
import { ModalCreateProjectComponent } from './modals/modal-create-project/modal-create-project.component';
import { TeamsComponent } from './components/teams/teams.component';
import { ModalCreateTeamComponent } from './modals/modal-create-team/modal-create-team.component';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { ModalModifyTeamComponent } from './modals/modal-modify-team/modal-modify-team.component';
import { ModalCreateTaskComponent } from './modals/modal-create-task/modal-create-task.component';
import { ModalJoinTaskComponent } from './modals/modal-join-task/modal-join-task.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { ModalViewProjectComponent } from './modals/modal-view-project/modal-view-project.component';
import { ModalDeleteProjectComponent } from './modals/modal-delete-project/modal-delete-project.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    RegisterComponent,
    ProjetsComponent,
    TicketsUserComponent,
    TeamsComponent,
    ModalCreateProjectComponent,
    ModalCreateTeamComponent,
    ModalModifyTeamComponent,
    ModalCreateTaskComponent,
    ModalJoinTaskComponent,
    ModalViewProjectComponent,
    ModalDeleteProjectComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialsModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '',   redirectTo: '/register', pathMatch: 'full' },
      { path: 'register', component: RegisterComponent },
      { path: 'home', component: TeamsComponent, canActivate: [IsSignedInGuard] },
      { path: 'tickets', component: TicketsUserComponent, canActivate: [IsSignedInGuard] },
      { path: 'projets', component: ProjetsComponent, canActivate: [IsSignedInGuard] },
    ]),
    CalendarModule.forRoot({ provide: DateAdapter, useFactory: adapterFactory }),
    FlatpickrModule.forRoot(),
    NgbModule,
  ],
  providers: [
    AuthBadgageClient,
    ProjectBadgageClient,
    TeamBadgageClient,
    UserBadgageClient,
    TaskBadgageClient,
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
