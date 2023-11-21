import { Component, OnInit, ChangeDetectorRef  } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductModel, resetProducts } from 'src/app/Models/ProductModel';
import { CategoryModel, resetCategory } from 'src/app/Models/CategoryModel';
import { ProductService } from 'src/app/Services/product.service';
import { FormsModule } from '@angular/forms';
import { CategoryService } from 'src/app/Services/category.service';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { DialogComponent } from 'src/app/Shared/dialog/dialog.component';

@Component({
  selector: 'app-product-panel',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product-panel.component.html',
  styles: []
})
export class ProductPanelComponent implements OnInit {
  message: string = "";
  products: ProductModel[] = [];
  product: ProductModel = resetProducts();
  category: CategoryModel = resetCategory();
  categories: CategoryModel[] = [];
  selected: number[] = [];
  selectedFile: File | undefined;
  formData = new FormData();
  
  constructor(private productService: ProductService, private categoryService:CategoryService, private snackBar: SnackBarService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe(x => this.products = x);
    this.categoryService.getCategories().subscribe(x => this.categories = x);
    this.selected = this.categories.filter(x => x.checked == true ? x.id : null).map(x => x.id);
  }

  marked(event: any) {
    let value = parseInt(event.target.value);
    if (this.selected.indexOf(value) == -1) {
      this.selected.push(value);
    } else {
      this.selected.splice(this.selected.indexOf(value), 1);
    }
    this.selected.sort((a, b) => a - b);
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
    }
  });
}

onFileSelected(event: any) {
  this.selectedFile = event.target.files[0];
}

uploadImage() {
  if (this.selectedFile) {
    const formData = new FormData();
    formData.append('file', this.selectedFile);

    this.productService.uploadProductPicture(this.product.id, formData).subscribe(
      (product: ProductModel) => {
        this.productService.getAllProducts().subscribe(x => this.products = x);
          this.product = resetProducts();
          this.snackBar.openSnackBar("Product Pic Updated", '', 'success');
      },
      (error) => {
        this.message = Object.values(error.error.errors).join(", ");
          this.snackBar.openSnackBar(this.message, '', 'error');
      }
    );
  }
}
  
  deleteProduct(product: ProductModel): void {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: { title: "Delete Product", message: "Are you sure you want to delete this product?" }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.productService.deleteProduct(product.id).subscribe(x => {
          this.products = this.products.filter(x => x.id != product.id);
        });
        this.snackBar.openSnackBar('Deletion successful.', '','success');
      } else {
        // User canceled the operation
        this.snackBar.openSnackBar('Deletion canceled.', '','warning');
      }
    });
  }

  cancel(): void {
    this.product = resetProducts();
    this.category = resetCategory();
    this.resetCheckboxes();
    this.snackBar.openSnackBar('Product canceled.', '','info');
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
          this.snackBar.openSnackBar("Product created", '', 'success');
        },
        error: (err) => {
          console.log(err);
          this.message = Object.values(err.error.errors).join(", ");
          this.snackBar.openSnackBar(this.message, '', 'error');
        }
      });
    } else {
      //update
      this.product.categoryIds = this.selected;
      this.productService.updateProduct(this.product.id, this.product)
      .subscribe({
        error: (err) => {
          this.message = Object.values(err.error.errors).join(", ");
          this.snackBar.openSnackBar(this.message, '', 'error');
        },
        complete: () => {
          this.productService.getAllProducts().subscribe(x => this.products = x);
          this.product = resetProducts();
          this.resetCheckboxes();
          this.snackBar.openSnackBar("Product updated", '', 'success');
        }
      });
    }
    this.product = resetProducts();
  }
}
