import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductModel, resetProducts } from 'src/app/Models/ProductModel';
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
  products: ProductModel = resetProducts();
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

  addToCart(products: ProductModel, ItemAmount: number): void {
    this.itemlength += 1;
    ItemAmount = this.itemlength;
    console.log(products);
    let item: CartItem = {
      id: products.id,
      price: products.price,
      quantity: 1,
      name: products.name,
    } as CartItem;
    this.cartService.addToCart(item);
  }
}
