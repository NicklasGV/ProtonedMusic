import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ProductService } from 'src/app/Services/Product.service';
import { ProductModel } from 'src/app/Models/ProductModel';
import { Cart, CartItem } from 'src/app/Models/CartModel';
import { CartService } from 'src/app/Services/cart.service';
import { Component, Input, OnInit } from '@angular/core';
import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { MatBadgeModule } from '@angular/material/badge';
import { DialogComponent } from 'src/app/Shared/dialog/dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { PriceService } from 'src/app/Services/Price.Service';

@Component({
  selector: 'app-merchandise',
  standalone: true,
  imports: [CommonModule, RouterModule, MatBadgeModule],
  templateUrl: './merchandise.component.html',
  styleUrls: ['./merchandise.component.css']
})
export class MerchandiseComponent implements OnInit {

  products: ProductModel[] = [];
  cart: CartItem[] = [];
  private _cart: Cart = { items: [] };
  itemlength = 0;
  itemsQuantity = 0;
  checkEmpty: boolean = false;
  userCurrency: string = 'DKK'; // Default currency

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
    private priceService: PriceService,
    private route: ActivatedRoute,
    private router: Router,
    private snackbar: SnackBarService,
    private dialog: MatDialog
  ) {}

  async ngOnInit(): Promise<void> {
    this.productService.getAllProducts().subscribe({
      next: (result) => {
        console.log('Products:', result);
        this.products = result;
        this.checkEmpty = this.checkIfEmpty();
        if (result.length > 0) {
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
      error: (error) => {
        console.error('Error getting products:', error);
      }
    });

    this.cartService.currentCart.subscribe((x) => (this.cart = x));

    // Get user's local currency and convert prices
    this.convertPricesToUserCurrency();
  }

  async convertPricesToUserCurrency(): Promise<void> {
    try {
      const geoLocation = await this.priceService.getGeolocation().toPromise(); // Fetch user's geolocation
      const localCurrency = this.priceService.getCountryCurrency(geoLocation.countryCode); // Fetch user's local currency
      this.userCurrency = localCurrency;

      for (const product of this.products) {
        console.log('Converting price for product:', product);
        product.price = await this.priceService.getPriceInLocalCurrency(product);
      }
      console.log('Products after price conversion:', this.products);
    } catch (error) {
      console.error('Error converting prices to local currency:', error);
    }
  }


  delay(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

  checkIfEmpty() {
    if (this.products == null || this.products.length <= 0) {
      return true;
    }
    return false;
  }

  formatCurrency(amount: number): string {
    return amount.toLocaleString('da-DK') + ' ' + this.userCurrency;
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
        this.snackbar.openSnackBar(products.name + ' added to cart Go to cart', '', 'success');
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
        this.snackbar.openSnackBar(products.name + ' added to cart Keep shopping', '', 'success');
      }
    });
  }
}
