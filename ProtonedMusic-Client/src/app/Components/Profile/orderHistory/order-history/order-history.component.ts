import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { User, resetUser } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/Services/user.service';
import { ActivatedRoute, RouterModule, Router, Route } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { OrderHistoryService } from 'src/app/Services/orderHistory.service';
import { OrderHistoryModel, resetOrderHistory } from 'src/app/Models/OrderHistoryModel';
import { resetArtist } from 'src/app/Models/ArtistModel';
import { ProductModel, resetProducts } from 'src/app/Models/ProductModel';

@Component({
  selector: 'app-order-history',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './order-history.component.html',
  styleUrls: ['./order-history.component.css'],
})
export class OrderHistoryComponent implements OnInit {
  message: string = '';
  user: User = resetUser();
  orders: OrderHistoryModel[] = [];
  order: OrderHistoryModel = resetOrderHistory();
  product: ProductModel = resetProducts();
  products: ProductModel[] = [];
  totalAmount: number = 0;

  constructor(
    private userService: UserService,
    private router: Router,
    private authService: AuthService,
    private activatedRoute: ActivatedRoute,
    private orderService: OrderHistoryService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.userService.findById(this.authService.currentUserValue.id).subscribe((x) => (this.user = x));
    this.activatedRoute.paramMap.subscribe((params) => {
      if ( this.authService.currentUserValue == null || this.authService.currentUserValue.id == 0 || this.authService.currentUserValue.id != Number(params.get('id'))) 
    {
      this.router.navigate(['/']);
    } else
    {
      this.user = this.authService.currentUserValue;
    }
    });
    const specificId = this.user.id;
    this.orderService.getOrderById(specificId).subscribe((x) => this.orders = x);
  }

  formatCurrency(amount: number): string {
    return amount.toLocaleString('da-DK') + ' DKK';
  }

  formatCurrencyAndTotal(order: OrderHistoryModel): string {
    let totalPrice = 0;
  
    order.products.forEach(product => {
      totalPrice += product.price * product.quantity;
    });
    return totalPrice.toLocaleString('da-DK') + ' DKK';
  }
}
