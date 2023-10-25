import { Component, OnInit, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MAT_SNACK_BAR_DATA } from '@angular/material/snack-bar';
import {MatIconModule} from '@angular/material/icon';

@Component({
  selector: 'app-snack-bar',
  standalone: true,
  imports: [CommonModule, MatIconModule],
  templateUrl: './snack-bar.component.html',
  styleUrls: ['./snack-bar.component.css']
})
export class SnackBarComponent implements OnInit {
  constructor(@Inject(MAT_SNACK_BAR_DATA) public data: any) { 
  }

  ngOnInit() {}

  get getIcon() {
    switch (this.data.snackType) {
      case 'Success':
        return 'done';
      case 'Error':
        return 'error';
      case 'Warn':
        return 'warning';
      case 'Info':
        return 'info';
    }
    return {type: this.data.snackType, icon: 'check'};
  }

  closeSnackbar() {
    this.data.snackBar.dismiss();
  }
}
