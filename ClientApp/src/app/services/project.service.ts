import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Project, ProjectBadgageClient } from '../client/badgageClient';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private projectClient : ProjectBadgageClient) { }

  createProject(project: Project): Promise<any> {
    return lastValueFrom(this.projectClient.createProject(project));
  }
}
