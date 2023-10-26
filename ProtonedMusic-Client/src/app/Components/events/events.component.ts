import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventModel } from 'src/app/Models/EventModel';
import { EventService } from 'src/app/Services/event.service';
import { RouterModule } from '@angular/router';
import { CartService } from 'src/app/Services/cart.service';
import { CartItem } from 'src/app/Models/CartModel';
import { SnackBarService } from 'src/app/Services/snack-bar.service';

@Component({
  selector: 'app-events',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {
  events: EventModel[] = [];
  

  constructor(private eventService: EventService, private cartService:CartService, private snackbar:SnackBarService) { }

  ngOnInit(): void {
    this.eventService.getAllEvents().subscribe(x => this.events = x);
  }

  addToCart(events: EventModel) {
    console.log(events);
    let item: CartItem = {
      id: events.id,
      price: events.price,
      quantity: 1,
      name: events.title,
    } as CartItem;
    this.cartService.addToCart(item);
    this.snackbar.openSnackBar(events.title + ' added to cart','','success');
  }

}
