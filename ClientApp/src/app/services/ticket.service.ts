import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { TaskBadgageClient, TaskModel } from '../client/badgageClient';

@Injectable({
  providedIn: 'root'
})
export class TicketService {

  constructor(private ticketService: TaskBadgageClient) { }

  getTaskByUser(): Promise<TaskModel[]> {
    return lastValueFrom(this.ticketService.getTasksByUser());
  }

  getTasksByIdUser(idUser: number): Promise<TaskModel[]> {
    return lastValueFrom(this.ticketService.getTasksByIdUser(idUser));
  }

  joinTask(idUser: number): Promise<any> {
    return lastValueFrom(this.ticketService.joinTask(idUser));
  }

  setTask(taskModel: TaskModel): Promise<any> {
    return lastValueFrom(this.ticketService.setTask(taskModel));
  }

  deleteTask(idTask: number): Promise<any> {
    return lastValueFrom(this.ticketService.deleteTask(idTask));
  }
}
