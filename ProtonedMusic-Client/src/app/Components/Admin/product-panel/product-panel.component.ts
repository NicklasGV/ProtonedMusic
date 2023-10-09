import { Component, OnInit, ChangeDetectorRef  } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductModel, resetProducts } from 'src/app/Models/ProductModel';
import { CategoryModel, resetCategory } from 'src/app/Models/CategoryModel';
import { ProductService } from 'src/app/Services/Product.service';
import { FormsModule } from '@angular/forms';
import { CategoryService } from 'src/app/Services/category.service';

@Component({
  selector: 'app-product-panel',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product-panel.component.html',
  styleUrls: ['./product-panel.component.css']
})
export class ProductPanelComponent implements OnInit {

  message: string = "";
  products: ProductModel[] = [];
  product: ProductModel = resetProducts();
  category: CategoryModel = resetCategory();
  categories: CategoryModel[] = [];
  selected: number[] = [];
  
  constructor(private productService: ProductService, private categoryService:CategoryService, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe(x => this.products = x);
    this.categoryService.getCategories().subscribe(x => this.categories = x);
    this.selected = this.categories.filter(x => x.checked == true ? x.id : null).map(x => x.id);
  }

  marked(event: any) {
    console.log(event)
    let value = parseInt(event.target.value);
    if (this.selected.indexOf(value) == -1) {
      this.selected.push(value);
    } else {
      this.selected.splice(this.selected.indexOf(value), 1);
    }
    this.selected.sort((a, b) => a - b);
    console.log("Seleted IDs ", this.selected);
  }
  resetCheckboxes(): void {
  this.categories.map(category => category.checked = false);
  this.selected.length = 0;
}
  
editProduct(product: ProductModel): void {
  this.resetCheckboxes();
  Object.assign(this.product, product);
  this.product.categories.forEach(category => {
    const existingCategory = this.categories.find(c => c.id === category.id);
    if (existingCategory) {
      existingCategory.checked = true;
      this.selected.push(existingCategory.id);
      console.log("selected", this.selected);
    }
  });
  console.log('Selected IDs:', this.selected);
}
  
  deleteProduct(product: ProductModel): void {
    if (confirm("Er du sikker på at du vil slette dette produkt?")) {
      this.productService.deleteProduct(product.id).subscribe(x => {
        this.products = this.products.filter(x => x.id != product.id);
      });
    }
  }

  cancel(): void {
    this.product = resetProducts();
    this.category = resetCategory();
    this.resetCheckboxes();
  }

  save(): void {
    this.message = "";
    if (this.product.id == 0) {
      //create
      this.product.categoryIds = this.selected;
      this.productService.createProduct(this.product)
      .subscribe({
        next: (x) => {
          this.products.push(x);
          this.product = resetProducts();
          this.category = resetCategory();
          this.resetCheckboxes();
        },
        error: (err) => {
          console.log(err);
          this.message = Object.values(err.error.errors).join(", ");
        }
      });
    } else {
      //update
      this.product.categoryIds = this.selected;
      this.productService.updateProduct(this.product.id, this.product)
      .subscribe({
        error: (err) => {
          this.message = Object.values(err.error.errors).join(", ");
        },
        complete: () => {
          this.productService.getAllProducts().subscribe(x => this.products = x);
          this.product = resetProducts();
          this.resetCheckboxes();
        }
      });
    }
    this.product = resetProducts();
  }
}
