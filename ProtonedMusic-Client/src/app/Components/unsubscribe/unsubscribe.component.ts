import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { User, resetUser } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/Services/user.service';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { SnackBarService } from 'src/app/Services/snack-bar.service';

@Component({
  selector: 'app-unsubscribe',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './unsubscribe.component.html',
  styleUrl: './unsubscribe.component.css'
})
export class UnsubscribeComponent {
  message: string = "";
  users: User[] = [];
  user: User = resetUser();
  mailUser?: User;

  constructor(private userService: UserService, private snackBar: SnackBarService) {
  }

  unsubscribeEmail(mail: string) {
      this.userService.unsubscribe(mail).subscribe({
        next: (x) => {
          this.users.push(x);
          this.user = resetUser();
          this.snackBar.openSnackBar('Unsubscribed successfully', '', 'success');
        },
        error: (err) => {
          console.log(err);
          this.message = Object.values(err.error.errors).join(', ');
          this.snackBar.openSnackBar(this.message, '', 'error');
        },
      });
  }
}