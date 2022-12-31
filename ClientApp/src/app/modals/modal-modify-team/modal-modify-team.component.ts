import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserModel, UserOnTeamModel } from 'src/app/client/badgageClient';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-modal-modify-team',
  templateUrl: './modal-modify-team.component.html',
  styleUrls: ['./modal-modify-team.component.css']
})
export class ModalModifyTeamComponent {
  constructor(
    public dialogRef : MatDialogRef<ModalModifyTeamComponent>, @Inject(MAT_DIALOG_DATA) public data : UserOnTeamModel, private userService : UserService,
  ) { }

  ngOnInit() : void {
    this.userService.getUsers().then((result) => { this.users = result });
    console.log(this.data);
  }

  users!: UserModel[];

  onCancelClick(): void {
    this.dialogRef.close();
  }
}
