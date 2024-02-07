import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartItem } from 'src/app/Models/CartModel';
import { HttpClient } from '@angular/common/http';
import { User, resetUser } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/Services/user.service';
import { AuthService } from 'src/app/Services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderHistoryService } from 'src/app/Services/orderHistory.service';
import { OrderHistoryModel, resetOrderHistory } from 'src/app/Models/OrderHistoryModel';
import { ProductModel, resetProducts } from 'src/app/Models/ProductModel';


@Component({
  selector: 'app-ordersuccess',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ordersuccess.component.html',
  styleUrl: './ordersuccess.component.css'
})
export class OrdersuccessComponent {
  cartItems: CartItem[] = [];
  user: User = resetUser();
  receiptLink: any[] = [];
  orders: OrderHistoryModel[] = [];
  order: OrderHistoryModel = resetOrderHistory();
  product: ProductModel = resetProducts();
  products: ProductModel[] = [];

  constructor(private orderService: OrderHistoryService, private userService: UserService, private authService : AuthService, private activatedRoute: ActivatedRoute, private router: Router,) {}

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

    this.orderService.postWebhook(this.user.email).subscribe((x) => x.forEach((link) => {this.receiptLink.push(link)}));
    const specificId = this.user.id;
    this.orderService.getOrderById(specificId).subscribe((x) => this.orders = x);

    console.log(this.receiptLink)
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
