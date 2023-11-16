import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserService } from 'src/app/Services/user.service';
import { Role, constRoles } from 'src/app/Models/role';
import { User, resetUser } from 'src/app/Models/UserModel';
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
  selectedFile: File | undefined;
  formData = new FormData();
  
  
    constructor(private userService: UserService, private snackBar: SnackBarService) { }
  
    ngOnInit(): void {
      this.userService.getAll().subscribe(x => this.users = x);
      this.roles = constRoles;
    }
  
  
    edit(user: User): void {
      Object.assign(this.user, user);
    }
  
    cancel(): void {
      this.user = resetUser();
      this.snackBar.openSnackBar('User canceled.', '','warning');
    }
  
    

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  uploadImage() {
    if (this.selectedFile) {
      const formData = new FormData();
      formData.append('file', this.selectedFile);

      this.userService.uploadProfilePicture(this.user.id, formData).subscribe(
        (user: User) => {
          this.userService.getAll().subscribe(x => this.users = x);
            this.user = resetUser();
            this.snackBar.openSnackBar("Profile Pic Updated", '', 'success');
        },
        (error) => {
          this.message = Object.values(error.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message, '', 'error');
        }
      );
    }
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
            this.snackBar.openSnackBar("User created", '', 'success');
          },
          error: (err) => {
            console.log(err);
            this.message = Object.values(err.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message, '', 'error');
          }
        });
      } else {
        //update
        this.userService.update(this.user)
        .subscribe({
          error: (err) => {
            this.message = Object.values(err.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message, '', 'error');
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
