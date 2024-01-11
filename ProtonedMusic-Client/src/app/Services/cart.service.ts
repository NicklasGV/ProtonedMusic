import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { Cart,CartItem } from "../Models/CartModel";

@Injectable({
  providedIn: 'root',
})

export class CartService {
  private CartName: string = "webshopProjectBasket";
  currentCartSubject: BehaviorSubject<CartItem[]>;
  currentCart: Observable<CartItem[]>;
  public itemLength = 0;

  constructor() {
    this.currentCartSubject = new BehaviorSubject<CartItem[]>(
      JSON.parse(localStorage.getItem(this.CartName) || "[]")
    );
    this.currentCart = this.currentCartSubject.asObservable();
  }
//dthhdtg
  get currentBasketValue(): CartItem[] {
    return this.currentCartSubject.value;
  }

  saveCart(basket: CartItem[]): void {
    localStorage.setItem(this.CartName, JSON.stringify(basket));
    this.currentCartSubject.next(basket);
  }

  addToCart(item: CartItem): void {
    const basket = this.currentBasketValue;
    const existingItem = basket.find(basketItem => basketItem.id === item.id);

    if (existingItem) {
      existingItem.quantity += item.quantity;
      if (existingItem.quantity <= 0) {
        this.removeItemFromCart(item.id);
      }
    } else {
      basket.push(item);
      this.itemLength += 1;
    }

    this.saveCart(basket);
  }

  removeItemFromCart(productId: number): void {
    const basket = this.currentBasketValue;
    const index = basket.findIndex(item => item.id === productId);
    if (index !== -1) {
      basket.splice(index, 1);
    }
    this.saveCart(basket);
  }

  clearCart(): void {
    const basket: CartItem[] = [];
    this.saveCart(basket);
  }

  getCartTotal(): number {
    return this.currentCartSubject.value.reduce(
      (total, item) => total + item.price * item.quantity,
      0
    );
  }
}
