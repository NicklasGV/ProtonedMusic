import { Component, OnInit, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventModel, resetEvent } from 'src/app/Models/EventModel';
import { EventService } from 'src/app/Services/event.service';
import { FormsModule } from '@angular/forms';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {MatFormFieldModule} from '@angular/material/form-field';
import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { DialogService } from 'src/app/Services/dialog.service';

@Component({
  selector: 'app-event-panel',
  standalone: true,
  imports: [CommonModule, FormsModule, MatDatepickerModule, MatNativeDateModule, MatFormFieldModule],
  templateUrl: './event-panel.component.html',
  styles: []
})
export class EventPanelComponent implements OnInit {
  message: string = "";
  events: EventModel[] = [];
  event: EventModel = resetEvent();
  
  constructor(private eventService: EventService, private snackBar: SnackBarService, private dialogService:DialogService) { }

  ngOnInit(): void {
    this.eventService.getAllEvents().subscribe(x => this.events = x);
  }
  
editProduct(event: EventModel): void {
  Object.assign(this.event, event);
}
  
  deleteProduct(event: EventModel): void {
    this.dialogService.openDialog('Are you sure you want to delete this product?');
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
          this.snackBar.openSnackBar("Event created", '', 'success');
        },
        error: (err) => {
          console.log(err);
          this.message = Object.values(err.error.errors).join(", ");
          this.snackBar.openSnackBar(this.message, '', 'error');
        }
      });
    } else {
      //update
      this.eventService.updateEvent(this.event.id, this.event)
      .subscribe({
        error: (err) => {
          this.message = Object.values(err.error.errors).join(", ");
          this.snackBar.openSnackBar(this.message, '', 'error');
        },
        complete: () => {
          this.eventService.getAllEvents().subscribe(x => this.events = x);
          this.event = resetEvent();
          this.snackBar.openSnackBar("Event updated", '','success');
        }
      });
    }
    this.event = resetEvent();
  }
}
