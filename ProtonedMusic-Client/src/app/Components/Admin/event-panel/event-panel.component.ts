import { Component, OnInit, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventModel, resetEvent } from 'src/app/Models/EventModel';
import { EventService } from 'src/app/Services/event.service';
import { FormsModule } from '@angular/forms';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';

@Component({
  selector: 'app-event-panel',
  standalone: true,
  imports: [CommonModule, FormsModule, MatDatepickerModule, MatNativeDateModule],
  templateUrl: './event-panel.component.html',
  styles: []
})
export class EventPanelComponent implements OnInit {
  message: string = "";
  events: EventModel[] = [];
  event: EventModel = resetEvent();
  
  constructor(private eventService: EventService) { }

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
        },
        error: (err) => {
          console.log(err);
          this.message = Object.values(err.error.errors).join(", ");
        }
      });
    } else {
      //update
      this.eventService.updateEvent(this.event.id, this.event)
      .subscribe({
        error: (err) => {
          this.message = Object.values(err.error.errors).join(", ");
        },
        complete: () => {
          this.eventService.getAllEvents().subscribe(x => this.events = x);
          this.event = resetEvent();
        }
      });
    }
    this.event = resetEvent();
  }
}
