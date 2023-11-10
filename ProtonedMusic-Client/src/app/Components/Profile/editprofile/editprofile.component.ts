import { User, resetUser } from 'src/app/Models/UserModel';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { UserService } from 'src/app/Services/user.service';
import { FormsModule } from '@angular/forms';
import { AddonRoles, constRoles } from 'src/app/Models/AddonRole';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { SnackBarService } from 'src/app/Services/snack-bar.service';

@Component({
  selector: 'app-editprofil',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, MatSlideToggleModule],
  templateUrl: './editprofile.component.html',
  styles: []
})
export class EditprofilComponent{
  user: User = resetUser();
  addonRoles: AddonRoles[] = [];
  message: string = "";
  roleChecker: string = 'Newsletter';

  constructor(private userService: UserService, private router: Router, private authService: AuthService, private activatedRoute: ActivatedRoute, private snackBar:SnackBarService) { }

  ngOnInit(user: User): void {
    this.userService.findById(this.authService.currentUserValue.id).subscribe(x => this.user = x);
    this.activatedRoute.paramMap.subscribe( params => {
      if (this.authService.currentUserValue == null || this.authService.currentUserValue.id == 0 || this.authService.currentUserValue.id != Number(params.get('id')))
      {
        this.router.navigate(['/']);
      }
      //Store user in variable
      this.user = this.authService.currentUserValue;
    });
    Object.assign(this.user, user);

    this.addonRoles = constRoles;
  }

  resetPassword(): User {
    return resetUser();
  }

  roleCheck(): boolean {
    if (this.user.role == this.roleChecker) {
      return true;
    }
    return false;
  }

  save(): void {
    this.message = "";
    if (this.user.id != 0) {
      //update
      this.userService.update(this.user)
      .subscribe({
        error: (err) => {
          this.message = Object.values(err.error.errors).join(", ");
          this.snackBar.openSnackBar(this.message, "","error");
        },
        complete: () => {
          this.user = this.resetPassword();
          this.snackBar.openSnackBar("Profile updated", "","success")
        }
      });
    }
    this.router.navigate(['../../../profilmenu', this.user.id]);
  }
}