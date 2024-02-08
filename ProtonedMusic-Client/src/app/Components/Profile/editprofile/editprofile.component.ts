import { User, resetUser } from 'src/app/Models/UserModel';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { UserService } from 'src/app/Services/user.service';
import { FormsModule } from '@angular/forms';
import { AddonRoles, constAddonRoles } from 'src/app/Models/AddonRole';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { SnackBarService } from 'src/app/Services/snack-bar.service';

@Component({
  selector: 'app-editprofil',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './editprofile.component.html',
  styleUrls: ['./editprofile.component.css']
})
export class EditprofilComponent{
  user: User = resetUser();
  message: string = "";

  constructor(private userService: UserService, private router: Router, private authService: AuthService, private activatedRoute: ActivatedRoute, private snackBar:SnackBarService) { }

  ngOnInit(): void {
    this.userService.findById(this.authService.currentUserValue.id).subscribe(x => this.user = x);
    this.activatedRoute.paramMap.subscribe( params => {
      if (this.authService.currentUserValue == null || this.authService.currentUserValue.id == 0 || this.authService.currentUserValue.id != Number(params.get('id')))
      {
        this.router.navigate(['/']);
      }
      this.user = this.authService.currentUserValue;
    });
  }

  errorOnChanges() {
    this.router.navigate(['../../../profilmenu', this.user.id, 'editprofil']);
  }

  save(): void {
    this.message = "";
    if (this.user.password != undefined)
      {
        this.userService.update(this.user)
        .subscribe({
          error: (err) => {
            this.message = Object.values(err.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message, '', 'error');
          },
          complete: () => {
            this.user = resetUser();
            this.snackBar.openSnackBar("User updated with password", '', 'success');
          }
        });
      }
      else if (this.user.password == undefined)
      {
        this.userService.updateNoPassword(this.user)
      .subscribe({
        error: (err) => {
          this.message = Object.values(err.error.errors).join(", ");
          this.snackBar.openSnackBar(this.message, '', 'error');
        },
        complete: () => {
          this.user = resetUser();
          this.snackBar.openSnackBar("User updated without password", '', 'success');
        }
      });
    }
    this.router.navigate(['../../../profilmenu', this.user.id]);
  }
}