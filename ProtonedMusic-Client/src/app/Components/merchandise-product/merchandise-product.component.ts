import {  Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductModel, resetProducts } from 'src/app/Models/ProductModel';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ProductService } from 'src/app/Services/product.service';
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
  product: ProductModel[] = [];
  itemlength = 0;
  itemsQuantity = 0;
  productList: ProductModel[] = [];
  currentIndex: number = 0;
  itemsPerPage: number = 4;


  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private cartService: CartService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {this.productService.getProductById(params['id']).subscribe({
      next: (product) => {
        this.products = product;
            product.beforePrice = product.price;
            if (product.discountProcent > 0) {
              product.price = product.price - (product.price / 100 * product.discountProcent);
            }
        }
    });});
    this.productService.getAllProducts().subscribe(products => {
      this.productList = products;
      this.loadProducts();
    });
  }

  addToCart(products: ProductModel, ItemAmount: number): void {
    this.itemlength += 1;
    ItemAmount = this.itemlength;
    let item: CartItem = {
      id: products.id,
      price: products.price,
      quantity: 1,
      name: products.name,
      picturePath: products.productPicturePath
    } as CartItem;
    this.cartService.addToCart(item);
  }

  loadProducts(): void {
    const endIndex = this.currentIndex + this.itemsPerPage;
    this.product = this.productList.slice(this.currentIndex, endIndex);
  }

  loadNextProducts(): void {
    const totalProducts = this.productList.length;
    const nextIndex = this.currentIndex + 1;
    if (nextIndex + this.itemsPerPage <= totalProducts) {
      this.currentIndex = nextIndex;
    }
    this.loadProducts();
  }

  previousProducts(): void {
    const prevIndex = this.currentIndex - 1;
    if (prevIndex >= 0) {
      this.currentIndex = prevIndex;
    }
    this.loadProducts();
  }
}
