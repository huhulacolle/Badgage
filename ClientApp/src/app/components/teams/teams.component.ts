import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ModalCreateTeamComponent } from 'src/app/modals/modal-create-team/modal-create-team.component';
import { ModalModifyTeamComponent } from 'src/app/modals/modal-modify-team/modal-modify-team.component';
import { StorageService } from 'src/app/services/storage.service';
import { TeamService } from 'src/app/services/team.service';
import { TeamModel, UserOnTeamModel } from '../../client/badgageClient';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.css']
})
export class TeamsComponent {

  constructor(public dialog: MatDialog, private teamService: TeamService, private _snackBar: MatSnackBar, private storageService: StorageService,
  ) { }

  ngOnInit(): void {
    this.teamService.getTeamsByUser().then((result) => { this.teams = result });
    console.log(this.teams);
  }

  teams!: TeamModel[];

  createTeamModal(): void {
    const dialogRef = this.dialog.open(ModalCreateTeamComponent, { data: { TeamModel } });
    dialogRef.afterClosed().subscribe((result) => {
      const team = new TeamModel();
      team.nom = result;
      team.byUser = new JwtHelperService().decodeToken(this.storageService.getUser().toString()).id;
      this.teamService.createTeam(team)
        .then(() => {
          this._snackBar.open('Equipe créée');
        }).catch(() => {
          this._snackBar.open('Erreur lors de la création de l\'équipe');
        });
    })
  }

  addMembersModal(idTeam: number| undefined): void {
    const userOnTeam = new UserOnTeamModel();
    console.log("SA RACE");
    userOnTeam.idTeam = idTeam as number;
    const dialogRef = this.dialog.open(ModalModifyTeamComponent, { data: { userOnTeam } });
    dialogRef.afterClosed().subscribe((result) => {
      userOnTeam.idUser = result;
      this.teamService.joinTeam(userOnTeam).then(() => {
        this._snackBar.open("Utilisateur ajouté avec succès dans l'équipe");
      }).catch(() => {
        this._snackBar.open("Erreur lors de l'ajout de l'utilisateur dans l'équipe")
      })
    })
  }

}
