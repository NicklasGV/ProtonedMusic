import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductModel, resetProducts } from 'src/app/Models/ProductModel';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ProductService } from 'src/app/Services/product.service';
import { CartItem } from 'src/app/Models/CartModel';
import { CartService } from 'src/app/Services/cart.service';
import { CategoryService } from 'src/app/Services/category.service';

@Component({
  selector: 'app-merchandise-product',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './merchandise-product.component.html',
  styleUrls: ['./merchandise-product.component.css'],
})
export class MerchandiseProductComponent implements OnInit {
  products: ProductModel = resetProducts();
  product: ProductModel[] = [];
  itemlength = 0;
  itemsQuantity = 0;
  productList: ProductModel[] = [];
  currentIndex: number = 1;
  itemsPerPage: number = 4;
  selectedCategory: number = 0;

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private cartService: CartService,
    private categoryService: CategoryService
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

          // Set the selected category based on the product's categories
          if (product.categories.length > 0) {
            this.selectedCategory = product.categories[0].id;
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
    const filteredProducts = this.productList.filter(
      (product) =>
        product.id !== this.products.id && // Exclude the current product
        product.categories.some((category) => category.id === this.selectedCategory)
    );

    const totalProducts = filteredProducts.length;
    const startIndex = this.currentIndex;
    const endIndex = startIndex + this.itemsPerPage;

    if (endIndex <= totalProducts) {
      this.product = filteredProducts.slice(startIndex, endIndex);
    } else {
      const remainingItems = endIndex - totalProducts;
      this.product = [
        ...filteredProducts.slice(startIndex, totalProducts),
        ...filteredProducts.slice(0, remainingItems),
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

  // Call this method when the selected category changes
  onCategoryChange(categoryId: number): void {
    this.selectedCategory = categoryId;
    this.loadProducts();
  }
}
