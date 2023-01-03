import { JwtHelperService } from '@auth0/angular-jwt';
import { Component, ChangeDetectionStrategy, ViewChild, TemplateRef, ChangeDetectorRef,} from '@angular/core';
import {startOfDay, endOfDay, subDays, addDays, endOfMonth, isSameDay, isSameMonth, addHours,} from 'date-fns';
import { Subject } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CalendarEvent, CalendarEventAction, CalendarEventTimesChangedEvent, CalendarView, } from 'angular-calendar';
import { EventColor } from 'calendar-utils';
import { TicketService } from 'src/app/services/ticket.service';
import { ProjectModel, SessionInput, SessionModel, TaskModel } from 'src/app/client/badgageClient';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ProjectService } from 'src/app/services/project.service';
import { MatDialog } from '@angular/material/dialog';
import { ModalCreateTaskComponent } from 'src/app/modals/modal-create-task/modal-create-task.component';
import { ModalJoinTaskComponent } from 'src/app/modals/modal-join-task/modal-join-task.component';
import { ModalAddSessionComponent } from 'src/app/modals/modal-add-session/modal-add-session.component';
import { SessionService } from 'src/app/services/session.service';
import { StorageService } from 'src/app/services/storage.service';

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
})
export class TicketsUserComponent {
  @ViewChild('modalContent')
  modalContent!: TemplateRef<any>;
  numberOfTicks = 0;
  constructor(private modal: NgbModal, private ticketService: TicketService,
    private _snackBar: MatSnackBar, private dialog: MatDialog,private ref: ChangeDetectorRef,
    private projectService: ProjectService, private sessionService: SessionService, private storageService: StorageService) {
      ref.detach();
      setInterval(() => {
        this.numberOfTicks++;
        this.ref.detectChanges();
      }, 500);
    }

  ngOnInit(): void {
    this.getProjects();
  }

  idTask!: number;
  tasks!: TaskModel[];
  projects!: ProjectModel[];
  nbTickets!: number;
  sessions: SessionModel[] = [];

  getSessionsByTasks(): void {
    this.events= [];
    for(let i = 0; this.tasks.length > i; i++){
      this.sessionService.getSessionsByIdTask(this.tasks[i].idTask as number).then((result) => {
        for(let j = 0; result.length > j;j++)
        {
          this.sessions.push(result[j]);
          const color = this.tasks[i].dateFin == undefined?colors.yellow:colors.blue;
          this.events.push({ start : result[j].dateDebut, end: result[j].dateFin, title: this.tasks[i].nomDeTache, color: color });
        }
      })
    }
    this.refresh;
  }

  getTasks(): void {
    console.log(this.projects);
    for(let i = 0; this.projects.length > i;i++){
      console.log(this.projects[i].idProject as number);
      this.ticketService.getTaskByProject(this.projects[i].idProject as number).then((result) => {
        this.tasks = result;
        this.nbTickets = this.tasks.length;
        this.getSessionsByTasks();
      }).catch();
    }
  }

  getProjects(): void {
    this.projectService.getProjectByUser().then((result)=> {
      console.log("je suis là")
      this.projects = result;
      this.getTasks();
    });
  }

  deleteTask(idTask: number): void {
    this.ticketService.deleteTask(idTask).then(() => {
      this._snackBar.open("Tâche supprimée avec succès", '', {duration: 3000});
      this.getTasks();
    }).catch((error) => {
      this._snackBar.open(error, '', {duration: 3000});
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
            this._snackBar.open("Tâche créée avec succès", '', {duration: 3000});
            this.getTasks();
          }).catch((error) => {
            this._snackBar.open(error, '', {duration: 3000});
          })
    })
  }

  seeTask(task: TaskModel): void {
    const dialogRef = this.dialog.open(ModalCreateTaskComponent, {data: task});
    dialogRef.afterClosed();
  }

  joinTask(task: TaskModel): void {
    const dialogRef = this.dialog.open(ModalJoinTaskComponent, {data : task});
    dialogRef.afterClosed().subscribe(result => {
        if(result){
          this.ticketService.joinTask(result)
          .then(() => {
            this._snackBar.open("Tâche attribuée avec succès", '', {duration: 3000});
            this.getProjects();
          }).catch((error) => {
            this._snackBar.open(error);
          })
        }
    })
  }

  addSession(task: TaskModel): void {
    console.log(task);
    const dialogRef = this.dialog.open(ModalAddSessionComponent, {data : task});
    dialogRef.afterClosed().subscribe(result => {
        if(result){
          console.log(result);
          const session = new SessionInput();
          session.dateDebut = result.session.dateDebut;
          session.dateFin = result.session.dateFin;
          session.idTask = result.session.idTask;
          console.log(session);
          this.sessionService.setSession(session)
          .then(() => {
            this._snackBar.open("Tâche attribuée avec succès");
            this.ticketService.endTask(task.idTask as number,result.session.dateFin as Date).then(() => {
              this._snackBar.open("Tâche finie avec succès");
              this.getSessionsByTasks();
            }).catch((error) => {
              this._snackBar.open(error);
            });
            this.getProjects();
          }).catch((error) => {
            this._snackBar.open(error, '', {duration: 3000});
          })
        }
    })
  }

  view: CalendarView = CalendarView.Month;

  CalendarView = CalendarView;

  viewDate: Date = new Date();

  modalData!: {
    action: string;
    event: CalendarEvent;
  };

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

  eventTimesChanged({
    event,
    newStart,
    newEnd,
  }: CalendarEventTimesChangedEvent): void {
    this.events = this.events.map((iEvent) => {
      if (iEvent === event) {
        return {
          ...event,
          start: newStart,
          end: newEnd,
        };
      }
      return iEvent;
    });
    this.handleEvent('Dropped or resized', event);
  }

  handleEvent(action: string, event: CalendarEvent): void {
    this.modalData = { event, action };
    this.modal.open(this.modalContent, { size: 'lg' });
  }

  addEvent(): void {
    this.events = [
      ...this.events,
      {
        title: 'New event',
        start: startOfDay(new Date()),
        end: endOfDay(new Date()),
        color: colors.red,
        draggable: true,
        resizable: {
          beforeStart: true,
          afterEnd: true,
        },
      },
    ];
  }

  deleteEvent(eventToDelete: CalendarEvent) {
    this.events = this.events.filter((event) => event !== eventToDelete);
  }

  setView(view: CalendarView) {
    this.view = view;
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }
}
