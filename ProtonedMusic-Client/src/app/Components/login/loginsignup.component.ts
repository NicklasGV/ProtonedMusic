import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from 'src/app/Services/auth.service';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './loginsignup.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  email: string = '';
  password: string = '';
  error = '';

  constructor(
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    // redirect to home if already logged in
    /* if (this.authService.currentUserValue != null && this.authService.currentUserValue.id > 0) {
      this.router.navigate(['/']);
    } */
  }

  login(): void {
    this.error = '';
    this.authService.login(this.email, this.password)
      .subscribe({
        next: () => {
          // get return url from route parameters or default to '/'
          let returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
          this.router.navigate([returnUrl]);
        },
        error: err => {
          if (err.error?.status == 400 || err.error?.status == 401 || err.error?.status == 500) {
            this.error = 'Forkert brugernavn eller kodeord';
          }
          else {
            this.error = err.error.title;
          }
        }
      });
  }

}
