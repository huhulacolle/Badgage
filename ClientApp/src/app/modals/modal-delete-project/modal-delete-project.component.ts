import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProjectModel } from 'src/app/client/badgageClient';

@Component({
  selector: 'app-modal-delete-project',
  templateUrl: './modal-delete-project.component.html',
  styleUrls: ['./modal-delete-project.component.css']
})
export class ModalDeleteProjectComponent {
  constructor(
     public dialogRef : MatDialogRef<ModalDeleteProjectComponent>, @Inject(MAT_DIALOG_DATA) public data : ProjectModel
  ) {
   }


onCancelClick(): void {
  this.dialogRef.close();
}

}