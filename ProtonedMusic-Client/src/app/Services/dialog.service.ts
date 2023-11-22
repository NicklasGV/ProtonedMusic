import { Injectable } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { DialogComponent } from '../Shared/dialog/dialog.component';

@Injectable({
  providedIn: 'root'
})
export class DialogService {
  
  constructor(private dialog:MatDialog) { }

  openDialog(title:string, message: string) {
    this.dialog.open(DialogComponent, {
      data: { title: title, message: message }
    });
  }
}
