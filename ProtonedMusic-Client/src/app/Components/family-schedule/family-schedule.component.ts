import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

@Component({
  selector: 'app-family-schedule',
  standalone: true,
  imports: [CommonModule, MatNativeDateModule, MatDatepickerModule],
  templateUrl: './family-schedule.component.html',
  styleUrl: './family-schedule.component.css'
})
export class FamilyScheduleComponent {
  selected?: Date;

}
