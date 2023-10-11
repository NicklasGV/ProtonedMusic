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
  testikler: boolean = false;
  constructor(private authService: AuthService) {
    this.authService.currentUser.subscribe((x) => (this.currentUser = x));
  }

  ngOnInit(): void {
    console.log('Bruger logger ind:', this.authService.currentUserValue);

    this.authService.currentUser.subscribe((x) => {
      if (x != null) {
        this.isLoggedIn = true;
      }
    });
  }

  ngAfterViewChecked() {
    this.authService.pik.subscribe((emitted) => {
      this.isLoggedIn = emitted;
    });
  }

  roleCheck(): boolean {
    if (this.currentUser.role == this.roleChecker) {
      return true;
    }
    return false;
  }

  logout() {
    console.log('Bruger logger ud:', this.authService.currentUserValue);
    this.authService.logout();
    this.isLoggedIn = false;
    window.location.reload();
  }
}
