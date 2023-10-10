import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserService } from 'src/app/Services/user.service';
import { Role, constRoles } from 'src/app/Models/role';
import { User, resetUser } from 'src/app/Models/UserModel';

@Component({
  selector: 'app-user-panel',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './user-panel.component.html',
  styles: [
  ]
})
export class UserPanelComponent implements OnInit {
  message: string = "";
  users: User[] = [];
  user: User = resetUser();
  roles: Role[] = [];
  
    constructor(private userService: UserService) { }
  
    ngOnInit(): void {
      this.userService.getAll().subscribe(x => this.users = x);
      this.roles = constRoles;
    }
  
  
    edit(user: User): void {
      Object.assign(this.user, user);
    }
  
    cancel(): void {
      this.user = resetUser();
    }
  
    save(): void {
      this.message = "";
      if (this.user.id == 0) {
        //create
        this.userService.create(this.user)
        .subscribe({
          next: (x) => {
            this.users.push(x);
            this.user = resetUser();
          },
          error: (err) => {
            console.log(err);
            this.message = Object.values(err.error.errors).join(", ");
          }
        });
      } else {
        //update
        this.userService.update(this.user)
        .subscribe({
          error: (err) => {
            this.message = Object.values(err.error.errors).join(", ");
          },
          complete: () => {
            this.userService.getAll().subscribe(x => this.users = x);
            this.user = resetUser();
          }
        });
      }
      this.user = resetUser();
    }
}
