import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductModel } from 'src/app/Models/ProductModel';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ProductService } from 'src/app/Services/Product.service';


@Component({
  selector: 'app-merchandise-product',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './merchandise-product.component.html',
  styleUrls: ['./merchandise-product.component.css']
})
export class MerchandiseProductComponent implements OnInit {
  products: ProductModel = new ProductModel();

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {this.productService.getProductById(params['id']).subscribe(products => this.products = products);});

  }
}
