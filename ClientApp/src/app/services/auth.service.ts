import { AuthBadgageClient, UserModel, UserLogin } from './../client/badgageClient';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private authClient: AuthBadgageClient
  ) { }

  login(user: UserLogin): Promise<string> {
    return lastValueFrom(this.authClient.login(user));
  }

  register(user: UserModel): Promise<any> {
    return lastValueFrom(this.authClient.register(user));
  }


}
