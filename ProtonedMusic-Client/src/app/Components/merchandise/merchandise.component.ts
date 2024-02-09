import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ProductService } from 'src/app/Services/product.service';
import { ProductModel } from 'src/app/Models/ProductModel';
import { Cart, CartItem } from 'src/app/Models/CartModel';
import { CartService } from 'src/app/Services/cart.service';
import { Component, Input, OnInit } from '@angular/core';
import { SnackBarService } from 'src/app/Services/snack-bar.service';
import {MatBadgeModule} from '@angular/material/badge';
import { DialogComponent } from 'src/app/Shared/dialog/dialog.component';
import { MatDialog } from '@angular/material/dialog';


@Component({
  selector: 'app-merchandise',
  standalone: true,
  imports: [CommonModule, RouterModule, MatBadgeModule],
  templateUrl: './merchandise.component.html',
  styleUrls: ['./merchandise.component.css']
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
    private router: Router,
    private snackbar: SnackBarService,
    private dialog: MatDialog
  ) {}

  async ngOnInit(): Promise<void> {
    this.productService.getAllProducts().subscribe({
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
        this.checkEmpty = this.checkIfEmpty();
      },
    });
    this.cartService.currentCart.subscribe((x) => (this.cart = x));
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

  formatCurrency(amount: number): string {
    return amount.toLocaleString('da-DK') + ' DKK';
  }
  

  CartTotal(): number {
    return this.cartService.getCartTotal();
  }

  addToCart(products: ProductModel) {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        title: 'Item added to cart',
        img: products.productPicturePath,
        message: products.name,
        secondMessage: this.formatCurrency(products.price),
        confirmYes: 'Go to cart',
        confirmNo: 'Keep shopping'
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.itemlength += 1;
        let item: CartItem = {
        id: products.id,
        price: products.price,
        quantity: 1,
        name: products.name,
        picturePath: products.productPicturePath
      } as CartItem;
       this.cartService.addToCart(item);
       this.router.navigate(['/cart']);
        this.snackbar.openSnackBar(products.name + ' added to cart Go to cart','','success');
      }
      else {
        this.itemlength += 1;
        let item: CartItem = {
          id: products.id,
          price: products.price,
          quantity: 1,
          name: products.name,
          picturePath: products.productPicturePath
       } as CartItem;
       this.cartService.addToCart(item);
       this.snackbar.openSnackBar(products.name + ' added to cart Keep shopping','','success');
      }
    });
  }

}
