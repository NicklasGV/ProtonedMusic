import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from 'src/app/Services/auth.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/Services/user.service';
import { User, resetUser } from 'src/app/Models/UserModel';
import { Role, constRoles } from 'src/app/Models/role';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  resetUser(): User {
    throw new Error('Method not implemented.');
  }
  email: string = '';
  password: string = '';
  error = '';
  users: User[] = [];
  userForm: FormGroup = this.resetForm();
  user: User = resetUser();
  roles:Role[] = []

  constructor(
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private userService: UserService
  ) { }

  ngOnInit(): void {
    // redirect to home if already logged in
    /* if (this.authService.currentUserValue != null && this.authService.currentUserValue.id > 0) {
      this.router.navigate(['/']);
    } */
    //this.userService.getAll().subscribe(x => this.users = x);
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

  save(): void {
    if (this.userForm.valid && this.userForm.touched) {
      this.userForm.value.role = 0
      if (this.user.id == 0) {
        console.log(this.userForm.value);
        this.userService.create(this.userForm.value).subscribe({
          next: (x) => {
            this.users.push(x);
            this.cancel();
            console.log(this.users)
            this.userForm.reset();
          },
          error: (err) => {
            console.warn(Object.values(err.error.errors).join(','));
          },
        });
      } else {
        console.log(this.userForm.value)
        this.userService.update(this.userForm.value).subscribe({
          error: (err) => {
            console.warn(Object.values(err.error.errors).join(','));

          },
          complete: () => {
            this.userService.getAll().subscribe((x) => (this.users = x));
            this.cancel();
            this.userForm.reset();
          },
        });
      }

    }
  }

  cancel(): void {
    this.user = resetUser();
  }

  resetForm(): FormGroup {
    return new FormGroup({
      firstName: new FormControl(null),
      lastName: new FormControl(null),
      email: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required),
      phoneNumber: new FormControl(null),
      address: new FormControl(null),
      city: new FormControl(null),
      postal: new FormControl(null),
      country: new FormControl(null)
    })
  }

}
