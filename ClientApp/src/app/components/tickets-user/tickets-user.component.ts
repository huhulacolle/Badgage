import { JwtHelperService } from '@auth0/angular-jwt';
import { Component, ChangeDetectionStrategy, ViewChild, TemplateRef, ChangeDetectorRef, } from '@angular/core';
import { isSameDay, isSameMonth, } from 'date-fns';
import { Subject } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CalendarDateFormatter, CalendarEvent, CalendarView, } from 'angular-calendar';
import { DAYS_OF_WEEK, EventColor } from 'calendar-utils';
import { TicketService } from 'src/app/services/ticket.service';
import { ProjectModel, SessionInput, SessionModel, TaskModel, UserOnTaskModelWithName, } from 'src/app/client/badgageClient';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ProjectService } from 'src/app/services/project.service';
import { MatDialog } from '@angular/material/dialog';
import { ModalCreateTaskComponent } from 'src/app/modals/modal-create-task/modal-create-task.component';
import { ModalJoinTaskComponent } from 'src/app/modals/modal-join-task/modal-join-task.component';
import { ModalAddSessionComponent } from 'src/app/modals/modal-add-session/modal-add-session.component';
import { SessionService } from 'src/app/services/session.service';
import { StorageService } from 'src/app/services/storage.service';
import { ModalDeleteTaskComponent } from 'src/app/modals/modal-delete-task/modal-delete-task.component';
import { bounceInLeftOnEnterAnimation, bounceOutRightOnLeaveAnimation } from 'angular-animations';
import { CustomDateFormatter } from './customized-calendar';

const colors: Record<string, EventColor> = {
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF',
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA',
  },
};


class TaskOutput {

  constructor(private task: TaskModel) {
    this.idTask = this.task.idTask;
    this.idProjet = this.task.idProjet;
    this.nomDeTache = this.task.nomDeTache;
    this.description = this.task.description;
    this.dateFin = this.task.dateFin;
    this.dateCreation = this.task.dateCreation;
    this.etat = 0;
  }

  nomUser!: string;
  etat!: number;
  idTask?: number | undefined;
  idProjet!: number;
  nomDeTache!: string;
  description?: string | undefined;
  dateFin?: Date | undefined;
  dateCreation!: Date;


  getTaskModel(): TaskModel {
    const taskModel = new TaskModel();
    taskModel.idTask = this.idTask;
    taskModel.nomDeTache = this.nomDeTache;
    taskModel.dateCreation = this.dateCreation;
    taskModel.dateFin = this.dateFin;
    taskModel.idProjet = this.idProjet;
    taskModel.description = this.description;
    return taskModel;
  }
}

@Component({
  selector: 'app-tickets-user',
  changeDetection: ChangeDetectionStrategy.OnPush,
  styleUrls: ['./tickets-user.component.css'],
  templateUrl: './tickets-user.component.html',
  animations: [
    bounceInLeftOnEnterAnimation({ anchor: 'enterL', delay: 100, animateChildren: 'together' }),
    bounceOutRightOnLeaveAnimation({ anchor: 'leaveR', delay: 100, animateChildren: 'together' }),
  ],
  providers: [
    {
      provide: CalendarDateFormatter,
      useClass: CustomDateFormatter,
    },
  ],
})


export class TicketsUserComponent {
  @ViewChild('modalContent')
  modalContent!: TemplateRef<any>;
  @ViewChild('basicTimer')
  basicTimer!: any;
  numberOfTicks = 0;

  constructor(private modal: NgbModal, private ticketService: TicketService,
    private _snackBar: MatSnackBar, private dialog: MatDialog, private ref: ChangeDetectorRef,
    private projectService: ProjectService, private sessionService: SessionService, private storageService: StorageService) {
    ref.detach();
    setInterval(() => {
      this.numberOfTicks++;
      this.ref.detectChanges();
    }, 400);
  }
  ngOnInit(): void {
    this.getProjects();
  }

  idTask!: number;
  tasks!: TaskOutput[];
  userOnTask: UserOnTaskModelWithName[] = [];
  projects!: ProjectModel[];
  nbTickets!: number;
  sessions: SessionModel[] = [];

  async getSessionsByTasks(): Promise<void> {
    this.events = [];
    for (let i = 0; this.tasks.length > i; i++) {
      this.tasks[i] = this.checkNotJoined(this.tasks[i]);
      this.getTaskNotValid(this.tasks);
      const result = await this.sessionService.getSessionsByIdTask(this.tasks[i].idTask as number)
      for (let j = 0; result.length > j; j++) {
        this.sessions.push(result[j]);
        const color = this.tasks[i].dateFin == undefined ? colors.yellow : colors.blue;
        const title: string = this.tasks[i].nomDeTache + " - " + result[j].dateDebut.toLocaleString('fr-FR', { timeZone: 'UTC' }) + " - " + ((result[j].dateDebut.getDate == result[j].dateFin?.getDate) ? result[j].dateFin?.toLocaleTimeString('fr-FR', { timeZone: 'UTC' }) : result[j].dateFin?.toLocaleString('fr-FR', { timeZone: 'UTC' }));
        this.events.push({ start: result[j].dateDebut, end: result[j].dateFin, title: title, color: color });

      }
    }

    this.refresh;
  }
  // variable pour chrono
  idTaskTimer!: TaskModel;
  EtatSession = true;
  dateDebut!: Date;
  dateFin!: Date;
  checkTimer = false;
  notValidTask: TaskModel[] = [];

  NewSession(): void {
    if (this.idTaskTimer.dateFin != undefined) {
      this._snackBar.open('Cette tâche est déjà terminé', '', { duration: 3000 });
    }
    else {
      this.EtatSession = false;
      this.dateDebut = new Date;
      this.basicTimer.start(0);
    }
  }

