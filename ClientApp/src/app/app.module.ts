import { API_BASE_URL, BadgageClient } from './client/badgageClient';
import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ApiUrlService, apiUrlServiceFactory } from './services/api-url.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ])
  ],
  providers: [
    BadgageClient,
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
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
