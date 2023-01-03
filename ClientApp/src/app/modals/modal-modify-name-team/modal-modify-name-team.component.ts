import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TeamModel } from 'src/app/client/badgageClient';

@Component({
  selector: 'app-modal-modify-name-team',
  templateUrl: './modal-modify-name-team.component.html',
  styleUrls: ['./modal-modify-name-team.component.css']
})
export class ModalModifyNameTeamComponent {
  constructor(
    public dialogRef : MatDialogRef<ModalModifyNameTeamComponent>, @Inject(MAT_DIALOG_DATA) public data : TeamModel
  ) { }

  onCancelClick(): void {
    this.dialogRef.close();
  }
}
