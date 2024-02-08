import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { Cart,CartItem } from "../Models/CartModel";

@Injectable({
  providedIn: 'root',
})

export class CartService {
  private CartName: string = "UserCart";
  currentCartSubject: BehaviorSubject<CartItem[]>;
  currentCart: Observable<CartItem[]>;
  public itemLength = 0;
  itemTotal: any;


  constructor() {
    this.currentCartSubject = new BehaviorSubject<CartItem[]>(
      JSON.parse(localStorage.getItem(this.CartName) || "[]")
    );
    this.currentCart = this.currentCartSubject.asObservable();
  }

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

  getCartItemTotal() {
    // Retrieve the localStorage item
  const cartItemString = localStorage.getItem(this.CartName);

  // Check if the localStorage item is not null
  if (cartItemString !== null) {
    try {
      // Parse the JSON string to an object
      const parsedCartItem = JSON.parse(cartItemString);

      // Assuming your items are in an array under a key named 'items'
      // Replace 'items' with the actual key where your array is located
      const itemsArray = parsedCartItem;

      // Check if itemsArray is an array
      if (Array.isArray(itemsArray)) {
        // Sum up all quantities
        const totalQuantity = itemsArray.reduce((sum, item) => sum + item.quantity, 0);

        // Use 'totalQuantity' as needed, for example, set it to this.itemTotal
        this.itemTotal = totalQuantity;

        // Return the total quantity
        return this.itemTotal;
      } else {
        console.error('Invalid or missing items array in localStorage item');
        return 0; // or any default value
      }
    } catch (error) {
      // Handle the case where the JSON parsing fails
      console.error('Error parsing localStorage item:', error);
      return 0; // or any default value
    }
  } else {
    // Handle the case where the localStorage item is null
    console.error('localStorage item is null or undefined');
    return 0; // or any default value
  }
  }

  getCartTotal(): number {
    return this.currentCartSubject.value.reduce(
      (total, item) => total + item.price * item.quantity,
      0
    );
  }

  getFullCart(): CartItem[] {
    const cartItemString = localStorage.getItem(this.CartName);

    if (cartItemString !== null) {
      try {
        const parsedCartItem = JSON.parse(cartItemString);
        return Array.isArray(parsedCartItem) ? parsedCartItem : [];
      } catch (error) {
        console.error('Error parsing localStorage item:', error);
        return [];
      }
    } else {
      console.error('localStorage item is null or undefined');
      return [];
    }
  }
}
