import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Project } from 'src/app/client/badgageClient';

@Component({
  selector: 'app-modal-create-project',
  templateUrl: './modal-create-project.component.html',
  styleUrls: ['./modal-create-project.component.css']
})
export class ModalCreateProjectComponent {
  constructor(
    public dialogRef : MatDialogRef<ModalCreateProjectComponent>, @Inject(MAT_DIALOG_DATA) public data : Project
  ) { }

  onCancelClick(): void {
    this.dialogRef.close();
  }
}
