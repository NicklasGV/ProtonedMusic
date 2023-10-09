import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryModel, resetCategory } from 'src/app/Models/CategoryModel';
import { ProductService } from 'src/app/Services/Product.service';
import { CategoryService } from 'src/app/Services/category.service';
import { ProductModel } from 'src/app/Models/ProductModel';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-category-panel',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './category-panel.component.html',
  styles: []
})
export class CategoryPanelComponent implements OnInit {
  message: string = "";
  products: ProductModel[] = [];
  category: CategoryModel = resetCategory();
  categories: CategoryModel[] = [];
  
  constructor(private productService: ProductService, private categoryService:CategoryService) { }

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe(x => this.products = x);
    this.categoryService.getCategories().subscribe(x => this.categories = x);
  }

  editCategory(category: CategoryModel): void {
    Object.assign(this.category, category);
  }

  deleteCategory(category: CategoryModel): void {
    if (confirm("Er du sikker på at du vil slette dette produkt?")) {
      this.categoryService.deleteCategory(category.id).subscribe(x => {
        this.products = this.products.filter(x => x.id != category.id);
      });
    }
  }

  cancel(): void {
    this.category = resetCategory();
  }

  save(): void {
    this.message = "";
    if (this.category.id == 0) {
      //create
      this.categoryService.createCategory(this.category)
      .subscribe({
        next: (x) => {
          this.categories.push(x);
          this.category = resetCategory();
        },
        error: (err) => {
          console.log(err);
          this.message = Object.values(err.error.errors).join(", ");
        }
      });
    } else {
      //update
      this.categoryService.updateCategory(this.category.id, this.category)
      .subscribe({
        error: (err) => {
          this.message = Object.values(err.error.errors).join(", ");
        },
        complete: () => {
          this.categoryService.getCategories().subscribe(x => this.categories = x);
          this.category = resetCategory();
        }
      });
    }
    this.category = resetCategory();
  }
}
