import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { AuthService } from '../auth.service';
import { SnackBarService } from '../snack-bar.service';

@Injectable({
  providedIn: 'root'
})
export class FamilyGuard  {
  constructor(private router: Router, private authService: AuthService, private snackBar: SnackBarService) { }

  canActivate() {
    const currentUser = this.authService.currentUserValue;

  if (currentUser && currentUser.role == 'Family' || currentUser.role == 'Admin') {
    return true;
  } else {
    this.snackBar.openSnackBar('You are not authorized to access this page', '', 'error');
    this.router.navigate(['/']);
    return false;
  }
}
}
