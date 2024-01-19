import { Injectable } from '@angular/core';
import {MatSnackBar} from '@angular/material/snack-bar';
import { SnackBarComponent } from '../Shared/snack-bar/snack-bar.component';
type snackType = 'success' | 'error' | 'warning' | 'info';

@Injectable({
  providedIn: 'root'
})
export class SnackBarService {
  constructor(private snackBar: MatSnackBar) {}
  
  public openSnackBar(message: string, action:string , type: snackType) {
    const _snackType = type !== undefined ? type : 'success';
    this.snackBar.openFromComponent(SnackBarComponent, {
      duration: 4000,
      verticalPosition: 'bottom',
      data: { message: message, action: action , snackType: _snackType }
    });
  }
}
