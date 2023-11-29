import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { User, resetUser } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/Services/user.service';
import { AuthService } from 'src/app/Services/auth.service';
import {MatTooltipModule} from '@angular/material/tooltip';

@Component({
  selector: 'app-admin-panel',
  standalone: true,
  imports: [CommonModule, RouterModule, MatTooltipModule],
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {
  user: User = resetUser();
  msg: string = '';

  constructor(private userService:UserService, private router: Router, private authService:AuthService, private activatedRoute:ActivatedRoute) { }

  ngOnInit(): void {
    this.userService.findById(this.authService.currentUserValue.id).subscribe(x => this.user = x);
    this.activatedRoute.paramMap.subscribe( params => {
      //Store user in variable
      this.user = this.authService.currentUserValue;
    });

    this.WelcomeUser();
  }

  WelcomeUser() {
    var today = new Date().getHours();
    if (today >= 6 && today <= 11)
    {
      return this.msg = "Good Morning"
    }
    else if (today >= 12 && today <= 13)
    {
      return this.msg = "Good Afternoon"
    }
    return this.msg = "Good Evening"
  }
}
