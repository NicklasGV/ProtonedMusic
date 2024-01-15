import { Role } from 'src/app/Models/role';
import { Component, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDialog } from '@angular/material/dialog';
import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { CalendarModel, resetCalendar } from 'src/app/Models/CalendarModel';
import { FormsModule } from '@angular/forms';
import { User, resetUser } from 'src/app/Models/UserModel';
import { CalendarService } from 'src/app/Services/calendar.service';
import { DialogComponent } from 'src/app/Shared/dialog/dialog.component';
import { CalendarModule } from 'primeng/calendar';
import { ArtistModel, resetArtist } from 'src/app/Models/ArtistModel';
import { ArtistService } from 'src/app/Services/artist.service';
import { UserService } from 'src/app/Services/user.service';
import { AuthService } from 'src/app/Services/auth.service';
import { FilterService } from 'primeng/api';
import { TableModule } from 'primeng/table';

@Component({
  selector: 'app-family-schedule',
  standalone: true,
  imports: [CommonModule, MatNativeDateModule, MatDatepickerModule, FormsModule, CalendarModule, TableModule],
  templateUrl: './family-schedule.component.html',
  styleUrl: './family-schedule.component.css'
})
export class FamilyScheduleComponent implements OnInit {
  selected: Date = new Date();
  message: string = '';
  content: CalendarModel[] = [];
  calendarContent: CalendarModel = resetCalendar();
  artist: ArtistModel = resetArtist();
  Artists: ArtistModel[] = [];
  currentUser: User = resetUser();
  user: User = resetUser();
  showEventBool: boolean = false;
  crossedOutDates: string[] = [];

  constructor(
    private snackBar: SnackBarService,
    private dialog: MatDialog,
    private calendarService: CalendarService,
    private datePipe: DatePipe,
    private artistService: ArtistService,
    private userService: UserService,
    private authService: AuthService,
    private filterService: FilterService,
  ) {}

  ngOnInit() {
    this.calendarService.getAllContent().subscribe((x) => (this.content = x));
    this.artistService.getAll().subscribe((x) => (this.Artists = x));
    this.userService.findById(this.authService.currentUserValue.id).subscribe(x => this.currentUser = x);
  }
  showEvent() {
    if (this.showEventBool == false) {
      return this.showEventBool = true;
    }
    else{
      this.calendarContent = resetCalendar();
      this.selected = new Date();
      return this.showEventBool = false;
    }
  }

  transformDate(date: any) {
    return this.datePipe.transform(date, 'dd');
  }
  transformMonth(date: any) {
    return this.datePipe.transform(date, 'M');
  }

  calendarDates(date: any, month: any): boolean {
    let isDateTrue = false;
    if (this.content)
    {
      this.content.forEach(calDate => {
        if (this.transformDate(calDate.date) == date) {
          if (this.transformMonth(calDate.date) == month) 
          {
            isDateTrue = true;
          }
        }
      });
    }
    return isDateTrue;
  }

  getBackgroundColor(date: any): string {
    return this.calendarDates(date.day, date.month + 1) ? '#2de291c9' : '';
  }

  editEvent(event: CalendarModel) {
    this.selected = event.date;
    Object.assign(this.calendarContent, event);
  }
  deleteEvent(event: CalendarModel) {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        title: 'Delete Event',
        message: 'Are you sure you want to delete this event?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.calendarService.deleteEvent(event.id).subscribe((x) => {
          this.content = this.content.filter((x) => x.id != event.id);
        });
        this.snackBar.openSnackBar('Deletion successful.', '', 'success');
      } else {
        // User canceled the operation
        this.snackBar.openSnackBar('Deletion canceled.', '', 'warning');
      }
    });
  }

  cancel() {
    this.calendarContent = resetCalendar();
    this.showEvent();
  }

  transformDateTime(date: any) {
    return this.datePipe.transform(date, 'dd-MM-yyyy HH:mm:ss');
  }

  save() {
    this.message = '';
    if (this.currentUser.role == 'Family')
      {
        this.calendarContent.artistId = this.currentUser.id;
      };
      if (this.currentUser.role == 'Admin')
      {
        this.calendarContent.artistId = this.calendarContent.artist.id;
      };
      console.log(this.calendarContent);
      if (this.calendarContent.id == 0) {
        //create
      this.calendarContent.date = this.transformDateTime(this.selected);
      this.calendarService.create(this.calendarContent).subscribe({
        next: (x) => {
          this.content.push(x);
          this.calendarContent = resetCalendar();
          this.snackBar.openSnackBar('Event created', '', 'success');
        },
        error: (err) => {
          console.log(err);
          this.message = Object.values(err.error.errors).join(', ');
          this.snackBar.openSnackBar(this.message, '', 'error');
        },
      });
    } else {
      //update
      this.calendarContent.date = this.transformDateTime(this.selected);
      this.calendarService.update(this.calendarContent.id, this.calendarContent).subscribe({
        error: (err) => {
          this.message = Object.values(err.error.errors).join(', ');
          this.snackBar.openSnackBar(this.message, '', 'error');
        },
        complete: () => {
          this.calendarService.getAllContent().subscribe((x) => (this.content = x));
          this.calendarContent = resetCalendar();
          this.snackBar.openSnackBar('Event updated', '', 'success');
        },
      });
    }
    this.calendarContent = resetCalendar();
  }
}