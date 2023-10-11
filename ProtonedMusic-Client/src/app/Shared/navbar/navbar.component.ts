import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { User, resetUser } from 'src/app/Models/UserModel';
import { AuthService } from 'src/app/Services/auth.service';
import { UserService } from 'src/app/Services/user.service';


@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  currentUser: User = resetUser();
  roleCheck: any;
  roleChecker: string = 'Admin';

  constructor(private router: Router, private authService:AuthService, private userService: UserService) {
  }

  ngOnInit(): void {
    this.authService.currentUser.subscribe(x => this.currentUser = x);
    if (this.currentUser != null) {
    this.roleCheck = this.currentUser.role;
    }
    console.log('Bruger logger ind:', this.currentUser); 
    if (this.currentUser != null) {
      this.roleCheck = this.currentUser.role;
    }
  }


}
