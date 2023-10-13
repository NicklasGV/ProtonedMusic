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


  constructor() { }

  ngOnInit(): void {

  }

}
