import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserService } from 'src/app/Services/user.service';
import { AuthService } from 'src/app/Services/auth.service';
import { User, resetUser } from 'src/app/Models/UserModel';
import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { AddonRoles } from 'src/app/Models/AddonRole';
import { constRoles } from 'src/app/Models/role';
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-usersettings',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './usersettings.component.html',
  styleUrls: ['./usersettings.component.css']
})
export class UsersettingsComponent implements OnInit {
  currentUser: User = resetUser();
  roleChecker: string = 'Newsletter';
  message: string = "";
  addonRoles: AddonRoles[] = [];

  constructor(private userService: UserService, private authService: AuthService, private snackBar: SnackBarService) { }

  ngOnInit(): void {
    this.userService.findById(this.authService.currentUserValue.id).subscribe(x => this.currentUser = x);
    this.authService.currentUser.subscribe((x) => (this.currentUser = x));
    this.addonRoles = constRoles;
  }

  roleCheck(): boolean {
    if (this.currentUser.role == this.roleChecker) {
      return true;
    }
    return false;
  }

  newsletterEvent() {
    if (this.currentUser.addonRoles == "None")
    {
      this.currentUser.addonRoles = "Newsletter";
      this.userService.update(this.currentUser)
      this.snackBar.openSnackBar("You are now subscribed to the newsletter", "", "success");
    }
    else if (this.currentUser.addonRoles == "Newsletter")
    {
      this.currentUser.addonRoles = "None";
      this.userService.update(this.currentUser)
      .subscribe({
        error: (err) => {
          this.message = Object.values(err.error.errors).join(", ");
          this.snackBar.openSnackBar(this.message, '', 'error');
        },
        complete: () => {
          this.currentUser = resetUser();
          this.snackBar.openSnackBar("User updated", '', 'success');
        }
      });
      this.snackBar.openSnackBar("You have now removed your subscription to the newsletter", "", "warning");
    }
  }

}
