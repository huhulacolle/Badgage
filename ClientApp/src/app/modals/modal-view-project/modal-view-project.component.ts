import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TeamService } from 'src/app/services/team.service';
import { UserService } from 'src/app/services/user.service';
import { ProjectService } from 'src/app/services/project.service';
import { ProjectModel, TaskModel, TeamModel, UserModel, UserOnTeamModel } from 'src/app/client/badgageClient';


@Component({
  selector: 'app-modal-view-project',
  templateUrl: './modal-view-project.component.html',
  styleUrls: ['./modal-view-project.component.css']
})
export class ModalViewProjectComponent {
  constructor(
    public dialogRef: MatDialogRef<ModalViewProjectComponent>, private userService: UserService,
    @Inject(MAT_DIALOG_DATA) public data: ProjectModel, private teamService: TeamService
    , private projectService: ProjectService) { }

  ngOnInit(): void {
    this.userService.getTeamUser(this.data.idTeam as number).then((result) => { this.usersOnTeam = result });
    this.teamService.getTeamsByUser().then((result) => { this.teamModel = result });
    // this.getUserTeamNumber();

  }
  team!: number;
  projectModel!: ProjectModel[];
  usersOnTeam!: UserModel[];
  teamModel!: TeamModel[];
  idUser!: number;




  getTeamOnProject(): void {
    this.userService.getTeamUser(this.team).then((result) => {
      this.usersOnTeam = result;
    }).catch((error) => {
    });

  }

  getUserOnProject(): void {
    this.projectService.getProjectByTeam(this.team).then((result) => {
      this.projectModel = result;
      this.getUserOnProject();
    }).catch((error) => {
    });
  }

}
