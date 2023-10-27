import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserService } from 'src/app/Services/user.service';
import { Role, constRoles } from 'src/app/Models/role';
import { User, resetUser } from 'src/app/Models/UserModel';
import { MatDialog } from '@angular/material/dialog';
import { SnackBarService } from 'src/app/Services/snack-bar.service';

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
  
    constructor(private userService: UserService, private snackBar: SnackBarService, private dialog: MatDialog) { }
  
    ngOnInit(): void {
      this.userService.getAll().subscribe(x => this.users = x);
      this.roles = constRoles;
    }
  
  
    edit(user: User): void {
      Object.assign(this.user, user);
    }
  
    cancel(): void {
      this.user = resetUser();
      this.snackBar.openSnackBar('User canceled.', '','info');
    }
  
    save(): void {
      this.message = "";
      console.log(this.user)
      if (this.user.id == 0) {
        //create
        this.userService.create(this.user)
        .subscribe({
          next: (x) => {
            this.users.push(x);
            this.user = resetUser();
            this.snackBar.openSnackBar("User created", '', 'success');
          },
          error: (err) => {
            console.log(err);
            this.message = Object.values(err.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message, '', 'error');
            console.log(this.message)
          }
        });
      } else {
        //update
        this.userService.update(this.user)
        .subscribe({
          error: (err) => {
            this.message = Object.values(err.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message, '', 'error');
            console.log(this.message)
          },
          complete: () => {
            this.userService.getAll().subscribe(x => this.users = x);
            this.user = resetUser();
            this.snackBar.openSnackBar("User updated", '', 'success');
          }
        });
      }
      this.user = resetUser();
    }
}
