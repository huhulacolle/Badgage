import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiUrlService {

  public apiUrl = "";

  load(): void {
    this.apiUrl = environment.api;
  }
}

export function apiUrlServiceFactory(apiUrlService: ApiUrlService) {
	return (): void => apiUrlService.load();
}
