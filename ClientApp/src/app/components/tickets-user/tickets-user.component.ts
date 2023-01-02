import { Component, ChangeDetectionStrategy, ViewChild, TemplateRef, ChangeDetectorRef,} from '@angular/core';
import {startOfDay, endOfDay, subDays, addDays, endOfMonth, isSameDay, isSameMonth, addHours,} from 'date-fns';
import { Subject } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CalendarEvent, CalendarEventAction, CalendarEventTimesChangedEvent, CalendarView, } from 'angular-calendar';
import { EventColor } from 'calendar-utils';
import { TicketService } from 'src/app/services/ticket.service';
import { ProjectModel, TaskModel } from 'src/app/client/badgageClient';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ProjectService } from 'src/app/services/project.service';
import { MatDialog } from '@angular/material/dialog';
import { ModalCreateTaskComponent } from 'src/app/modals/modal-create-task/modal-create-task.component';
import { ModalJoinTaskComponent } from 'src/app/modals/modal-join-task/modal-join-task.component';
import { ModalAddSessionComponent } from 'src/app/modals/modal-add-session/modal-add-session.component';
import { SessionService } from 'src/app/services/session.service';

const colors: Record<string, EventColor> = {
  red: {
    primary: '#ad2121',
    secondary: '#FAE3E3',
  },
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
    private projectService: ProjectService, private sessionService: SessionService) {
      ref.detach();
      setInterval(() => {
        this.numberOfTicks++;
        this.ref.detectChanges();
      }, 100);
    }

  ngOnInit(): void {
    this.getProjects();
  }

  tasks!: TaskModel[];
  projects!: ProjectModel[];
  showTasks: boolean = false;


  getTasks(): void {
    console.log(this.projects);
    for(let i = 0; this.projects.length > i;i++){
      console.log(this.projects[i].idProject as number);
      this.ticketService.getTaskByProject(this.projects[i].idProject as number).then((result) => {
        this.tasks = result;
        console.log(this.tasks);
        this.showTasks= true;
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
      this._snackBar.open("Tâche supprimée avec succès");
      this.getProjects();
    }).catch((error) => {
      this._snackBar.open(error);
    })
  }

  createTask(): void {
    const dialogRef = this.dialog.open(ModalCreateTaskComponent);
    dialogRef.updateSize('lg','lg');
    dialogRef.afterClosed().subscribe(result => {
        result.dateCreation = new Date();
        result.idTache = undefined;
        result.dateFin = null;
        this.ticketService.setTask(result)
          .then(() => {
            this._snackBar.open("Tâche créée avec succès");
            this.getProjects();
          }).catch((error) => {
            this._snackBar.open(error);
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
            this._snackBar.open("Tâche attribuée avec succès");
            this.getProjects();
          }).catch((error) => {
            this._snackBar.open(error);
          })
        }
    })
  }

  addSession(task: TaskModel): void {
    const dialogRef = this.dialog.open(ModalAddSessionComponent, {data : task});
    dialogRef.afterClosed().subscribe(result => {
        if(result){
          this.sessionService.setSession(result)
          .then(() => {
            this._snackBar.open("Tâche attribuée avec succès");
            this.getProjects();
          }).catch((error) => {
            this._snackBar.open(error);
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

  actions: CalendarEventAction[] = [
    {
      label: '<i class="fas fa-fw fa-pencil-alt"></i>',
      a11yLabel: 'Edit',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.handleEvent('Edited', event);
      },
    },
    {
      label: '<i class="fas fa-fw fa-trash-alt"></i>',
      a11yLabel: 'Delete',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.events = this.events.filter((iEvent) => iEvent !== event);
        this.handleEvent('Deleted', event);
      },
    },
  ];

  refresh = new Subject<void>();

  events: CalendarEvent[] = [
    {
      start: subDays(startOfDay(new Date()), 1),
      end: addDays(new Date(), 1),
      title: 'A 3 day event',
      color: { ...colors.red },
      actions: this.actions,
      allDay: true,
      resizable: {
        beforeStart: true,
        afterEnd: true,
      },
      draggable: true,
    },
    {
      start: startOfDay(new Date()),
      title: 'An event with no end date',
      color: { ...colors.yellow },
      actions: this.actions,
    },
    {
      start: subDays(endOfMonth(new Date()), 3),
      end: addDays(endOfMonth(new Date()), 3),
      title: 'A long event that spans 2 months',
      color: { ...colors.blue },
      allDay: true,
    },
    {
      start: addHours(startOfDay(new Date()), 2),
      end: addHours(new Date(), 2),
      title: 'A draggable and resizable event',
      color: { ...colors.yellow },
      actions: this.actions,
      resizable: {
        beforeStart: true,
        afterEnd: true,
      },
      draggable: true,
    },
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