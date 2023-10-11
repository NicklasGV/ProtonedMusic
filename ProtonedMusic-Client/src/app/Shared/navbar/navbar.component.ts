import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { User, resetUser } from 'src/app/Models/UserModel';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  currentUser: User = resetUser();
  roleChecker: string = 'Admin';
  isLoggedIn: boolean = false;

  constructor(
    private authService: AuthService,

    ) {
      
      this.authService.currentUser.subscribe((x) => (this.currentUser = x));
    }
    
    ngOnInit(): void {
      console.log(this.currentUser.role);
      console.log('Bruger logger ind:', this.authService.currentUserValue);
      this.authService.currentUser.subscribe((x) => {
        this.currentUser = x;
        this.isLoggedIn = !!x;
      })
    }

  roleCheck(): boolean {
    if (this.currentUser.role == this.roleChecker) {
      return true;
    }
    return false;
  }

    logout() {
    this.authService.logout();
    console.log('Bruger logger ud:', this.authService.currentUserValue);
  }
}
