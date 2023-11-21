import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryModel, resetCategory } from 'src/app/Models/CategoryModel';
import { ProductService } from 'src/app/Services/product.service';
import { CategoryService } from 'src/app/Services/category.service';
import { ProductModel } from 'src/app/Models/ProductModel';
import { FormsModule } from '@angular/forms';
import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { DialogService } from 'src/app/Services/dialog.service';
import { DialogComponent } from 'src/app/Shared/dialog/dialog.component';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';

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
  
  constructor(private productService: ProductService, private categoryService:CategoryService, private snackBar:SnackBarService, private dialog:MatDialog) { }

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe(x => this.products = x);
    this.categoryService.getCategories().subscribe(x => this.categories = x);
  }

  editCategory(category: CategoryModel): void {
    Object.assign(this.category, category);
  }

  deleteCategory(category: CategoryModel): void {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: { title: "Delete Event", message: "Are you sure you want to delete this category?" }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.categoryService.deleteCategory(category.id).subscribe(x => {
          this.categories = this.categories.filter(x => x.id != category.id);
        });
        this.snackBar.openSnackBar('Deletion successful.', '','success');
        console.log('Product deleted!');
      } else {
        // User canceled the operation
        this.snackBar.openSnackBar('Deletion canceled.', '','warning');
        console.log('Deletion canceled.');
      }
    });
  }

  cancel(): void {
    this.category = resetCategory();
    this.snackBar.openSnackBar('Category canceled.', '','info');
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
          this.snackBar.openSnackBar("Category created", '', 'success');
        },
        error: (err) => {
          console.log(err);
          this.message = Object.values(err.error.errors).join(", ");
          this.snackBar.openSnackBar(this.message, '', 'error');
        }
      });
    } else {
      //update
      this.categoryService.updateCategory(this.category.id, this.category)
      .subscribe({
        error: (err) => {
          this.message = Object.values(err.error.errors).join(", ");
          this.snackBar.openSnackBar(this.message, '', 'error');
        },
        complete: () => {
          this.categoryService.getCategories().subscribe(x => this.categories = x);
          this.category = resetCategory();
          this.snackBar.openSnackBar("Category updated", '', 'success');
        }
      });
    }
    this.category = resetCategory();
  }
}
