import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TeamModel } from 'src/app/client/badgageClient';

@Component({
  selector: 'app-modal-create-team',
  templateUrl: './modal-create-team.component.html',
  styleUrls: ['./modal-create-team.component.css']
})
export class ModalCreateTeamComponent {
  constructor(
    public dialogRef : MatDialogRef<ModalCreateTeamComponent>, @Inject(MAT_DIALOG_DATA) public data : TeamModel
  ) { }

  onCancelClick(): void {
    this.dialogRef.close();
  }
}
