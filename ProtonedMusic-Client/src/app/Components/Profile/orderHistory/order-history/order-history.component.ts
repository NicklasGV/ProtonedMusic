import { Order } from './../../../../Models/Order';
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { User, resetUser } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/Services/user.service';


@Component({
  selector: 'app-order-history',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './order-history.component.html',
  styleUrl: './order-history.component.css'
})
export class OrderHistoryComponent {
  user: User = resetUser();
  orderHistory: Order[] = [];

  constructor(private userService: UserService) {}

  ngOnInit(): void{
    this.userService.findById(1).subscribe(x => this.user = x);
  }
  
}
