import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
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


@Component({
  selector: 'app-family-schedule',
  standalone: true,
  imports: [CommonModule, MatNativeDateModule, MatDatepickerModule, FormsModule, CalendarModule],
  templateUrl: './family-schedule.component.html',
  styleUrl: './family-schedule.component.css'
})
export class FamilyScheduleComponent implements OnInit {
  selected: Date = new Date();
  message: string = '';
  content: CalendarModel[] = [];
  calendarContent: CalendarModel = resetCalendar();
  currentUser: User = resetUser();
  user: User = resetUser();
  showEventBool: boolean = false;

  constructor(
    private snackBar: SnackBarService,
    private dialog: MatDialog,
    private calendarService: CalendarService,
  ) {}

  ngOnInit() {
    this.calendarService.getAllContent().subscribe((x) => (this.content = x));
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

  save() {
    this.message = '';
    if (this.calendarContent.id == 0) {
      //create
      this.calendarContent.date = this.selected;
      this.calendarService.createEvent(this.calendarContent).subscribe({
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
      this.calendarContent.date = this.selected;
      this.calendarService.updateEvent(this.calendarContent.id, this.calendarContent).subscribe({
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
