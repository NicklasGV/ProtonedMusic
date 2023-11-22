import { Component, ElementRef, OnInit, Renderer2, ViewChild } from '@angular/core';
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
  showPassword: boolean = false;

  constructor(
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private userService: UserService,
    private formBuilder: FormBuilder,
    private snackBar: SnackBarService,
    private renderer: Renderer2
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

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  hidePassword(passwordInput: any) {
    this.renderer.setAttribute(passwordInput.nativeElement, 'type', 'password');
  }

  login(): void {
    this.message  = '';
    this.authService.login(this.email, this.password)
    .subscribe({
      next: async () => {
        this.router.navigate(['/login']);
        window.location.reload();
        this.snackBar.openSnackBar('Login Succesful','','success');
      },
      error: err => {
        console.error('Login error:', err);
        if (err.status === 400 || err.status === 401 || err.status === 500) {
          this.message = 'Incorrect email or password. Please try again.';
          this.snackBar.openSnackBar(this.message, 'asdfasd', 'error');
        } else {
          this.message = 'An unexpected error occurred.';
          this.snackBar.openSnackBar(this.message, 'asd', 'error');
        }
      }
    });
  }
  
  save(): void {
    if (this.userForm.valid && this.userForm.touched) {
      this.userForm.value.role = 'Customer'
      this.userForm.value.addonRole = 'None'
      if (this.user.id == 0) {
        this.userService.create(this.userForm.value).subscribe({
          next: (x) => {
            this.users.push(x);
            this.cancel();
            this.userForm.reset();
            this.snackBar.openSnackBar('User registered','','success');
          },
          error: (err) => {
            this.message = Object.values(err.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message,'','error');
          },
        });
      } else {
        this.userService.update(this.userForm.value).subscribe({
          error: (err) => {
            this.message = Object.values(err.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message, '', 'error');

          },
          complete: () => {
            this.userService.getAll().subscribe((x) => (this.users = x));
            this.userForm.reset();
            window.location.reload();
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
      country: new FormControl(''),
      profilePicturePath: new FormControl('')
    })
  }

}
