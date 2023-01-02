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
    public dialogRef : MatDialogRef<ModalCreateTaskComponent>, private projetService: ProjectService, @Inject(MAT_DIALOG_DATA) public data: TaskModel
  ) { }

  ngOnInit(): void {
      if(this.data != undefined)
        this.task = this.data;
      else
        this.task = new TaskModel();
      this.projetService.getProjectByUser().then((result) => {
        this.projets = result;
        console.log(this.projets);
      }).catch((error) => {
        console.log(error);
      })
    }

    task!: TaskModel;
    projets!: ProjectModel[];
    error!: string | undefined;

  onCancelClick(): void {
    this.dialogRef.close();
  }

  checkDate(date: Date | undefined): void {
    date = date as Date;
    if(date.getTime()  < new Date().getTime())
      this.error = "La date de fin ne peut être avant celle d'aujourd'hui";
    else
      this.error = undefined;
  }
}
