import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DialogService } from 'src/app/Services/dialog.service';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-dialog',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.css']
})
export class DialogComponent {
  constructor(private dialogService: DialogService, @Inject(MAT_DIALOG_DATA) public data: any) { }

  openDialog() {
    this.dialogService.openDialog({ /* optional data */ })
      .subscribe(result => {
        console.log('Dialog closed with result:', result);
      });
  }
}
