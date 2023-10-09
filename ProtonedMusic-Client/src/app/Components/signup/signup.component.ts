import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {   FormsModule, FormGroup, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from 'src/app/Services/user.service';
import { User } from 'src/app/Models/UserModel';
import { Role, constRoles } from 'src/app/Models/role';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {

  constructor(private userService: UserService) {

  }

  users: User[] = [];
  userForm: FormGroup = this.resetForm();
  user: User = this.resetUser();
  roles:Role[] = []

  resetUser(): User {
    return { id: 0, firstname: '', lastname: '', email: '', password: '', phonenumber:0, address:'', city:'', postal:0, country:'' ,role: constRoles[0] };
  }

  ngOnInit(): void {
    this.userService.getAll().subscribe(x => this.users = x);
  }

  save(): void {
    if (this.userForm.valid && this.userForm.touched) {
      this.userForm.value.role = 3
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
    this.user = this.resetUser();
  }

  resetForm(): FormGroup {
    return new FormGroup({
      firstName: new FormControl(null, Validators.required),
      lastName: new FormControl(null, Validators.required),
      email: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required),
      phonenumber: new FormControl(null, Validators.required),
      address: new FormControl(null, Validators.required),
      city: new FormControl(null, Validators.required),
      postal: new FormControl(null, Validators.required),
      country: new FormControl(null, Validators.required)
    })
  }
}