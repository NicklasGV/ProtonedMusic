import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Order } from 'src/app/Models/Order';

@Component({
  selector: 'app-order-history',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './order-history.component.html',
  styleUrl: './order-history.component.css'
})
export class OrderHistoryComponent {
orderHistory: Order[] = [];


}
