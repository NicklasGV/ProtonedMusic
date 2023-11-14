import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FrontpagePostService } from 'src/app/Services/frontpagePost.service';
import { FrontpagePost, resetFrontpage } from 'src/app/Models/FrontpagePostModel';
import { Banner, constBanners } from 'src/app/Models/banner';
import { SnackBarService } from 'src/app/Services/snack-bar.service';

@Component({
  selector: 'app-frontpagepost-panel',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './frontpagepost-panel.component.html',
  styleUrls: ['./frontpagepost-panel.component.css']
})
export class FrontpagepostPanelComponent implements OnInit {

  message: string = "";
  frontpagePosts: FrontpagePost[] = [];
  post: FrontpagePost = resetFrontpage();
  banner: Banner[] = [];
  selectedFile: File | undefined;
  formData = new FormData();
  
  
    constructor(private frontpagePostService: FrontpagePostService, private snackBar: SnackBarService) { }
  
    ngOnInit(): void {
      this.frontpagePostService.getAll().subscribe(x => this.frontpagePosts = x);
      this.banner = constBanners;
    }

    edit(post: FrontpagePost): void {
      Object.assign(this.post, post);
    }
  
    cancel(): void {
      this.post = resetFrontpage();
      this.snackBar.openSnackBar('User canceled.', '','warning');
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
            this.snackBar.openSnackBar("Profile Pic Updated", '', 'success');
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
            this.snackBar.openSnackBar("User created", '', 'success');
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
            this.snackBar.openSnackBar("User updated", '', 'success');
          }
        });
      }
      this.post = resetFrontpage();
    }
}
