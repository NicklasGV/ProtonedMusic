import { Order } from './../../../../Models/Order';
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { User, resetUser } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/Services/user.service';
import { ActivatedRoute, RouterModule, Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { OrderHistoryService } from 'src/app/Services/orderHistory.service';

@Component({
  selector: 'app-order-history',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './order-history.component.html',
  styleUrls: ['./order-history.component.css']
})
export class OrderHistoryComponent {
  message: string = "";
  user: User = resetUser();
  orderHistory: Order[] = [];

  constructor(
    private userService: UserService,
    private router: Router,
    private authService: AuthService,
    private activatedRoute: ActivatedRoute,
    private orderHistoryService: OrderHistoryService
  ) {}

  ngOnInit(): void {
    this.userService.findById(this.authService.currentUserValue.id).subscribe(x => this.user = x);

    this.activatedRoute.paramMap.subscribe(params => {
      if (
        this.authService.currentUserValue == null ||
        this.authService.currentUserValue.id == 0 ||
        this.authService.currentUserValue.id != Number(params.get('id'))
      ) {
        this.router.navigate(['/']);
      } else {
        // Store user in variable
        this.user = this.authService.currentUserValue;

        // Fetch order history
        this.orderHistoryService.GetAllOrderHistory(this.user.id.toString()).subscribe(
          x => {
            console.log("Order history response:", x);
            this.orderHistory = x;
          },
          error => {
            console.error("Error fetching order history:", error);
          }
        );
      }
    });
  }
}
