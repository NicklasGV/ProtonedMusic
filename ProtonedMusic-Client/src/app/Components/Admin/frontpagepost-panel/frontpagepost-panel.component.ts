import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FrontpagePostService } from 'src/app/Services/frontpagePost.service';
import { FrontpagePost, resetFrontpage } from 'src/app/Models/FrontpagePostModel';
import { Banner, constBanners } from 'src/app/Models/banner';
import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { FormsModule } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from 'src/app/Shared/dialog/dialog.component';


@Component({
  selector: 'app-frontpagepost-panel',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './frontpagepost-panel.component.html',
  styleUrls: ['./frontpagepost-panel.component.css']
})
export class FrontpagepostPanelComponent implements OnInit {

  message: string = "";
  frontpagePosts: FrontpagePost[] = [];
  post: FrontpagePost = resetFrontpage();
  banners: Banner[] = [];
  selectedFile: File | undefined;
  formData = new FormData();
  
  
    constructor(private frontpagePostService: FrontpagePostService, private snackBar: SnackBarService, private dialog: MatDialog) { }
  
    ngOnInit(): void {
      this.frontpagePostService.getAll().subscribe(x => this.frontpagePosts = x);
      this.banners = constBanners;
    }

    edit(post: FrontpagePost): void {
      Object.assign(this.post, post);
    }
  
    cancel(): void {
      this.post = resetFrontpage();
      this.snackBar.openSnackBar('User canceled.', '','warning');
    }

    delete(frontpagepost: FrontpagePost): void {
      const dialogRef = this.dialog.open(DialogComponent, {
        data: { title: "Delete page", message: "Are you sure you want to delete this page?",
        confirmYes: 'Confirm',
        confirmNo: 'Cancel' }
      });
  
      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.frontpagePostService.delete(frontpagepost.id).subscribe(x => {
            this.frontpagePosts = this.frontpagePosts.filter(x => x.id != frontpagepost.id);
          });
          this.snackBar.openSnackBar('Deletion successful.', '','success');
        } else {
          this.snackBar.openSnackBar('Deletion canceled.', '','warning');
        }
      });
    }

    onPictureFileSelected(event: any): void {
      const file = event.target.files[0];
      this.post.pictureFile = file;
    }
  
  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  uploadImage() {
    if (this.selectedFile) {
      const formData = new FormData();
      formData.append('file', this.selectedFile);

      this.frontpagePostService.uploadFrontpagePicture(this.post.id, formData).subscribe(
        (post: FrontpagePost) => {
          this.frontpagePostService.getAll().subscribe(x => this.frontpagePosts = x);
            this.post = resetFrontpage();
            this.snackBar.openSnackBar("Frontpage pic Updated", '', 'success');
        },
        (error) => {
          this.message = Object.values(error.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message, '', 'error');
        }
      );
    }
  }
    

    save(): void {
      this.message = "";
      console.log(this.post)
      if (this.post.id == 0) {
        //create
        this.frontpagePostService.create(this.post)
        .subscribe({
          next: (x) => {
            this.frontpagePosts.push(x);
            this.post = resetFrontpage();
            this.snackBar.openSnackBar("Page created", '', 'success');
          },
          error: (err) => {
            console.log(err);
            this.message = Object.values(err.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message, '', 'error');
          }
        });
      } else {
        //update
        this.frontpagePostService.update(this.post)
        .subscribe({
          error: (err) => {
            this.message = Object.values(err.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message, '', 'error');
          },
          complete: () => {
            this.frontpagePostService.getAll().subscribe(x => this.frontpagePosts = x);
            this.post = resetFrontpage();
            this.snackBar.openSnackBar("Page updated", '', 'success');
          }
        });
      }
      this.post = resetFrontpage();
    }
}
