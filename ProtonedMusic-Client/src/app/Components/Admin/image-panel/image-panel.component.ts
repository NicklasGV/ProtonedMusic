import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ImageService } from 'src/app/Services/image.service';
import { PaginationComponent } from 'src/app/Shared/pagination/pagination.component';
import { ImageModel} from 'src/app/Models/ImageModel';

@Component({
  selector: 'app-image-panel',
  standalone: true,
  imports: [CommonModule, PaginationComponent],
  templateUrl: './image-panel.component.html',
  styles: []
})
export class ImagePanelComponent implements OnInit {
  queryResult: any = [];
  imageCount: any;
  images: ImageModel[] = [];
  imageBaseUrl: string = this.imageService.url;
  query: any = {
    pageSize: 6,
  };

  constructor(private imageService: ImageService) { }

  ngOnInit(): void {
    this.imageService.getAll().subscribe(x => this.images = x);
  }

  delete(id: number) {
    this.imageService.delete(id).subscribe({
      next: (res: any) => {
        console.log(res);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
  
  

}
