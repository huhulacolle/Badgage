import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { TeamBadgageClient, TeamModel, UserOnTeamModel } from '../client/badgageClient';

@Injectable({
  providedIn: 'root'
})
export class TeamService {

  constructor(private teamClient : TeamBadgageClient) { }

  createTeam(team: TeamModel): Promise<void> {
    return lastValueFrom(this.teamClient.setTeam(team));
  }

  getTeamsByUser(): Promise<TeamModel[]> {
    return lastValueFrom(this.teamClient.getTeamByUser());
  }

  joinTeam(newUser: UserOnTeamModel): Promise<void> {
    return lastValueFrom(this.teamClient.joinTeam(newUser));
  }

}
