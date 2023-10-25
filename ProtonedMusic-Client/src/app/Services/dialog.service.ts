import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { DialogComponent } from '../Shared/dialog/dialog.component';

@Injectable({
  providedIn: 'root'
})
export class DialogService {
  constructor(private dialog: MatDialog) { }

  openDialog(data?: any): Observable<any> {
    const dialogRef = this.dialog.open(DialogComponent, {
      width: '400px',
      data: data
    });

    return dialogRef.afterClosed();
  }
}
