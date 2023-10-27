import { RouterModule } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventModel } from 'src/app/Models/EventModel';
import { CartService } from 'src/app/Services/cart.service';
import { EventService } from 'src/app/Services/event.service';
import { SnackBarService } from 'src/app/Services/snack-bar.service';

@Component({
  selector: 'app-upcoming',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './upcoming.component.html',
  styleUrls: ['./upcoming.component.css']
})
export class UpcomingComponent implements OnInit {
  events: EventModel[] = [];
  

  constructor(private eventService: EventService, private cartService:CartService, private snackbar:SnackBarService) { }

  ngOnInit(): void {
    this.eventService.getAllEvents().subscribe(x => this.events = x);
  }
}
