import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ProductService } from 'src/app/Services/product.service';
import { ProductModel } from 'src/app/Models/ProductModel';
import { Cart, CartItem } from 'src/app/Models/CartModel';
import { CartService } from 'src/app/Services/cart.service';
import { Component, Input, OnInit } from '@angular/core';
import { SnackBarService } from 'src/app/Services/snack-bar.service';

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
  itemlength = 0;
  itemsQuantity = 0;
  checkEmpty: boolean = false;

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
    private cartService: CartService,
    private route: ActivatedRoute,
    private snackbar: SnackBarService
  ) {}

  async ngOnInit(): Promise<void> {
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

    await this.delay(200);
    this.checkEmpty = this.checkIfEmpty();
  }

  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
  }

  checkIfEmpty() {
    if (this.products.length <= 0)
    {
      return true;
    }
    return false;
  }
  

  CartTotal(): number {
    return this.cartService.getCartTotal();
  }

  addToCart(products: ProductModel) {
    this.itemlength += 1;
    let item: CartItem = {
      id: products.id,
      price: products.price,
      quantity: 1,
      name: products.name,
      picturePath: products.productPicturePath
    } as CartItem;
    this.cartService.addToCart(item);
    this.snackbar.openSnackBar(products.name + ' added to cart','','success');
  }

}
