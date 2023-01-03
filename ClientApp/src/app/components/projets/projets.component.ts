import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ProjectModel, TaskModel, TeamModel } from 'src/app/client/badgageClient';
import { ModalCreateProjectComponent } from 'src/app/modals/modal-create-project/modal-create-project.component';
import { ModalViewProjectComponent } from 'src/app/modals/modal-view-project/modal-view-project.component';
import { ModalDeleteProjectComponent } from 'src/app/modals/modal-delete-project/modal-delete-project.component';


import { ProjectService } from 'src/app/services/project.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { StorageService } from 'src/app/services/storage.service';
import { TeamService } from 'src/app/services/team.service';
import { ModalCreateTaskComponent } from 'src/app/modals/modal-create-task/modal-create-task.component';

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

  tasks!: TaskModel[];
  showTasks: boolean = false;


  getProjetsByUser(): void {
      this.projectService.getProjectByUser().then((result) => {
        this.projects = result;
        console.log(this.projects);
      })
  }

  projects!: ProjectModel[];
  teams!: TeamModel[];

  seeTask(task: TaskModel): void {
    const dialogRef = this.dialog.open(ModalCreateTaskComponent, {data: task});
    dialogRef.afterClosed();
  }



  createProject(): void {
    const projet = new ProjectModel();
    projet.byUser = new JwtHelperService().decodeToken(this.storageService.getUser()).id;
    const dialogRef = this.dialog.open(ModalCreateProjectComponent, { data: { projet } });
    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
      if (result != null)
        this.projectService.createProject(result)
          .then(() => {
            this._snackBar.open('Projet créé');
            this.getProjetsByUser();
          }).catch(() => {
            this._snackBar.open('Erreur lors de la création du projet');
          })
    })
  }

  deleteProjectModal(project: ProjectModel): void {
    const dialogRef = this.dialog.open(ModalDeleteProjectComponent, { data: project });
    dialogRef.afterClosed().subscribe((result) => {
      this.projectService.deleteProject(project.idProject as number)
        .then(() => {
          this._snackBar.open('Projet suprimmée', '', {duration: 3000});
          this.getProjetsByUser();
        }).catch(() => {
          this._snackBar.open('Erreur lors de la suppression de le projet', '', {duration: 3000});
        });
    })
  }

  // modifyProjectNameModal(project: ProjectModel): void {
  //   const dialogRef = this.dialog.open(ModalDeleteProjectComponent, { data: Team });
  //   dialogRef.afterClosed().subscribe((result) => {
  //     this.projectService.(result.idTeam as number, result.nom)
  //       .then(() => {
  //         this._snackBar.open('Equipe renommée', '', {duration: 3000});
  //         this.getTeamByUser();
  //       }).catch(() => {
  //         this._snackBar.open('Erreur lors du renommage de l\'équipe', '', {duration: 3000});
  //       });
  //   })
  // }
  openProject(project: ProjectModel): void {
    const dialogRef = this.dialog.open(ModalViewProjectComponent, { data:  project  });

 
  }


}