  async StopSession(): Promise<void> {
    this.dateFin = new Date;
    const sessionInput = new SessionInput();
    sessionInput.idTask = this.idTaskTimer.idTask as number;
    sessionInput.dateDebut = this.dateDebut;
    sessionInput.dateFin = this.dateFin;
    this.sessionService.setSession(sessionInput)
      .then(
        async () => {
          if (this.checkTimer) {
            this.UpdateTimeEndTask(this.idTaskTimer.idTask as number, this.dateFin)
          }
        }
      )
    this.getTasks();
    this.basicTimer.stop();
    this.EtatSession = true;
  }

  async UpdateTimeEndTask(idTask: number, DateFin: Date): Promise<void> {
    await this.ticketService.endTask(idTask, DateFin)
  }

  async getTasks(): Promise<void> {
    this.tasks = [];
    this.userOnTask = [];
    for (let i = 0; this.projects.length > i; i++) {
      const result = await this.ticketService.getTaskByProject(this.projects[i].idProject as number);
      console.log(result);
      for (let a = 0; result.length > a; a++) {
        this.tasks.push(new TaskOutput(result[a]));
        const id = this.tasks[a].idTask as number;
        console.log(id);
        const result1 = await this.ticketService.getListUserByIdTask(id)
        console.log(result1);
        for (let j = 0; result.length > j; j++) { this.userOnTask.push(result1[j]); }
      }
      this.nbTickets = this.tasks.length;
      this.getSessionsByTasks();
    }
  }

  checkNotJoined(task: TaskOutput): TaskOutput {
    let id = new JwtHelperService().decodeToken(this.storageService.getUser()).id;
    console.log(task);
    for (let i = 0; this.userOnTask.length > i; i++) {
      if (this.userOnTask[i] != undefined && task.idTask != undefined && task.idTask == this.userOnTask[i].idTask) {
        task.nomUser = this.userOnTask[i].email;
        task.etat = 1;
        if (this.userOnTask[i].idUser == id) {
          task.etat = 2
          return task;
        }
      }
    }
    return task;
  }

  getTaskNotValid(result: TaskOutput[]): void {
    this.notValidTask = [];
    result.forEach(r => {
      if (r.dateFin == undefined && r.etat == 2) {
        this.notValidTask.push(r.getTaskModel());
      }
    });
  }

  async getProjects(): Promise<void> {
    const result = await this.projectService.getProjectByUser()
    this.projects = result;
    this.getTasks();
  }

  deleteTask(task: TaskModel): void {
    const dialogRef = this.dialog.open(ModalDeleteTaskComponent, { data: task });
    dialogRef.afterClosed().subscribe((result) => {
      const idTache = result.idTask as number;
      this.ticketService.deleteTask(idTache).then(async () => {
        this._snackBar.open("Tâche supprimée avec succès", '', { duration: 3000 });
        this.getTasks();
      }).catch((error) => {
        this._snackBar.open(error, '', { duration: 3000 });
      });
    })
  }

  createTask(): void {
    const dialogRef = this.dialog.open(ModalCreateTaskComponent);
    dialogRef.afterClosed().subscribe(result => {
      result.dateCreation = new Date();
      result.idTache = undefined;
      result.dateFin = null;
      this.ticketService.setTask(result)
        .then(async () => {
          this._snackBar.open("Tâche créée avec succès", '', { duration: 3000 });
          this.getTasks();
        }).catch((error) => {
          this._snackBar.open(error, '', { duration: 3000 });
        })
    })
  }

  seeTask(task: TaskModel): void {
    const dialogRef = this.dialog.open(ModalCreateTaskComponent, { data: task });
    dialogRef.afterClosed();
  }

  joinTask(task: TaskModel): void {
    const dialogRef = this.dialog.open(ModalJoinTaskComponent, { data: task });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.ticketService.joinTask(task.idTask as number, result)
          .then(async () => {
            this._snackBar.open("Tâche attribuée avec succès", '', { duration: 3000 });
            this.getProjects();
          }).catch((error) => {
            this._snackBar.open(error);
          })
      }
    })
  }

  addSession(task: TaskModel): void {
    const dialogRef = this.dialog.open(ModalAddSessionComponent, { data: task });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const session = new SessionInput();
        session.dateDebut = result.session.dateDebut;
        session.dateFin = result.session.dateFin;
        session.idTask = result.session.idTask;
        this.getSessionsByTasks();
        this.sessionService.setSession(session)
          .then(async () => {
            this._snackBar.open("Tâche attribuée avec succès");
            if (result.complete) {
              this.ticketService.endTask(task.idTask as number, result.session.dateFin as Date).then(() => {
                this._snackBar.open("Tâche finie avec succès");
              }).catch((error) => {
                this._snackBar.open(error);
              });
            }
            this.getProjects();
          }).catch((error) => {
            this._snackBar.open(error, '', { duration: 3000 });
          })
      }
    })
  }

  view: CalendarView = CalendarView.Month;

  CalendarView = CalendarView;

  viewDate: Date = new Date();

  refresh = new Subject<void>();
  
  locale: string = 'fr';

  weekStartsOn: number = DAYS_OF_WEEK.MONDAY;

  weekendDays: number[] = [DAYS_OF_WEEK.FRIDAY, DAYS_OF_WEEK.SATURDAY];

  events: CalendarEvent[] = [
  ];

  activeDayIsOpen: boolean = true;

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
      this.viewDate = date;
    }
  }

  setView(view: CalendarView) {
    this.view = view;
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }
}
