import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { NewsModel, resetNews  } from 'src/app/Models/NewsModel';
import { NewsService } from 'src/app/Services/news.service';
import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User, resetUser } from 'src/app/Models/UserModel';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-news',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css'],
})
export class NewsComponent implements OnInit {
  currentUser: User = resetUser();
  currentUserId: number = 0;
  news: NewsModel[] = [];
  anews: NewsModel = resetNews();
  user: User = resetUser();
  

  constructor(
    private newsService: NewsService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  

  ngOnInit(): void {
    this.newsService.getAllNews().subscribe({
      next: (result) => {
        this.news = result;
        this.filterAndSortNews();
      },
    });

    this.currentUserId = this.authService.currentUserValue.id;
    console.log(this.currentUserId)
  }

  filterAndSortNews() {
    const currentDate = new Date();
    this.news = this.news
      .filter(anews => new Date(anews.dateTime) <= currentDate)
      .sort((a, b) => new Date(b.dateTime).getTime() - new Date(a.dateTime).getTime());
  }

  

  navigateToNews(newsId: number) {
    this.router.navigate(['/newsDetailed', newsId]);
  }

  isLiked(anews: NewsModel): boolean {
    return anews.newsLikes.some(({id}) => id === this.currentUserId);
  }

  isIdInArray(array: any[], targetId: any): boolean {
    return array.includes(targetId);
}

  
  toggleLike(anews: NewsModel) {
    anews.userIds = anews.newsLikes.map(user => user.id)
    if (this.isLiked(anews)) {
      // Unlike the news
      anews.userIds = anews.userIds.filter(user => this.currentUserId !== this.currentUserId);
    } else {
      // Like the news
      anews.userIds.push(this.currentUserId);
      
    }
    

    this.newsService.updateNews(anews.id, anews).subscribe({
      error: (err) => {
      },
      complete: () => {
        this.newsService.getAllNews().subscribe({
          next: (result) => {
            this.news = result;
            this.filterAndSortNews();
          },
        });
        console.log("Works");
      }
    });
  }

}
