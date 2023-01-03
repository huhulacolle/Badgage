import { JwtHelperService } from '@auth0/angular-jwt';
import { Component, ChangeDetectionStrategy, ViewChild, TemplateRef, ChangeDetectorRef, } from '@angular/core';
import { startOfDay, endOfDay, isSameDay, isSameMonth, addHours, } from 'date-fns';
import { concat, Subject } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CalendarEvent, CalendarEventTimesChangedEvent, CalendarView, } from 'angular-calendar';
import { EventColor } from 'calendar-utils';
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

@Component({
  selector: 'app-tickets-user',
  changeDetection: ChangeDetectionStrategy.OnPush,
  styleUrls: ['./tickets-user.component.css'],
  templateUrl: './tickets-user.component.html',
  animations: [
    bounceInLeftOnEnterAnimation({ anchor: 'enterL', delay: 100, animateChildren: 'together' }),
    bounceOutRightOnLeaveAnimation({ anchor: 'leaveR', delay: 100, animateChildren: 'together' }),
  ]
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
  tasks!: TaskModel[];
  userOnTask!: UserOnTaskModelWithName[];
  projects!: ProjectModel[];
  nbTickets!: number;
  sessions: SessionModel[] = [];

  getSessionsByTasks(): void {
    this.events = [];
    for (let i = 0; this.tasks.length > i; i++) {
      this.sessionService.getSessionsByIdTask(this.tasks[i].idTask as number).then((result) => {
        for (let j = 0; result.length > j; j++) {
          this.sessions.push(result[j]);
          const color = this.tasks[i].dateFin == undefined ? colors.yellow : colors.blue;
          const title: string = this.tasks[i].nomDeTache + " - " + result[j].dateDebut.toLocaleString('fr-FR', { timeZone: 'UTC' }) + " - " + ((result[j].dateDebut.getDate == result[j].dateFin?.getDate) ? result[j].dateFin?.toLocaleTimeString('fr-FR', { timeZone: 'UTC' }) : result[j].dateFin?.toLocaleString('fr-FR', { timeZone: 'UTC' }));
          this.events.push({ start: result[j].dateDebut, end: result[j].dateFin, title: title, color: color });
        }
      })
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

  StopSession(): void {
    this.dateFin = new Date;
    const sessionInput = new SessionInput();
    sessionInput.idTask = this.idTaskTimer.idTask as number;
    sessionInput.dateDebut = this.dateDebut;
    sessionInput.dateFin = this.dateFin;
    this.sessionService.setSession(sessionInput)
      .then(
        () => {
          if (this.checkTimer) {
            this.UpdateTimeEndTask(this.idTaskTimer.idTask as number, this.dateFin)
          }
        }
      )
    this.basicTimer.stop();
    this.EtatSession = true;
  }

  UpdateTimeEndTask(idTask: number, DateFin: Date): void {
    this.ticketService.endTask(idTask, DateFin)
      .then(() => {
        this.getTasks();
      })
  }

  getTasks(): void {
    for (let i = 0; this.projects.length > i; i++) {
      this.ticketService.getTaskByProject(this.projects[i].idProject as number).then((result) => {
        this.tasks = result;
        for (let j = 0; this.tasks.length > j; j++) {
          this.ticketService.getListUserByIdTask(this.tasks[j].idTask as number).then((result) => {
            this.userOnTask = result;
          });
        }
        this.nbTickets = this.tasks.length;
        this.getSessionsByTasks();
        this.getTaskNotValid(result);
      }).catch();
    }
  }

  checkNotJoined(idTask: number | undefined): boolean {
    for (let i = 0; this.userOnTask.length > i; i++) {
      if (this.userOnTask[i].idTask as number == idTask as number) {
        return true;
      }
    }
    return false;
  }

  checkJoinedByUSer(idTask: number | undefined): boolean {
    for (let i = 0; this.userOnTask.length > i; i++) {
      if (this.userOnTask[i].idTask as number == idTask as number && this.userOnTask[i].idUser == new JwtHelperService().decodeToken(this.storageService.getUser()).id) {
        return true;
      }
    }
    return false;
  }

  getTaskNotValid(result: TaskModel[]): void {
    result.forEach(r => {
      if (r.dateFin == undefined) {
        this.notValidTask.push(r);
      }
    });
  }

  getProjects(): void {
    this.projectService.getProjectByUser().then((result) => {
      this.projects = result;
      this.getTasks();
    });
  }

  deleteTask(task: TaskModel): void {
    const dialogRef = this.dialog.open(ModalDeleteTaskComponent, { data: task });
    dialogRef.afterClosed().subscribe((result) => {
      const idTache = result.idTask as number;
      this.ticketService.deleteTask(idTache).then(() => {
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
        .then(() => {
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
          .then(() => {
            this._snackBar.open("Tâche attribuée avec succès", '', { duration: 3000 });
            this.getProjects();
            this.getTasks();
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
          .then(() => {
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
