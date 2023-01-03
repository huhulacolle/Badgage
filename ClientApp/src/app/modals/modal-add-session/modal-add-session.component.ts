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
  ) {this.dataToSend = {complete: false,session: new SessionInput()};
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
      console.log(error);
    })
  }

  ngAfterContentInit(): void {
    console.log(this.dataToSend.complete);
  }

  dataToSend: {complete: boolean, session: SessionInput};
  nomProjetSeeing!: string;
  projets!: ProjectModel[];
  error!: boolean;
  hoursDateDebut!: string;
  hoursDateFin!: string;
  session!: SessionInput;

  onCancelClick(): void {
    this.dialogRef.close();
  }

  sendSession(): any {
    if (this.session.dateFin != undefined && this.session.dateDebut != undefined) {
      const timeDebut = this.hoursDateDebut.split(':');
      const timeFin = this.hoursDateFin.split(':');
      console.log(this.hoursDateFin, this.hoursDateDebut);
    this.session.dateDebut = new Date(this.session.dateDebut.getFullYear(),
      this.session.dateDebut.getMonth(), this.session.dateDebut.getDate(),+timeDebut[0] + 1,+timeDebut[1]);
      this.session.dateFin = new Date(this.session.dateFin.getFullYear(),
        this.session.dateFin.getMonth(), this.session.dateFin.getDate(),+timeFin[0] + 1,+timeFin[1]);
    }
    this.dataToSend.session = this.session;
    console.log(this.dataToSend);
    return ;
  }

  checkDate(): boolean {
    if (this.session.dateFin != undefined) {
      return ((this.session.dateDebut.getTime() <= this.session.dateFin.getTime()) && (this.session.dateDebut.getTime() <= this.session.dateFin.getTime()));
    }
    return false;
  }
}
