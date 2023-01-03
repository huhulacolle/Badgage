import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TaskModel, TeamModel, UserModel, UserOnTeamModel } from 'src/app/client/badgageClient';
import { ProjectService } from 'src/app/services/project.service';
import { TeamService } from 'src/app/services/team.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-modal-join-task',
  templateUrl: './modal-join-task.component.html',
  styleUrls: ['./modal-join-task.component.css']
})
export class ModalJoinTaskComponent {
  constructor(
    public dialogRef: MatDialogRef<ModalJoinTaskComponent>, private userService: UserService,
    @Inject(MAT_DIALOG_DATA) public data: TaskModel, private teamService: TeamService
    , private projectService: ProjectService) { }

  ngOnInit(): void {
    this.getUserTeamNumber();
  }

  team!: number;
  usersOnTeam!: UserModel[];
  idUser!: number;

  getUsersOnTeam(): void {
    this.userService.getTeamUser(this.team).then((result) => {
      this.usersOnTeam = result;
    }).catch((error) => {
    });
  }

  getUserTeamNumber(): void {
    this.projectService.getProjectByUser().then((result) => {
      for (let i = 0; i < result.length; i++) {
        if (result[i].idProject === this.data.idProjet)
          this.team = result[i].idTeam;
      }
      this.getUsersOnTeam();
    }).catch((error) => {
    });
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }
}
