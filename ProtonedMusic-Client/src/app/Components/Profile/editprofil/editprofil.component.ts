import { User, resetUser } from 'src/app/Models/UserModel';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { UserService } from 'src/app/Services/user.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-editprofil',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './editprofil.component.html',
  styles: []
})
export class EditprofilComponent{
  user: User = resetUser();
  message: string = "";

  constructor(private userService: UserService, private router: Router, private authService: AuthService, private activatedRoute: ActivatedRoute) { }

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
  }
  
  resetPassword(): User {
    return resetUser();
  }

  save(): void {
    this.message = "";
    if (this.user.id != 0) {
      //update
      this.userService.update(this.user)
      .subscribe({
        error: (err) => {
          this.message = Object.values(err.error.errors).join(", ");
        },
        complete: () => {
          this.user = this.resetPassword();
          this.message = "Profil opdateret";
        }
      });
    }
    this.router.navigate(['../../../profilmenu', this.user.id]);
  }
}
