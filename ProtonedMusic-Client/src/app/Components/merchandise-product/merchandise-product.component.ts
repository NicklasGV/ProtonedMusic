import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductModel, resetProducts } from 'src/app/Models/ProductModel';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ProductService } from 'src/app/Services/product.service';
import { CartItem } from 'src/app/Models/CartModel';
import { CartService } from 'src/app/Services/cart.service';

@Component({
  selector: 'app-merchandise-product',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './merchandise-product.component.html',
  styleUrls: ['./merchandise-product.component.css'],
})
export class MerchandiseProductComponent implements OnInit {
  products: ProductModel = resetProducts();
  //Nu jeg her
  product: ProductModel[] = [];
  itemlength = 0;
  itemsQuantity = 0;
  //nu jeg der
  productList: ProductModel[] = [];
  currentIndex: number = 0;
  itemsPerPage: number = 4;

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private cartService: CartService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.productService.getProductById(params['id']).subscribe({
        next: (product) => {
          this.products = product;
          product.beforePrice = product.price;
          if (product.discountProcent > 0) {
            product.price =
              product.price - (product.price / 100) * product.discountProcent;
          }
        },
      });
    });
    this.productService.getAllProducts().subscribe((products) => {
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
      picturePath: products.productPicturePath,
    } as CartItem;
    this.cartService.addToCart(item);
  }

  loadProducts(): void {
    const totalProducts = this.productList.length;
    const startIndex = this.currentIndex;
    const endIndex = startIndex + this.itemsPerPage;

    if (endIndex <= totalProducts) {
      this.product = this.productList.slice(startIndex, endIndex);
    } else {
      // Løkke
      const remainingItems = endIndex - totalProducts;
      this.product = [
        ...this.productList.slice(startIndex, totalProducts),
        ...this.productList.slice(0, remainingItems),
      ];
    }
  }
  nextProducts(): void {
    this.currentIndex = (this.currentIndex + 1) % this.productList.length;
    this.loadProducts();
  }

  previousProducts(): void {
    this.currentIndex =
      (this.currentIndex - 1 + this.productList.length) %
      this.productList.length;
    this.loadProducts();
  }
}
