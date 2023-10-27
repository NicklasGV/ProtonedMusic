import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { User, resetUser } from 'src/app/Models/UserModel';
import { AuthService } from 'src/app/Services/auth.service';
import { CartItem, Cart } from 'src/app/Models/CartModel';
import { CartService } from 'src/app/Services/cart.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  currentUser: User = resetUser();
  roleChecker: string = 'Admin';
  isLoggedIn: boolean = false;
  cart: CartItem[] = [];
  private _cart: Cart = { items: [] };
  itemsQuantity = 0;
  itemLength = 0;

  @Input()
  get carts(): Cart {
    return this._cart;
  }

  set carts(cart: Cart) {
    this._cart = cart;

    this.itemsQuantity = cart.items
      .map((item) => item.quantity)
      .reduce((prev, current) => prev + current, 0);
  }

  constructor(private authService: AuthService, private cartService: CartService) {
    this.authService.currentUser.subscribe((x) => (this.currentUser = x));
  }

  CartTotal(): number {
    return this.cartService.getCartTotal();
  }

  ngOnInit(): void {
    this.authService.currentUser.subscribe((x) => {
      if (x != null) {
        this.isLoggedIn = true;
      }
    });
    console.log('Bruger logger ind:', this.authService.currentUserValue);
  }

  roleCheck(): boolean {
    if (this.currentUser.role == this.roleChecker) {
      return true;
    }
    return false;
  }

  logout() {
    console.log('Bruger logger ud:', this.authService.currentUserValue);
    this.authService.logout();
    window.location.reload();
  }


}
