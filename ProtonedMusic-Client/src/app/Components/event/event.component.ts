import { EventService } from './../../Services/event.service';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CartService } from 'src/app/Services/cart.service';
import { EventModel, resetEvent } from 'src/app/Models/EventModel';
import { CartItem } from 'src/app/Models/CartModel';
import { SnackBarService } from 'src/app/Services/snack-bar.service';

@Component({
  selector: 'app-event',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.css']
})
export class EventComponent implements OnInit {
  events: EventModel = resetEvent();
  itemlength = 0;
  itemsQuantity = 0;

  constructor(private route: ActivatedRoute, private cartService: CartService, private eventService: EventService, private snackbar:SnackBarService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {this.eventService.getEventById(params['id']).subscribe(event => this.events = event);});
  }

  addToCart(eventItem: EventModel) {
    this.itemlength += 1;
    let item: CartItem = {
      id: eventItem.id,
      price: eventItem.price,
      quantity: 1,
      name: eventItem.title,
      picturePath: eventItem.eventPicturePath
    } as CartItem;
    this.cartService.addToCart(item);
    this.snackbar.openSnackBar(eventItem.title + ' added to cart','','success');
  }

}
