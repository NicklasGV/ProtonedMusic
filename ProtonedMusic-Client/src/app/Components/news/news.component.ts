import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { NewsModel, resetNews  } from 'src/app/Models/NewsModel';
import { NewsService } from 'src/app/Services/news.service';
import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-news',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css'],
})
export class NewsComponent implements OnInit {
  news: NewsModel[] = [];
  

  constructor(
    private newsService: NewsService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  

  ngOnInit(): void {
    this.newsService.getAllNews().subscribe({
      // This is the call to the service to get all news.
      next: (result) => {
        this.news = result;
        this.filterAndSortNews(); // Call the filterNews function after fetching news
      },
    });
  }

  // Function to filter news items with datetime in the past and sort them by datetime (newest first)
  filterAndSortNews() {
    const currentDate = new Date();
    this.news = this.news
      .filter(anews => new Date(anews.dateTime) <= currentDate)
      .sort((a, b) => new Date(b.dateTime).getTime() - new Date(a.dateTime).getTime());
  }

  

  navigateToNews(newsId: number) {
    // Here you can specify the route to navigate to, passing the newsId as a parameter
    this.router.navigate(['/newsProduct', newsId]);
  }


}
