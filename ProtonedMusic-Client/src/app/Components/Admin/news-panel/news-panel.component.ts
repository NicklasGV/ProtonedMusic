import { Component, OnInit, ChangeDetectorRef  } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewsModel, resetNews } from 'src/app/Models/NewsModel';
import { NewsService } from 'src/app/Services/news.service';
import { FormsModule } from '@angular/forms';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-news-panel',
  standalone: true,
  imports: [CommonModule, FormsModule, MatDatepickerModule, MatNativeDateModule, MatFormFieldModule, MatInputModule],
  templateUrl: './news-panel.component.html',
  styles: []
})
export class NewsPanelComponent implements OnInit {

  message: string = "";
  news: NewsModel[] = [];
  anews: NewsModel = resetNews();
  selected: number[] = [];
  
  constructor(private newsService: NewsService, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.newsService.getAllNews().subscribe(x => this.news = x);
  }

  marked(event: any) {
    console.log(event)
    let value = parseInt(event.target.value);
    if (this.selected.indexOf(value) == -1) {
      this.selected.push(value);
    } else {
      this.selected.splice(this.selected.indexOf(value), 1);
    }
    this.selected.sort((a, b) => a - b);
    console.log("Seleted IDs ", this.selected);
  }
  
editNews(anews: NewsModel): void {
  Object.assign(this.anews, anews);
}
  
  deleteNews(anews: NewsModel): void {
    if (confirm("Er du sikker på at du vil slette dette opslag?")) {
      this.newsService.deleteNews(anews.id).subscribe(x => {
        this.news = this.news.filter(x => x.id != anews.id);
      });
    }
  }

  cancel(): void {
    this.anews = resetNews();
  }

  save(): void {
    this.message = "";
    if (this.anews.id == 0) {
      //create
      this.anews.dateTime = this.anews.dateTime.toISOString();
      this.newsService.createNews(this.anews)
      .subscribe({
        next: (x) => {
          this.news.push(x);
          this.anews = resetNews();
        },
        error: (err) => {
          console.log(err);
          this.message = Object.values(err.error.errors).join(", ");
        }
      });
    } else {
      //update
      this.newsService.updateNews(this.anews.id, this.anews)
      .subscribe({
        error: (err) => {
          this.message = Object.values(err.error.errors).join(", ");
        },
        complete: () => {
          this.newsService.getAllNews().subscribe(x => this.news = x);
          this.anews = resetNews();
        }
      });
    }
    this.anews = resetNews();
  }

  log_console(anews: NewsModel) {
    console.log
        (anews.dateTime);
}
}
