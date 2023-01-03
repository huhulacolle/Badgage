import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { SessionBadgageClient, SessionInput, SessionModel } from '../client/badgageClient';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  constructor(private sessionService: SessionBadgageClient) { }

  getSessionsByUser(idUser: number): Promise<SessionModel[]> {
    return lastValueFrom(this.sessionService.getSessionByIdUser(idUser));
  }

  getSessionsByIdTask(idTask: number): Promise<SessionModel[]> {
    return lastValueFrom(this.sessionService.getSessionByIdTask(idTask));
  }

  setSession(session: SessionInput): Promise<any> {
    return lastValueFrom(this.sessionService.setSession(session));
  }
}

