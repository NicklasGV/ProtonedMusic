import { RouterModule } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { UpcomingService } from 'src/app/Services/upcoming.service';
import { UpcomingModel } from 'src/app/Models/UpcomingModel';

@Component({
  selector: 'app-upcoming',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './upcoming.component.html',
  styleUrls: ['./upcoming.component.css']
})
export class UpcomingComponent implements OnInit {
  upcomings: UpcomingModel[] = [];


  getUpcomingShows(): any[] {
    const currentTime = new Date();
    currentTime.setDate(currentTime.getDate() - 1);
    currentTime.setHours(23, 59, 59, 999);

    return this.upcomings.filter(upcoming => {
      const showDate = new Date(upcoming.timeof);
      return showDate > currentTime;
    });
  }

  constructor(private upcomingService: UpcomingService) { }

  ngOnInit(): void {
    this.upcomingService.getAllUpcomings().subscribe(x => this.upcomings = x);
  }
}
