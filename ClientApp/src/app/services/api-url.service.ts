import { lastValueFrom } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Settings } from '../interfaces/settings';

@Injectable({
  providedIn: 'root'
})
export class ApiUrlService {

  constructor(
    private http: HttpClient
  ) { }

  public apiUrl!: Settings;

  async load(): Promise<void> {
    this.apiUrl = await lastValueFrom(this.http.get<Settings>("assets/settings.json"));
    console.log(this.apiUrl);
  }
}

export function apiUrlServiceFactory(apiUrlService: ApiUrlService) {
	return (): Promise<void> => apiUrlService.load();
}
