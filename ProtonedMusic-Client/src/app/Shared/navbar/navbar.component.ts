import { UserService } from './../../Services/user.service';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { User, resetUser } from 'src/app/Models/UserModel';
import { AuthService } from 'src/app/Services/auth.service';
import { AvatarModule } from 'primeng/avatar';

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

  constructor(private authService: AuthService, private userService:UserService) {
    this.authService.currentUser.subscribe((x) => (this.currentUser = x));
  }

  ngOnInit(): void {
    this.authService.currentUser.subscribe((x) => {
      if (x != null) {
        this.isLoggedIn = true;
      }
    });
    if(this.currentUser.id > 0) {
      this.userService.findById(this.authService.currentUserValue.id).subscribe(x => this.currentUser = x);
    }
  }

  avatarCheck(thisuser: User): string {
    if (thisuser.id > 0)
    {
      if (thisuser.profilePicturePath == '') {
        return 'Letter'
      }
      else if (thisuser.profilePicturePath != '') {
        return 'PicPath'
      }
      else if (thisuser.profilePicturePath == '' && thisuser.firstName == '')
      {
        return 'No name'
      }
      return 'Nothing'
    }
    return 'DontShow'

  }

  avatarLetterCheck(userName: string) {
    if (userName != null) {
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
