import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { TaskBadgageClient, TaskModel, UserOnTaskModelWithName } from '../client/badgageClient';

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

  getTaskByProject(idProject: number): Promise<TaskModel[]> {
    return lastValueFrom(this.ticketService.getTaskFromProject(idProject));
  }

  joinTask(idTask: number, idUser: number): Promise<any> {
    return lastValueFrom(this.ticketService.joinTask(idTask, idUser));
  }

  setTask(taskModel: TaskModel): Promise<any> {
    return lastValueFrom(this.ticketService.setTask(taskModel));
  }

  deleteTask(idTask: number): Promise<any> {
    return lastValueFrom(this.ticketService.deleteTask(idTask));
  }

  endTask(idTask: number, dateFin: Date): Promise<void> {
    return lastValueFrom(this.ticketService.updateTimeEndTask(idTask, dateFin));
  }

  listTask(idTask: number): Promise<UserOnTaskModelWithName[]> {
    return lastValueFrom(this.ticketService.getListTaskByIdTask(idTask));
  }

}
