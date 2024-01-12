import { CalendarService } from 'src/app/Services/calendar.service';
import { RouterModule } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UpcomingService } from 'src/app/Services/upcoming.service';
import { UpcomingModel } from 'src/app/Models/UpcomingModel';
import { CalendarModel } from 'src/app/Models/CalendarModel';

@Component({
  selector: 'app-upcoming',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './upcoming.component.html',
  styleUrls: ['./upcoming.component.css']
})
export class UpcomingComponent implements OnInit {
  upcomings: CalendarModel[] = [];
  checkEmpty: boolean = false;

  constructor(private calendarService: CalendarService) { }

  getUpcomingShows() {
    const currentTime = new Date();
    currentTime.setDate(currentTime.getDate() - 1);
    currentTime.setHours(23, 59, 59, 999);
    if (this.upcomings != null)
    {
      return this.upcomings.filter(upcoming => {
        const showDate = new Date(upcoming.date);
        return showDate > currentTime;
      });
    }
    return null;
  }

  async ngOnInit(): Promise<void> {
      this.calendarService.getAllContent().subscribe(x => this.upcomings = x);

      await this.delay(200);
      this.checkEmpty = this.checkIfEmpty();
  }

  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
  }

  checkIfEmpty() {
    if (this.upcomings == null || this.upcomings.length <= 0)
    {
      return true;
    }
    return false;
  }
}
