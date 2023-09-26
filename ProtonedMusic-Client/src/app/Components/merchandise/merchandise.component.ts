import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ProductService } from 'src/app/Services/Product.service';
import { ProductModel } from 'src/app/Models/ProductModel';
import { Cart, CartItem } from 'src/app/Models/CartModel';
import { CartService } from 'src/app/Services/cart.service';
import { Component, Input, OnInit, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-merchandise',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './merchandise.component.html',
  styleUrls: ['./merchandise.component.css'],
})
export class MerchandiseComponent implements OnInit {
  products: ProductModel[] = []; // This is the array of products that will be displayed on the page.
  cart: CartItem[] = [];

  private _cart: Cart = { items: [] };
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
    private productService: ProductService,
    private cartService: CartService
  ) {}

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe({
      // This is the call to the service to get all products.
      next: (result) => {
        this.products = result;
      }, // This is the callback function that will be executed when the service returns the data.
    });
    this.cartService.currentCart.subscribe((x) => (this.cart = x));
  }

  CartTotal(): number {
    return this.cartService.getCartTotal();
  }

  addToCart(products: ProductModel) {
    let item: CartItem = {
      id: products.id,
      price: products.productPrice,
      quantity: 1,
      name: products.productName,
    } as CartItem;

    this.cartService.addToCart(item);
  }
}
