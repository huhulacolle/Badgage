import { Component, Inject, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProjectModel, SessionInput, TaskModel } from 'src/app/client/badgageClient';
import { ProjectService } from 'src/app/services/project.service';

@Component({
  selector: 'app-modal-add-session',
  templateUrl: './modal-add-session.component.html',
  styleUrls: ['./modal-add-session.component.css']
})
export class ModalAddSessionComponent {
  @ViewChild('picker') picker: any;

  constructor(public dialogRef: MatDialogRef<ModalAddSessionComponent>, private projetService: ProjectService,
     @Inject(MAT_DIALOG_DATA) public data: TaskModel
  ) { }

  ngOnInit(): void {
    this.session = new SessionInput();
    this.session.idTask = this.data.idTache as number;
    this.projetService.getProjectByUser().then((result) => {
      this.projets = result;
        this.projets.map(p => {
          if (this.data.idProjet == p.idProject)
            this.nomProjetSeeing = p.projectName;
        })
    }).catch((error) => {
      console.log(error);
    })
  }

  nomProjetSeeing!: string;
  projets!: ProjectModel[];
  error!: boolean;
  hoursDateDebut!: Date;
  hoursDateFin!: Date;
  session!: SessionInput;

  onCancelClick(): void {
    this.dialogRef.close();
  }

  checkDate(): boolean {
    this.session.dateDebut.setHours(this.hoursDateDebut.getHours());
    this.session.dateDebut.setMinutes(this.hoursDateDebut.getMinutes());
    if(this.session.dateFin != undefined){
      this.session.dateFin.setMinutes(this.hoursDateFin.getMinutes());
      this.session.dateFin.setHours(this.hoursDateFin.getHours());
      return (this.session.dateDebut.getTime() < this.session.dateFin.getTime());
    }
    return false;
  }
}
