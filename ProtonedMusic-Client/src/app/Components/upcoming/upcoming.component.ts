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
  dateTime = new Date().toISOString();

  

  constructor(private upcomingService: UpcomingService, private snackbar:SnackBarService) { }

  ngOnInit(): void {
    this.upcomingService.getAllUpcomings().subscribe(x => this.upcomings = x);
    console.log(this.dateTime);
  }
}
