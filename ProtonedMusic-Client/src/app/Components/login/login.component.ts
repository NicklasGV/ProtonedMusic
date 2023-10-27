import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from 'src/app/Services/auth.service';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/Services/user.service';
import { User, resetUser } from 'src/app/Models/UserModel';
import { Role, constRoles } from 'src/app/Models/role';
import { SnackBarService } from 'src/app/Services/snack-bar.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  email: string = '';
  password: string = '';
  message: string = '';	
  users: User[] = [];
  userForm: FormGroup = this.resetForm();
  user: User = resetUser();
  roles:Role[] = []
  loginForm: FormGroup;

  constructor(
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private userService: UserService,
    private formBuilder: FormBuilder,
    private snackBar: SnackBarService
  ) { 
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    // redirect to home if already logged in
    if (this.authService.currentUserValue != null && this.authService.currentUserValue.id > 0) {
      this.router.navigate(['/']);
    }
  }

  login(): void {
    this.message  = '';
    this.authService.login(this.email, this.password)
    .subscribe({
      next: () => {        
        //get return url from activatedRoute service or default to '/'
        let returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
        // this.authService.tt(true);
        this.router.navigate([returnUrl]);
        this.snackBar.openSnackBar('Login Succesful','','success');
      },
      error: err => {
        if (err.error?.status == 400 || err.error?.status == 401 || err.error?.status == 500) {
          this.message = Object.values(err.error.errors).join(", ");
          this.snackBar.openSnackBar(this.message, '', 'error');
        }
        else {
          this.message = Object.values(err.error.errors).join(", ");
          this.snackBar.openSnackBar(this.message, '', 'error');
        }
      }
    });
  }

  save(): void {
    if (this.userForm.valid && this.userForm.touched) {
      this.userForm.value.role = 'Customer'
      if (this.user.id == 0) {
        console.log(this.userForm.value);
        this.userService.create(this.userForm.value).subscribe({
          next: (x) => {
            this.users.push(x);
            this.cancel();
            console.log(this.users)
            this.userForm.reset();
            this.snackBar.openSnackBar('User registered','','success');
          },
          error: (err) => {
            this.message = Object.values(err.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message,'','error');
          },
        });
      } else {
        console.log(this.userForm.value)
        this.userService.update(this.userForm.value).subscribe({
          error: (err) => {
            this.message = Object.values(err.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message, '', 'error');

          },
          complete: () => {
            this.userService.getAll().subscribe((x) => (this.users = x));
            this.userForm.reset();
            this.snackBar.openSnackBar('User registered','','success');
            this.cancel();
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
      lastName: new FormControl(''),
      email: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required),
      phoneNumber: new FormControl(0),
      address: new FormControl(''),
      city: new FormControl(''),
      postal: new FormControl(0),
      country: new FormControl('')
    })
  }

}
