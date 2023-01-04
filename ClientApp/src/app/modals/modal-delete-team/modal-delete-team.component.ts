import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TeamModel } from 'src/app/client/badgageClient';

@Component({
  selector: 'app-modal-delete-team',
  templateUrl: './modal-delete-team.component.html',
  styleUrls: ['./modal-delete-team.component.css']
})
export class ModalDeleteTeamComponent {
  constructor(
    public dialogRef: MatDialogRef<ModalDeleteTeamComponent>, @Inject(MAT_DIALOG_DATA) public data: TeamModel
  ) { }

}
