import { Injectable } from '@angular/core';
import { CartItem } from '../Models/CartModel';
import { BehaviorSubject, Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class OrdersuccessService {
  private CartName: string = "testing";
  currentCartSubject: BehaviorSubject<CartItem[]>;
  currentCart: Observable<CartItem[]>;
  public itemlenght =0;

  constructor() {
    this.currentCartSubject = new BehaviorSubject<CartItem[]>(
      JSON.parse(localStorage.getItem(this.CartName)||"[]")
    );
    this.currentCart = this.currentCartSubject.asObservable();
   }


  get showBoughtItems(): CartItem[]
  {
    return this.currentCartSubject.value;
  }

  savedCart(basket: CartItem[]): void {
    localStorage.setItem(this.CartName, JSON.stringify(basket));
    this.currentCartSubject.next(basket);
  }


}
