import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ProjectModel, TeamModel } from 'src/app/client/badgageClient';
import { ModalCreateProjectComponent } from 'src/app/modals/modal-create-project/modal-create-project.component';
import { ProjectService } from 'src/app/services/project.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { StorageService } from 'src/app/services/storage.service';
import { TeamService } from 'src/app/services/team.service';

@Component({
  selector: 'app-projets',
  templateUrl: './projets.component.html',
  styleUrls: ['./projets.component.css']
})
export class ProjetsComponent {

  constructor(public dialog: MatDialog, private projectService: ProjectService, private _snackBar: MatSnackBar, private storageService: StorageService, private teamService : TeamService
  ) { }

  ngAfterContentInit() : void {
    this.getProjetsByUser();
  }

  getProjetsByUser(): void {
      this.projectService.getProjectByUser().then((result) => {
        this.projects = result;
        console.log(this.projects);
      })
  }

  projects!: ProjectModel[];
  teams!: TeamModel[];

  createProject(): void {
    const projet = new ProjectModel();
    projet.byUser = new JwtHelperService().decodeToken(this.storageService.getUser()).id;
    const dialogRef = this.dialog.open(ModalCreateProjectComponent, { data: { projet } });
    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
      if (result != null)
        this.projectService.createProject(result)
          .then(() => {
            this._snackBar.open('Projet créé', '', {duration: 3000});
            this.getProjetsByUser();
          }).catch(() => {
            this._snackBar.open('Erreur lors de la création du projet', '', {duration: 3000});
          })
    })
  }

}