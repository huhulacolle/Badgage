import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProjectModel, TeamModel } from 'src/app/client/badgageClient';
import { TeamService } from 'src/app/services/team.service';

@Component({
  selector: 'app-modal-create-project',
  templateUrl: './modal-create-project.component.html',
  styleUrls: ['./modal-create-project.component.css']
})
export class ModalCreateProjectComponent {
  constructor(
    public dialogRef : MatDialogRef<ModalCreateProjectComponent>, @Inject(MAT_DIALOG_DATA) public data : ProjectModel, private teamService : TeamService,
  ) { }

  ngOnInit() : void {
    this.teamService.getTeamsByUser(this.data.idTeam).then((result) => { this.teams = result });
    console.log(this.data);
  }
    
    teams!: TeamModel[];

  showData(): void {
    console.log(this.data);
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }
}
