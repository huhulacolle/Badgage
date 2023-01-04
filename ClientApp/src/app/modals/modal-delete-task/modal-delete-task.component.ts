import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TaskModel } from 'src/app/client/badgageClient';

@Component({
  selector: 'app-modal-delete-task',
  templateUrl: './modal-delete-task.component.html',
  styleUrls: ['./modal-delete-task.component.css']
})
export class ModalDeleteTaskComponent {
  constructor(
    public dialogRef: MatDialogRef<ModalDeleteTaskComponent>, @Inject(MAT_DIALOG_DATA) public data: TaskModel
  ) { }

}
