import { NewsModel, resetNews } from './../../Models/NewsModel';
import { NewsService } from './../../Services/news.service';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { User, resetUser } from 'src/app/Models/UserModel';
import { AuthService } from 'src/app/Services/auth.service';
import { UserService } from 'src/app/Services/user.service';


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
  itemlength = 0;
  itemsQuantity = 0;

  constructor(
    private newsService: NewsService,
    private authService: AuthService,
    private userService: UserService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {this.newsService.getNewsById(params['id']).subscribe(news => this.news = news);});
    
    console.log(this.news.userIds)
    this.currentUserId = this.authService.currentUserValue.id;
    console.log(this.currentUserId)

    this.userService.findById(1).subscribe(user => {this.user = user});
    
    
  }

  Like() {
    if (this.news.newsLikes.some(({id}) => id === this.currentUserId))
    {
      console.log("You unliked that")
    }
    else
    {
      console.log("You liked that")
    }
    
  }

  getUserInfo()
  {
    this.userService.findById(this.currentUser.id).subscribe(user => {this.authService.currentUserValue.id = user.id;});
  }

  isLiked(): boolean {
    // Check if the current user's ID is in the list of likes
    return this.news.newsLikes.some(({id}) => id === this.currentUserId);
  }

  isIdInArray(array: any[], targetId: any): boolean {
    return array.includes(targetId);
}

  
  toggleLike() {
    this.news.userIds = this.news.newsLikes.map(user => user.id)
    if (this.isLiked()) {
      // Unlike the news
      console.log("You unliked that")
      this.news.userIds = this.news.userIds.filter(user => this.currentUserId !== this.currentUserId);
    } else {
      // Like the news
      console.log("You liked that")
      this.news.userIds.push(this.currentUserId);
      
    }

    console.log(this.user)
    console.log(this.news.userIds)
    console.log(this.news)

    this.newsService.updateNews(this.news.id, this.news).subscribe({
      error: (err) => {
      },
      complete: () => {
        console.log("Works");
      }
    });
  }

}
