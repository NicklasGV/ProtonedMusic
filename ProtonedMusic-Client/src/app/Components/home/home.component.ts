import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FrontpagePostService } from 'src/app/Services/frontpagePost.service';
import { FrontpagePost, resetFrontpage } from 'src/app/Models/FrontpagePostModel';
import { Banner, constBanners } from 'src/app/Models/banner';

@Component({
  selector: 'app-homepage',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomepageComponent implements OnInit {

  message: string = "";
  frontpagePosts: FrontpagePost[] = [];
  user: FrontpagePost = resetFrontpage();
  banner: Banner[] = [];
  selectedFile: File | undefined;
  formData = new FormData();
  
  
    constructor(private frontpagePostService: FrontpagePostService) { }
  
    ngOnInit(): void {
      this.frontpagePostService.getAll().subscribe(x => this.frontpagePosts = x);
      this.banner = constBanners;
    }

}
