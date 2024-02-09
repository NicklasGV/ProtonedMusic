import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventModel } from 'src/app/Models/EventModel';
import { EventService } from 'src/app/Services/event.service';
import { RouterModule } from '@angular/router';
import { CartService } from 'src/app/Services/cart.service';
import { CartItem } from 'src/app/Models/CartModel';
import { SnackBarService } from 'src/app/Services/snack-bar.service';

import { CarouselModule } from 'primeng/carousel';
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';


@Component({
  selector: 'app-events',
  standalone: true,
  imports: [CommonModule, RouterModule, CarouselModule, ButtonModule, TagModule],
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {
  events: EventModel[] = [];
  checkEmpty: boolean = false;
  

  constructor(private eventService: EventService, private cartService:CartService, private snackbar:SnackBarService) { }

  async ngOnInit(): Promise<void> {
    this.eventService.getAllEvents().subscribe({
      next: (result) => {
        this.events = result;
        this.checkEmpty = this.checkIfEmpty();
      },
    });
  }

  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
  }

  checkIfEmpty() {
    if (this.events.length <= 0)
    {
      return true;
    }
    return false;
  }

  addToCart(events: EventModel) {
    let item: CartItem = {
      id: events.id,
      price: events.price,
      quantity: 1,
      name: events.title,
      picturePath: events.eventPicturePath
    } as CartItem;
    this.cartService.addToCart(item);
    this.snackbar.openSnackBar(events.title + ' added to cart','','success');
  }

}
