import { Component, OnInit, Renderer2 } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from 'src/app/Services/auth.service';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/Services/user.service';
import { User, resetUser } from 'src/app/Models/UserModel';
import { Role } from 'src/app/Models/role';
import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { DividerModule } from 'primeng/divider'; 
import { PasswordModule } from 'primeng/password';
import { StrongPasswordRegx } from 'src/app/Models/PasswordReqs';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, ReactiveFormsModule, DividerModule, PasswordModule],
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
  passwordError: string = '';

  constructor(
    private authService: AuthService,
    private router: Router,
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
          this.snackBar.openSnackBar(this.message, '', 'error');
        } else {
          this.message = 'An unexpected error occurred.';
          this.snackBar.openSnackBar(this.message, '', 'error');
        }
      }
    });
  }
  
  save(): void {
    if (this.userForm.valid && this.userForm.touched) {
      this.userForm.value.role = 'Customer'
      this.userForm.value.addonRole = 'None'
      this.userService.create(this.userForm.value).subscribe({
        next: (x) => {
          this.users.push(x);
          this.cancel();
          this.userForm.reset();
          this.snackBar.openSnackBar('User registered','','success');
        },
        error: err => {
          if (err.status === 400 || err.status === 409) {
            this.message = 'Email is already in use';
            this.userForm.reset();
            this.snackBar.openSnackBar(this.message, '', 'error');
          } else {
            this.message = 'An unexpected error occurred.';
            this.userForm.reset();
            this.snackBar.openSnackBar(this.message, '', 'error');
          }
        }
      });
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
      password: new FormControl(null, [Validators.required, Validators.pattern(StrongPasswordRegx)]),
      phoneNumber: new FormControl(0),
      address: new FormControl(''),
      city: new FormControl(''),
      postal: new FormControl(0),
      country: new FormControl(''),
      profilePicturePath: new FormControl('')
    })
  }

  get passwordFormField() {
    return this.userForm.get('password');
  }

}
