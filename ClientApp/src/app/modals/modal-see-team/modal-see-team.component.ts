import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TeamModel, UserModel, UserOnTeamModel } from 'src/app/client/badgageClient';
import { UserService } from 'src/app/services/user.service';
import { ModalModifyTeamComponent } from '../modal-modify-team/modal-modify-team.component';

@Component({
  selector: 'app-modal-see-team',
  templateUrl: './modal-see-team.component.html',
  styleUrls: ['./modal-see-team.component.css']
})
export class ModalSeeTeamComponent {
  constructor(
    public dialogRef: MatDialogRef<ModalModifyTeamComponent>, @Inject(MAT_DIALOG_DATA) public data: TeamModel, private userService: UserService
  ) { }

  async ngOnInit(): Promise<void> {
    this.getTeamMembers();
  }

  users: UserModel[] = [];

  async getTeamMembers(): Promise<void> {
    this.users = await this.userService.getTeamUser(this.data.idTeam as number);
  }
}
