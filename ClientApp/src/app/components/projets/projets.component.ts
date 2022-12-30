import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Project } from 'src/app/client/badgageClient';
import { ModalCreateProjectComponent } from 'src/app/modals/modal-create-project/modal-create-project.component';
import { ProjectService } from 'src/app/services/project.service';

@Component({
  selector: 'app-projets',
  templateUrl: './projets.component.html',
  styleUrls: ['./projets.component.css']
})
export class ProjetsComponent {

  constructor(public dialog: MatDialog, private projectService : ProjectService, private _snackBar: MatSnackBar
    ) { }

    openDialog(): void {
      const dialogRef = this.dialog.open(ModalCreateProjectComponent, {data: {Project} } );
      dialogRef.afterClosed().subscribe(result => {
        const project = new Project();
        project.projectName = result
        if(result != null)
          this.projectService.createProject(project)
          .then( () =>{
            this._snackBar.open('Projet créé');
        }).catch( () => {
          this._snackBar.open('Erreur lors de la création du projet')
        })
      })
    }

}