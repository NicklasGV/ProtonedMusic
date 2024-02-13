import { Component, OnInit, importProvidersFrom } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { CartItem } from 'src/app/Models/CartModel';
import { User, resetUser } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/Services/user.service';
import { AuthService } from 'src/app/Services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderHistoryService } from 'src/app/Services/orderHistory.service';
import { OrderHistoryModel, resetOrderHistory } from 'src/app/Models/OrderHistoryModel';
import { ProductModel, resetProducts } from 'src/app/Models/ProductModel';
import { CartService } from 'src/app/Services/cart.service';
import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { ProductOrderModel } from 'src/app/Models/ProductOrderModel';
import { HttpStatusCode } from '@angular/common/http';


@Component({
  selector: 'app-ordersuccess',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ordersuccess.component.html',
  styleUrl: './ordersuccess.component.css',
})
export class OrdersuccessComponent implements OnInit {
  cartItems: CartItem[] = [];
  user: User = resetUser();
  receiptLink: any[] = [];
  orders: OrderHistoryModel[] = [];
  order: OrderHistoryModel = resetOrderHistory();
  product: ProductModel = resetProducts();
  products: ProductModel[] = [];
  message: string = '';

  constructor(
    private orderService: OrderHistoryService, 
    private userService: UserService, 
    private authService : AuthService, 
    private activatedRoute: ActivatedRoute, 
    private router: Router, 
    private datePipe: DatePipe,
    private cartService: CartService,
    private snackBar: SnackBarService) {}

    transformDate(date: any) {
      return this.datePipe.transform(date, 'yyyy-MM-dd');
    }

  async ngOnInit(): Promise<void> {
    this.userService.findById(this.authService.currentUserValue.id).subscribe((x) => (this.user = x));
    this.activatedRoute.paramMap.subscribe( params => {
      if (this.authService.currentUserValue == null || this.authService.currentUserValue.id == 0 || this.authService.currentUserValue.id != Number(params.get('id')))
      {
        this.router.navigate(['/']);
      }
      //Store user in variable
      this.user = this.authService.currentUserValue;
    });
    this.orderService.getStatusCode(this.user.email).subscribe(({
      complete: () => {
          const storedCart = localStorage.getItem('UserCart');
          const today = new Date()
          const cart = storedCart ? JSON.parse(storedCart) : [];
    
          this.order.customerId = this.authService.currentUserValue.id;
          this.order.orderDate = this.transformDate(today);
    
          const propertyToRemove = 'picturePath';
          const modifiedCart = cart.map((element: { [x: string]: any; picturepath: any; }) => {
          const { [propertyToRemove]: _, ...modifiedElement } = element;
          return modifiedElement;
          });
          modifiedCart.forEach((element: ProductOrderModel) => {
            this.order.products.push(element)
          });
          if (modifiedCart.length > 0)
          {
            this.orderService.createOrder(this.order)
            .subscribe({
              next: (x) => {
                this.cartService.clearCart();
                window.location.reload()
              },
              error: (err) => {
                console.log(err);
                this.message = Object.values(err.error.errors).join(", ");
                this.snackBar.openSnackBar(this.message, '', 'error');
              }
            });
          }
        },
        error: (err) => {
          console.log(err);
          this.message = Object.values(err.error.errors).join(", ");
        }
    }));

    const specificId = this.user.id;
    this.orderService.postWebhook(this.user.email).subscribe((x) => x.forEach((link) => {this.receiptLink.push(link)}));
    
    await this.delay(500)
      this.orderService.getOrderById(specificId).subscribe((x) => this.orders = x.slice(-1));
  }

  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
  }

  formatCurrency(amount: number): string {
    return amount.toLocaleString('da-DK') + ' DKK';
  }

  formatCurrencyAndTotal(cart: OrderHistoryModel): string {
    let totalPrice = 0;

    cart.products.forEach(product => {
      totalPrice += product.price * product.quantity;
    });
    return totalPrice.toLocaleString('da-DK') + ' DKK';
  }
}
