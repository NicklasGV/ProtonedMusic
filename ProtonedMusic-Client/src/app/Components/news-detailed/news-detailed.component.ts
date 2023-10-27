import { NewsModel, resetNews } from './../../Models/NewsModel';
import { NewsService } from './../../Services/news.service';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { User, resetUser } from 'src/app/Models/UserModel';
import { AuthService } from 'src/app/Services/auth.service';


@Component({
  selector: 'app-news-detailed',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './news-detailed.component.html',
  styleUrls: ['./news-detailed.component.css']
})
export class NewsDetailedComponent implements OnInit {
  currentUser: User = resetUser();
  currentUserId: number = 0;
  news: NewsModel = resetNews();
  user: User = resetUser();
  e: number = 0;

  constructor(
    private newsService: NewsService,
    private authService: AuthService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {this.newsService.getNewsById(params['id']).subscribe(news => this.news = news);});
    
    this.currentUserId = this.authService.currentUserValue.id;   
  }

  isLiked(): boolean {
    // Check if the current user's ID is in the list of likes
    return this.news.newsLikes.some(({id}) => id === this.currentUserId);
  }

  isIdInArray(news: any, targetId: any): boolean {
    return news.newsLikes.some((newsLike: { id: any; }) => newsLike.id === targetId);
}

  
  toggleLike() {  
    this.news.userIds = this.news.newsLikes.map(user => user.id)
    if (this.isLiked()) {
      // Unlike the news
      this.news.userIds = this.news.userIds.filter(user => this.currentUserId !== this.currentUserId);
    } else {
      // Like the news
      this.news.userIds.push(this.currentUserId);
      
    }

    this.newsService.updateNews(this.news.id, this.news).subscribe({
      error: (err) => {
      },
      complete: () => {
        this.route.params.subscribe(params => {this.newsService.getNewsById(params['id']).subscribe(news => this.news = news);});
        console.log("Works");
      }
    });
  }

}
