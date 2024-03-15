import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { NewsModel, resetNews } from 'src/app/Models/NewsModel';
import { NewsService } from 'src/app/Services/news.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User, resetUser } from 'src/app/Models/UserModel';
import { AuthService } from 'src/app/Services/auth.service';
import { SnackBarService } from 'src/app/Services/snack-bar.service';

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
  checkEmpty: boolean = false;

  constructor(
    private newsService: NewsService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private snackBar: SnackBarService,
    private router: Router
  ) {}

  async ngOnInit(): Promise<void> {
    this.newsService.getAllNews().subscribe({
      next: (result) => {
        this.news = result;
        this.checkEmpty = this.checkIfEmpty();
        this.filterAndSortNews();
      },
    });

    this.currentUserId = this.authService.currentUserValue.id;
  }

  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
  }

  checkIfEmpty() {
    if (this.news == null || this.news.length <= 0)
    {
      return true;
    }
    return false;
  }

  filterAndSortNews() {
    const currentDate = new Date();
    this.news = this.news
      .filter((anews) => new Date(anews.dateTime) <= currentDate)
      .sort(
        (a, b) =>
          new Date(b.dateTime).getTime() - new Date(a.dateTime).getTime()
      );
  }

  navigateToNews(newsId: number) {
    this.router.navigate(['/newsDetailed', newsId]);
  }

  isLiked(anews: NewsModel): boolean {
    return anews.newsLikes.some(({ id }) => id === this.currentUserId);
  }

  isIdInArray(news: any, targetId: any): boolean {
    return news.newsLikes.some(
      (newsLike: { id: any }) => newsLike.id === targetId
    );
  }

  toggleLike(anews: NewsModel) {
    anews.userIds = anews.newsLikes.map((user) => user.id);
    if (this.isLiked(anews)) {
      // Unlike the news
      anews.userIds = anews.userIds.filter(userId => userId !== this.currentUserId);
    } else if (this.currentUserId == 0)
    {
      // Snackbar saying you need to be logged in to like
      this.snackBar.openSnackBar("You need to be logged in to like", '', 'error');
    } else {
      // Like the news
      anews.userIds.push(this.currentUserId);
    }

    this.newsService.updateNews(anews.id, anews).subscribe({
      error: (err) => {},
      complete: () => {
        this.newsService.getAllNews().subscribe({
          next: (result) => {
            this.news = result;
            this.filterAndSortNews();
          },
        });
      },
    });
  }
}
