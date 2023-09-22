import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ProductService } from 'src/app/Services/Product.service';
import { ProductModel } from 'src/app/Models/ProductModel';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-merchandise',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './merchandise.component.html',
  styleUrls: ['./merchandise.component.css']
})
export class MerchandiseComponent implements OnInit {
  products: ProductModel[] = []; // This is the array of products that will be displayed on the page.

  constructor(private productService: ProductService) { }
  
  ngOnInit(): void { 
    this.productService.getAllProducts().subscribe({// This is the call to the service to get all products.
      next:(result) => {this.products = result;}  // This is the callback function that will be executed when the service returns the data.
    })
  }

}
