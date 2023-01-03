import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProjectModel } from 'src/app/client/badgageClient';
import { ProjectService } from 'src/app/services/project.service';


@Component({
  selector: 'app-modal-modify-project',
  templateUrl: './modal-modify-project.component.html',
  styleUrls: ['./modal-modify-project.component.css']
})
export class ModalModifyProjectComponent {
  constructor(
    public dialogRef: MatDialogRef<ModalModifyProjectComponent>, @Inject(MAT_DIALOG_DATA) public data: ProjectModel, private projectService: ProjectService,
  ) { }

  projects!: ProjectModel[];

  onCancelClick(): void {
    this.dialogRef.close();
  }

}


