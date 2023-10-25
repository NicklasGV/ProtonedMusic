import { NewsModel, resetNews } from './../../Models/NewsModel';
import { NewsService } from './../../Services/news.service';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';


@Component({
  selector: 'app-news-detailed',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './news-detailed.component.html',
  styleUrls: ['./news-detailed.component.css']
})
export class NewsDetailedComponent implements OnInit {
  news: NewsModel = resetNews();
  itemlength = 0;
  itemsQuantity = 0;

  constructor(
    private newsService: NewsService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {this.newsService.getNewsById(params['id']).subscribe(news => this.news = news);});

  }

  Like() {
    console.log("You liked that")
  }

}
