import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationComponent } from 'src/app/Shared/pagination/pagination.component';
import {CloudinaryModule} from '@cloudinary/ng';
import { CloudinaryImage, Cloudinary } from '@cloudinary/url-gen';
import {fill} from "@cloudinary/url-gen/actions/resize";
import { ImageModel } from 'src/app/Models/ImageModel';
import { ImageService } from 'src/app/Services/image.service';


@Component({
  selector: 'app-image-panel',
  standalone: true,
  imports: [CommonModule, PaginationComponent, CloudinaryModule],
  templateUrl: './image-panel.component.html',
  styles: [],
})
export class ImagePanelComponent {
  img!: CloudinaryImage;
  images: ImageModel[] = [];

  constructor(private imgService: ImageService) {}

  ngOnInit() {
    // Create a Cloudinary instance and set your cloud name.
    const cld = new Cloudinary({
      cloud: {
        cloudName: 'dr79simff',
      },
    });
    // Instantiate a CloudinaryImage object for the image with the public ID, 'docs/models'.
    this.img = cld.image('/Uploads/ToastMalone');

    console.log(cld);

    // Resize to 250 x 250 pixels using the 'fill' crop mode.
    this.img.resize(fill().width(250).height(250));

    this.imgService.getImages().subscribe((data) => {
      this.images = data;
      console.log(data);
    });
  }
}

