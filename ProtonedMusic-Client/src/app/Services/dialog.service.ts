import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from '../Shared/dialog/dialog.component';

@Injectable({
  providedIn: 'root'
})
export class DialogService {
  
  constructor(private dialog:MatDialog) { }

  openDialog(title:string, message: string, img: File, secondMessage: string, confirmYes:string | 'Confirm', confirmNo: string) {
    this.dialog.open(DialogComponent, {
      data: { title: title, message: message, img: img, secondMessage: secondMessage, confirmYes: confirmYes, confirmNo: confirmNo }
    });
  }
}
