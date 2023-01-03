import { UserOnTaskModelWithName } from './../../client/badgageClient';
import { TicketService } from 'src/app/services/ticket.service';
import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProjectModel, TaskModel } from 'src/app/client/badgageClient';
import { ProjectService } from 'src/app/services/project.service';

@Component({
  selector: 'app-modal-create-team',
  templateUrl: './modal-create-task.component.html',
  styleUrls: ['./modal-create-task.component.css']
})
export class ModalCreateTaskComponent {
  constructor(
    public dialogRef: MatDialogRef<ModalCreateTaskComponent>, private projetService: ProjectService, private ticketService: TicketService, @Inject(MAT_DIALOG_DATA) public data: TaskModel
  ) {}

  ngOnInit(): void {
    this.projetService.getProjectByUser().then((result) => {
      this.projets = result;
      if (this.data != null) {
        for(let i = 0; i < this.projets.length; i++)
          if (this.data.idProjet = this.projets[i].idProject as number)
              this.nomProjetSeeing = this.projets[i].projectName;
        }
    }).catch((error) => {
      console.log(error);
    })
    if (this.data != null) {
      this.seeing = true;
    }
    else
      this.task = new TaskModel();
      this.ListTask();
  }

  task!: TaskModel;
  listTasks!: UserOnTaskModelWithName[];
  nomProjetSeeing!: string;
  seeing: boolean = false;
  projets!: ProjectModel[];
  error!: string | undefined;

  onCancelClick(): void {
    this.dialogRef.close();
  }

  ListTask(): void {
    this.ticketService.listTask(this.data.idTask as number)
    .then(
      data => {
        this.listTasks = data;
      }
    )
  }

  checkDate(date: Date | undefined): void {
    date = date as Date;
    if (date.getTime() < new Date().getTime())
      this.error = "La date de fin ne peut être avant celle d'aujourd'hui";
    else
      this.error = undefined;
  }
}
