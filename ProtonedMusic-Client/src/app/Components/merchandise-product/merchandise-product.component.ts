import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductModel } from 'src/app/Models/ProductModel';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ProductService } from 'src/app/Services/Product.service';
import { CartItem, } from 'src/app/Models/CartModel';
import { CartService } from 'src/app/Services/cart.service';


@Component({
  selector: 'app-merchandise-product',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './merchandise-product.component.html',
  styleUrls: ['./merchandise-product.component.css']
})
export class MerchandiseProductComponent implements OnInit {
  products: ProductModel = new ProductModel();
  itemlength = 0;
  itemsQuantity = 0;

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private cartService: CartService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {this.productService.getProductById(params['id']).subscribe(products => this.products = products);});

  }

  addToCart(products: ProductModel) {
    console.log(products);
    let item: CartItem = {
      id: products.id,
      price: products.productPrice,
      quantity: 1,
      name: products.productName,
    } as CartItem;
    this.cartService.addToCart(item);
    this.itemlength += 1;
  }
}
