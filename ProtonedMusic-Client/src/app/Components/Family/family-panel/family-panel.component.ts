import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserService } from 'src/app/Services/user.service';
import { AuthService } from 'src/app/Services/auth.service';
import { User, resetUser } from 'src/app/Models/UserModel';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
  selector: 'app-family-panel',
  standalone: true,
  imports: [CommonModule, MatTooltipModule, RouterModule],
  templateUrl: './family-panel.component.html',
  styleUrl: './family-panel.component.css'
})
export class FamilyPanelComponent implements OnInit {
  user: User = resetUser();
  msg: string = '';

  constructor(private userService: UserService, private authService: AuthService, private activatedRoute:ActivatedRoute) {}

  ngOnInit() {
    this.userService.findById(this.authService.currentUserValue.id).subscribe(x => this.user = x);
    this.activatedRoute.paramMap.subscribe( params => {
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
