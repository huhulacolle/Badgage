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

  getProjectByUser(idUser: number): Promise<any> {
    return lastValueFrom(this.projectClient.getProjectsByUser());
  }
}