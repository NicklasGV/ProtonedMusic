import { Component, OnInit, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventModel, resetEvent } from 'src/app/Models/EventModel';
import { EventService } from 'src/app/Services/event.service';
import { FormsModule } from '@angular/forms';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import { duration } from 'moment';

@Component({
  selector: 'app-event-panel',
  standalone: true,
  imports: [CommonModule, FormsModule, MatDatepickerModule, MatNativeDateModule, MatFormFieldModule, MatSnackBarModule],
  templateUrl: './event-panel.component.html',
  styles: []
})
export class EventPanelComponent implements OnInit {
  message: string = "";
  events: EventModel[] = [];
  event: EventModel = resetEvent();

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {duration: 8000});
  }
  
  constructor(private eventService: EventService, private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.eventService.getAllEvents().subscribe(x => this.events = x);
  }
  
editProduct(event: EventModel): void {
  Object.assign(this.event, event);
}
  
  deleteProduct(event: EventModel): void {
    if (confirm("Er du sikker på at du vil slette dette produkt?")) {
      this.eventService.deleteEvent(event.id).subscribe(x => {
        this.events = this.events.filter(x => x.id != event.id);
      });
    }
  }

  cancel(): void {
    this.event = resetEvent();
  }

  save(): void {
    this.message = "";
    if (this.event.id == 0) {
      //create
      this.eventService.createEvent(this.event)
      .subscribe({
        next: (x) => {
          this.events.push(x);
          this.event = resetEvent();
          this.openSnackBar("Event created", "Close");
        },
        error: (err) => {
          console.log(err);
          this.message = Object.values(err.error.errors).join(", ");
          this.openSnackBar(this.message, "Close");
        }
      });
    } else {
      //update
      this.eventService.updateEvent(this.event.id, this.event)
      .subscribe({
        error: (err) => {
          this.message = Object.values(err.error.errors).join(", ");
          this.openSnackBar(this.message, "Close");
        },
        complete: () => {
          this.eventService.getAllEvents().subscribe(x => this.events = x);
          this.event = resetEvent();
          this.openSnackBar("Event updated", "Close");
        }
      });
    }
    this.event = resetEvent();
  }
}
