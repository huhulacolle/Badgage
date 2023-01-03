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
  ) {
    this.dataToSend = { complete: false, session: new SessionInput() };
  }

  ngOnInit(): void {
    this.session = new SessionInput();
    this.session.idTask = this.data.idTask as number;
    this.projetService.getProjectByUser().then((result) => {
      this.projets = result;
      this.projets.map(p => {
        if (this.data.idProjet == p.idProject)
          this.nomProjetSeeing = p.projectName;
      })
    }).catch((error) => {
    })
  }

  dataToSend: { complete: boolean, session: SessionInput };
  nomProjetSeeing!: string;
  projets!: ProjectModel[];
  dateValide: boolean = false;
  hoursDateDebut!: string;
  hoursDateFin!: string;
  session!: SessionInput;

  onCancelClick(): void {
    this.dialogRef.close();
  }

  checkDate(): void {
    if (this.session.dateFin != undefined && this.session.dateDebut != undefined && this.hoursDateDebut != undefined && this.hoursDateFin != undefined) {
      const hoursDateDebut = this.hoursDateDebut.split(':');
      const hoursDateFin = this.hoursDateFin.split(':');
      this.dateValide = (((this.session.dateDebut.getTime() == this.session.dateFin.getTime()) && (+hoursDateDebut[0] + 1 > +hoursDateFin[0] + 1)) ||
        ((this.session.dateDebut.getTime() == this.session.dateFin.getTime()) && (+hoursDateDebut[0] + 1 == +hoursDateFin[0] + 1) && (+hoursDateDebut[1] > +hoursDateFin[1]))
        || (this.session.dateDebut.getTime() > this.session.dateFin.getTime())
      );
      const timeDebut = this.hoursDateDebut.split(':');
      const timeFin = this.hoursDateFin.split(':');
      this.session.dateDebut = new Date(this.session.dateDebut.getFullYear(),
        this.session.dateDebut.getMonth(), this.session.dateDebut.getDate(), +timeDebut[0] + 1, +timeDebut[1]);
      this.session.dateFin = new Date(this.session.dateFin.getFullYear(),
        this.session.dateFin.getMonth(), this.session.dateFin.getDate(), +timeFin[0] + 1, +timeFin[1]);
      this.dataToSend.session = this.session;
    }
  }
}
