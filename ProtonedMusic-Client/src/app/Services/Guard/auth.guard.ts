import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../auth.service';
import { SnackBarService } from '../snack-bar.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private authService: AuthService, private snackBar: SnackBarService) { }

  canActivate(route: ActivatedRouteSnapshot,state: RouterStateSnapshot) {
    const currentUser = this.authService.currentUserValue;

  if (currentUser && currentUser.role == 'Admin') {
    return true;
  } else {
    this.snackBar.openSnackBar('You are not authorized to access this page', '', 'error');
    this.router.navigate(['/login']);
    return false;
  }
}
  
}
