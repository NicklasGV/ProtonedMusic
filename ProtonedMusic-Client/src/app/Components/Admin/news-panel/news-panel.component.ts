import { Component, OnInit, ChangeDetectorRef  } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewsModel, resetNews } from 'src/app/Models/NewsModel';
import { NewsService } from 'src/app/Services/news.service';
import { FormsModule } from '@angular/forms';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialog } from '@angular/material/dialog';
import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { DialogComponent } from 'src/app/Shared/dialog/dialog.component';

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
  
  constructor(private newsService: NewsService, private cdr: ChangeDetectorRef, private snackBar: SnackBarService, private dialog: MatDialog) { }

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
    const dialogRef = this.dialog.open(DialogComponent, {
      data: { title: "Delete News", message: "Are you sure you want to delete this news?" }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.newsService.deleteNews(anews.id).subscribe(x => {
          this.news = this.news.filter(x => x.id != anews.id);
        });
        this.snackBar.openSnackBar('Deletion successful.', '','success');
        console.log('Product deleted!');
      } else {
        // User canceled the operation
        this.snackBar.openSnackBar('Deletion canceled.', '','warning');
        console.log('Deletion canceled.');
      }
    });
  }

  cancel(): void {
    this.anews = resetNews();
    this.resetCheckboxes();
    this.snackBar.openSnackBar('News canceled.', '','info');
  }

  save(): void {
    this.message = "";
    if (this.anews.id == 0) {
      //create
      this.newsService.createNews(this.anews)
      .subscribe({
        next: (x) => {
          this.news.push(x);
          this.anews = resetNews();
          this.resetCheckboxes();
          this.snackBar.openSnackBar("News created", '', 'success');
        },
        error: (err) => {
          console.log(err);
          this.message = Object.values(err.error.errors).join(", ");
          this.snackBar.openSnackBar(this.message, '', 'error');
        }
      });
    } else {
      //update
      this.newsService.updateNews(this.anews.id, this.anews)
      .subscribe({
        error: (err) => {
          this.message = Object.values(err.error.errors).join(", ");
          this.snackBar.openSnackBar(this.message, '', 'error');
        },
        complete: () => {
          this.newsService.getAllNews().subscribe(x => this.news = x);
          this.anews = resetNews();
          this.resetCheckboxes();
          this.snackBar.openSnackBar("News updated", '', 'success');
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
