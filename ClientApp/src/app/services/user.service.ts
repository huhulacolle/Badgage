import { Injectable } from '@angular/core';
import { User } from 'oidc-client';
import { lastValueFrom } from 'rxjs';
import { UserBadgageClient, UserModel } from '../client/badgageClient';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private userClient: UserBadgageClient
  ) { }

  getUserById(mail: string): Promise<UserModel> {
    return lastValueFrom(this.userClient.getUser(mail));
  }

  getUser(email: string): Promise<UserModel> {
    return lastValueFrom(this.userClient.getUser(email));
  }

  getUsers(): Promise<any> {
    return lastValueFrom(this.userClient.getUsers());
  }

  getTeamUser(idTeam: number): Promise<UserModel[]> {
    return lastValueFrom(this.userClient.getUsersOnTeam(idTeam));
  }
  // j'ai crée se service juste en exemple mais normalement ici il faut faire les fonction qui feront le lien avec le ClientSwagger,
  // et après vous utilisez ces fonction dans vos composents
}
