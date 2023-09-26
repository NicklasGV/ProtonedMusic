import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Cart, CartItem } from '../Models/CartModel';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  // BehaviorSubject til at holde styr på indkøbskurven
  cart = new BehaviorSubject<Cart>({ items: [] });

  constructor() {}

  // Tilføj et element til indkøbskurven
  addToCart(item: CartItem): void {
    // Opret en kopi af de eksisterende elementer i kurven
    const items = [...this.cart.value.items];

    // Find elementet i kurven, hvis det allerede eksisterer
    const itemInCart = items.find((_item) => _item.id === item.id);

    if (itemInCart) {
      // Øg antallet, hvis elementet findes i kurven
      itemInCart.quantity += 1;
    } else {
      // Tilføj elementet til kurven, hvis det ikke findes
      items.push(item);
    }

    // Opdater indkøbskurven ved at udsende en ny værdi
    this.cart.next({ items });
    console.log(this.cart.value);
  }

  // Fjern en mængde af et element fra kurven
  removeQuantity(item: CartItem): void {
    let itemForRemoval: CartItem | undefined;

    // Opdater antallet af elementer i kurven og identificer elementer, der skal fjernes helt
    let filteredItems = this.cart.value.items.map((_item) => {
      if (_item.id === item.id) {
        _item.quantity--;

        if (_item.quantity === 0) {
          itemForRemoval = _item;
        }
      }

      return _item;
    });

    // Fjern elementet helt, hvis det ikke længere har en mængde
    if (itemForRemoval) {
      filteredItems = this.removeFromCart(itemForRemoval, false);
    }

    // Opdater indkøbskurven
    this.cart.next({ items: filteredItems });
  }

  // Beregn den samlede pris for elementerne i kurven
  getTotal(items: Array<CartItem>): number {
    return items
      .map((item) => item.price * item.quantity)
      .reduce((prev, current) => prev + current, 0);
  }

  // Ryd indkøbskurven
  clearCart(): void {
    // Nulstil indkøbskurven til en tom tilstand
    this.cart.next({ items: [] });
    console.log(this.cart.value);
  }

  // Fjern et element fra kurven
  removeFromCart(item: CartItem, update = true): Array<CartItem> {
    // Filtrer elementet ud fra kurven
    const filteredItems = this.cart.value.items.filter(
      (_item) => _item.id !== item.id
    );

    // Opdater indkøbskurven, hvis det kræves
    if (update) {
      this.cart.next({ items: filteredItems });
    }
    
    return filteredItems;
  }
}
