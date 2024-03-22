import { constAddonRoles } from 'src/app/Models/AddonRole';
import { UserService } from './../../Services/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { User, resetUser } from 'src/app/Models/UserModel';
import { AuthService } from 'src/app/Services/auth.service';
import { AvatarModule } from 'primeng/avatar';
import { CartItem, Cart } from 'src/app/Models/CartModel';
import { CartService } from 'src/app/Services/cart.service';
import { ProductModel } from 'src/app/Models/ProductModel';
import { ProductService } from 'src/app/Services/Product.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule, AvatarModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  products: ProductModel[] = [];
  currentUser: User = resetUser();
  roleChecker: string = 'Admin';
  isLoggedIn: boolean = false;
  cart: CartItem[] = [];
  private _cart: Cart = { items: [] };
  itemlength = 0;
  itemsQuantity = 0;

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

  constructor(
    private authService: AuthService,
    private userService:UserService,
    private cartService: CartService,
    private productService: ProductService
    ) {
    this.authService.currentUser.subscribe((x) => (this.currentUser = x));
  }

  ngOnInit(): void {
    this.authService.currentUser.subscribe((x) => {
      if (x != null) {
        this.isLoggedIn = true;
      }
    });
    if(this.currentUser.id > 0) {
      this.userService.findById(this.authService.currentUserValue.id).subscribe(x => this.currentUser = x);
    }
    this.productService.getAllProducts().subscribe({
      // This is the call to the service to get all products.
      next: (result) => {
        this.products = result;
        if (result.length > 0){
          result.forEach((product) => {
            product.beforePrice = product.price;
            if (product.discountProcent > 0) {
              product.price = product.price - (product.price / 100 * product.discountProcent);
            }
          });
        }
        this.cart.forEach((element) => {
          this.itemlength += element.quantity;
        });
      }, // This is the callback function that will be executed when the service returns the data.
    });
    this.cartService.currentCart.subscribe((x) => (this.cart = x));
  }

  CartTotal(): number {
    return this.cartService.getCartTotal();
  }

  CartItemTotal(): number {
    return this.cartService.getCartItemTotal();
  }

  avatarCheck(thisuser: User): string {
    if (thisuser.id > 0)
    {
      if (thisuser.profilePicturePath == '' || thisuser.profilePicturePath == null || thisuser.profilePicturePath == undefined)
      {
        if (thisuser.firstName != null)
        {
          return 'Letter'
        }
        else {
          return 'Nothing'
        }
      }
      else if (thisuser.profilePicturePath != '') {
        return 'PicPath'
      }
    }
    return 'DontShow'

  }

  avatarLetterCheck(userName: string) {
    if (userName != null) {
      return userName.charAt(0)
    }
    return userName;
  }

  roleCheck(): boolean {
    if (this.currentUser.role == this.roleChecker) {
      return true;
    }
    return false;
  }

  collapseNavbar(): void {
    const navbar = document.getElementById('navbarSupportedContent');
    if (navbar?.classList.contains('show')) {
      navbar.classList.remove('show');
    }
  }
}
