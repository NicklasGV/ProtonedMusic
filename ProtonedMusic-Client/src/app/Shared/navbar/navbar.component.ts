import { UserService } from './../../Services/user.service';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { User, resetUser } from 'src/app/Models/UserModel';
import { AuthService } from 'src/app/Services/auth.service';
import { Avatar, AvatarModule } from 'primeng/avatar';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule, AvatarModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  currentUser: User = resetUser();
  roleChecker: string = 'Admin';
  isLoggedIn: boolean = false;

  constructor(private authService: AuthService, private router:Router, private userService:UserService) {
    this.authService.currentUser.subscribe((x) => (this.currentUser = x));
  }

  ngOnInit(): void {
    this.authService.currentUser.subscribe((x) => {
      if (x != null) {
        this.isLoggedIn = true;
      }
    });

    this.userService.findById(this.authService.currentUserValue.id).subscribe(x => this.currentUser = x);
  }

  avatarCheck(userRole: any, userPicpath: string): number {
    if (userRole != null && userPicpath != null && userPicpath.length > 0) {
      return 1;
    }
    if (userRole != null && userPicpath.length <= 0) {
      return 2;
    }
    return 0;
  }

  avatarLetterCheck(userName: string, userPicpath: string) {
    if (userName != null && userPicpath.length <= 0) {
      return userName.charAt(0)
    }
    return userName;
  }

  roleCheck(): boolean {
    if (this.currentUser.role == this.roleChecker) {
      return true;
    }
    return false;
  }

  collapseNavbar(): void {
    const navbar = document.getElementById('navbarSupportedContent');
    if (navbar?.classList.contains('show')) {
      navbar.classList.remove('show');
    }
  }
}
