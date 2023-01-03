import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { ProjectModel, ProjectBadgageClient } from '../client/badgageClient';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private projectClient : ProjectBadgageClient) { }

  createProject(project: ProjectModel): Promise<any> {
    return lastValueFrom(this.projectClient.createProject(project));
  }

  getProjectByTeam(idTeam: number): Promise<ProjectModel[]> {
    return lastValueFrom(this.projectClient.getProjectByTeam(idTeam));
  }

  getProjectByUser(): Promise<ProjectModel[]> {
    return lastValueFrom(this.projectClient.getProjectByUser());
  }

  deleteProject(idPoject : number) : Promise<any> {
    return lastValueFrom(this.projectClient.deleteProject(idPoject));
  }
}
