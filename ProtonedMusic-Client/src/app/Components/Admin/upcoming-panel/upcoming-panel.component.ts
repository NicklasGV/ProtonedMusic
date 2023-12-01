import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UpcomingModel, resetUpcoming } from 'src/app/Models/UpcomingModel';
import { UpcomingService } from 'src/app/Services/upcoming.service';
import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { FormsModule } from '@angular/forms';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { DialogComponent } from 'src/app/Shared/dialog/dialog.component';

@Component({
  selector: 'app-upcoming-panel',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,],
  templateUrl: './upcoming-panel.component.html',
  styleUrls: ['./upcoming-panel.component.css']
})
export class UpcomingPanelComponent implements OnInit {
  message: string = '';
  upcomings: UpcomingModel[] = [];
  upcoming: UpcomingModel = resetUpcoming();
  selectedFile: File | undefined;
  formData = new FormData();

  constructor(
    private upcomingService: UpcomingService,
    private snackBar: SnackBarService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.upcomingService.getAllUpcomings().subscribe((x) => (this.upcomings = x));
  }

  editUpcoming(upcoming: UpcomingModel): void {
    Object.assign(this.upcoming, upcoming);
  }

  deleteUpcoming(upcoming: UpcomingModel): void {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        title: 'Delete Event',
        message: 'Are you sure you want to delete this event?',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.upcomingService.deleteUpcoming(upcoming.id).subscribe((x) => {
          this.upcomings = this.upcomings.filter((x) => x.id != upcoming.id);
        });
        this.snackBar.openSnackBar('Deletion successful.', '', 'success');
      } else {
        // User canceled the operation
        this.snackBar.openSnackBar('Deletion canceled.', '', 'warning');
      }
    });
  }

  cancel(): void {
    this.upcoming = resetUpcoming();
    this.snackBar.openSnackBar('Upcoming canceled.', '', 'info');
  }

  save(): void {
    this.message = '';
    if (this.upcoming.id == 0) {
      //create
      this.upcomingService.createUpcoming(this.upcoming).subscribe({
        next: (x) => {
          this.upcomings.push(x);
          this.upcoming = resetUpcoming();
          this.snackBar.openSnackBar('Event created', '', 'success');
        },
        error: (err) => {
          console.log(err);
          this.message = Object.values(err.error.errors).join(', ');
          this.snackBar.openSnackBar(this.message, '', 'error');
        },
      });
    } else {
      //update
      this.upcomingService.updateUpcoming(this.upcoming.id, this.upcoming).subscribe({
        error: (err) => {
          this.message = Object.values(err.error.errors).join(', ');
          this.snackBar.openSnackBar(this.message, '', 'error');
        },
        complete: () => {
          this.upcomingService.getAllUpcomings().subscribe((x) => (this.upcomings = x));
          this.upcoming = resetUpcoming();
          this.snackBar.openSnackBar('Event updated', '', 'success');
        },
      });
    }
    this.upcoming = resetUpcoming();
  }
}
