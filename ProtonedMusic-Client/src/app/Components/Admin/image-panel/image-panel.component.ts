import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationComponent } from 'src/app/Shared/pagination/pagination.component';
import { HttpClient } from '@angular/common/http';
import { ImageService } from 'src/app/Services/image.service';
import { ImageModel } from 'src/app/Models/ImageModel';


@Component({
  selector: 'app-image-panel',
  standalone: true,
  imports: [CommonModule, PaginationComponent],
  templateUrl: './image-panel.component.html',
  styles: []
})
export class ImagePanelComponent implements OnInit {
  image : ImageModel[] = [];

  constructor(
    private http: HttpClient,
    private imageservice: ImageService,
    ) { 

  }
  ngOnInit(): void {
    this.imageservice.getImages().subscribe(x => this.image = x);
  }



}
