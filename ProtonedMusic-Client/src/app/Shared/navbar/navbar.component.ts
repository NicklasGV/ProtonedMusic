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
  roleChecker: string = 'Admin';
  tt: boolean = false;

  constructor(private router: Router, private authService:AuthService, private userService: UserService) {

  }

  ngOnInit(): void {
    this.authService.currentUser.subscribe(x => this.currentUser = x);

    console.log('Bruger logger ind:', this.currentUser); 
  }

  roleCheck() : boolean
  {

    if (this.currentUser.role == this.roleChecker)
    {
      return true;
    }
    return false;
  }

}
