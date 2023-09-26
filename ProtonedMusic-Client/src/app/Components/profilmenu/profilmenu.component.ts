import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { User, resetUser } from '../../Models/UserModel';
import { AuthService } from 'src/app/Services/auth.service';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';

@Component({
  selector: 'app-profilmenu',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './profilmenu.component.html',
  styleUrls: ['./profilmenu.component.css']
})
export class ProfilmenuComponent implements OnInit {
  user: User = resetUser();

  constructor(/* private accountService: AccountService, */private router: Router, private authService: AuthService, private activatedRoute: ActivatedRoute) {
  }

  ngOnInit(): void { 
    /* this.accountService.FindById(this.authService.CurrentUserValue.id).subscribe(x => this.user = x);
    this.activatedRoute.paramMap.subscribe( params => {
      if (this.authService.CurrentUserValue == null || this.authService.CurrentUserValue.id == 0 || this.authService.CurrentUserValue.id != Number(params.get('id')))
      {
        this.router.navigate(['/']);
      }
      //Store user in variable
      this.user = this.authService.CurrentUserValue;
    }); */
  }

  Logout(): void {
    /* this.authService.logout();
    window.location.reload(); */
  } 

}
