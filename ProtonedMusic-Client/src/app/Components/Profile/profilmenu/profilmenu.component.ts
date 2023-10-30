import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { User, resetUser } from '../../../Models/UserModel';
import { AuthService } from 'src/app/Services/auth.service';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-profilmenu',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './profilmenu.component.html',
  styles: []
})
export class ProfilmenuComponent implements OnInit {
  user: User = resetUser();
  msg: string = '';

  constructor(private userService: UserService,private router: Router, private authService: AuthService, private activatedRoute: ActivatedRoute, private snackBar: SnackBarService) {
  }

  ngOnInit(): void { 
    this.userService.findById(this.authService.currentUserValue.id).subscribe(x => this.user = x);
    this.activatedRoute.paramMap.subscribe( params => {
      if (this.authService.currentUserValue == null || this.authService.currentUserValue.id == 0 || this.authService.currentUserValue.id != Number(params.get('id')))
      {
        this.router.navigate(['/']);
      }
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

  async Logout(): Promise<void> {
    console.log('Bruger logger ud:', this.authService.currentUserValue);
    this.authService.logout();
    window.location.reload();
    this.router.navigate(['/login']);
    this.snackBar.openSnackBar('Logged out','','info');
  } 

}
