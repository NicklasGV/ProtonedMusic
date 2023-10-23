import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private authService: AuthService) { }

  canActivate(route: ActivatedRouteSnapshot,state: RouterStateSnapshot) {
    const currentUser = this.authService.currentUserValue;

  if (currentUser && currentUser.role == 'Admin') {
    return true;
  } else {
    console.error("Access denied");
    this.router.navigate(['/']);
    return false;
  }
}
  
}
