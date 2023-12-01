import { Component, OnInit, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventModel, resetEvent } from 'src/app/Models/EventModel';
import { EventService } from 'src/app/Services/event.service';
import { FormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from 'src/app/Shared/dialog/dialog.component';

@Component({
  selector: 'app-event-panel',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
  ],
  templateUrl: './event-panel.component.html',
  styles: [],
})
export class EventPanelComponent implements OnInit {
  message: string = '';
  events: EventModel[] = [];
  event: EventModel = resetEvent();
  selectedFile: File | undefined;
  formData = new FormData();

  constructor(
    private eventService: EventService,
    private snackBar: SnackBarService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.eventService.getAllEvents().subscribe((x) => (this.events = x));
  }

  editProduct(event: EventModel): void {
    Object.assign(this.event, event);
  }

  onPictureFileSelected(event: any): void {
    const file = event.target.files[0];
    this.event.pictureFile = file;
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }
  
  uploadImage() {
    if (this.selectedFile) {
      const formData = new FormData();
      formData.append('file', this.selectedFile);
  
      this.eventService.uploadProductPicture(this.event.id, formData).subscribe(
        (event: EventModel) => {
          this.eventService.getAllEvents().subscribe(x => this.events = x);
            this.event = resetEvent();
            this.snackBar.openSnackBar("Event Pic Updated", '', 'success');
        },
        (error) => {
          this.message = Object.values(error.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message, '', 'error');
        }
      );
    }
  }

  deleteProduct(event: EventModel): void {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        title: 'Delete Event',
        message: 'Are you sure you want to delete this event?',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.eventService.deleteEvent(event.id).subscribe((x) => {
          this.events = this.events.filter((x) => x.id != event.id);
        });
        this.snackBar.openSnackBar('Deletion successful.', '', 'success');
      } else {
        // User canceled the operation
        this.snackBar.openSnackBar('Deletion canceled.', '', 'warning');
      }
    });
  }

  cancel(): void {
    this.event = resetEvent();
    this.snackBar.openSnackBar('Event canceled.', '', 'info');
  }

  save(): void {
    this.message = '';
    if (this.event.id == 0) {
      //create
      this.eventService.createEvent(this.event).subscribe({
        next: (x) => {
          this.events.push(x);
          this.event = resetEvent();
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
      this.eventService.updateEvent(this.event.id, this.event).subscribe({
        error: (err) => {
          this.message = Object.values(err.error.errors).join(', ');
          this.snackBar.openSnackBar(this.message, '', 'error');
        },
        complete: () => {
          this.eventService.getAllEvents().subscribe((x) => (this.events = x));
          this.event = resetEvent();
          this.snackBar.openSnackBar('Event updated', '', 'success');
        },
      });
    }
    this.event = resetEvent();
  }
}
