import { UserService } from 'src/app/Services/user.service';
import { Component } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { User, resetUser } from 'src/app/Models/UserModel';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SnackBarService } from 'src/app/Services/snack-bar.service';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './footer.component.html',
  styles: ['']
})
export class FooterComponent{ 
  message: string = '';
  users: User[] = [];
  user: User = resetUser();
  currentUser: User = resetUser();
  roleChecker: string = 'Admin';
  currentYear: Date = new Date();
  currentYearString: any = this.transformYear(this.currentYear)?.toString();

  constructor(private authService: AuthService, private router:Router, private userService: UserService, private snackBar: SnackBarService, private datePipe: DatePipe) {
    this.authService.currentUser.subscribe((x) => (this.currentUser = x));
    if (this.currentUser.id <= 0){
      console.error("No user found");
    }
    else if (this.currentUser.id > 0)
    {
      this.userService.findById(this.authService.currentUserValue.id).subscribe({
        next: (x) => {
          this.user = x;
        },
        error: (err) => {
        }
      });
    }
  }

  transformYear(date: any) {
    return this.datePipe.transform(date, 'Y');
  }

  roleCheck(): boolean {
    if (this.currentUser.role == this.roleChecker) {
      return true;
    }
    return false;
  }

  subscribeNewsletter(mail: string) {
    this.userService.subscribe(mail).subscribe({
        next: (x) => {
          this.users.push(x);
          this.user = resetUser();
          this.snackBar.openSnackBar('Subscribed successfully', '', 'success');
        },
        error: (err) => {
          console.log(err);
          this.message = Object.values(err.error.errors).join(', ');
          this.snackBar.openSnackBar(this.message, '', 'error');
        },
      });
  }

}
