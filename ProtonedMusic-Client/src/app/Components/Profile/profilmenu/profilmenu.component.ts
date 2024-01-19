import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { Component, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { User, resetUser } from '../../../Models/UserModel';
import { AuthService } from 'src/app/Services/auth.service';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { UserService } from 'src/app/Services/user.service';
import { AvatarModule } from 'primeng/avatar';

@Component({
  selector: 'app-profilmenu',
  standalone: true,
  imports: [CommonModule, RouterModule, AvatarModule],
  templateUrl: './profilmenu.component.html',
  styleUrls: ['./profilmenu.component.css']
})
export class ProfilmenuComponent implements OnInit {
  message: string = "";
  user: User = resetUser();
  msg: string = '';
  selectedFile: File | undefined;
  formData = new FormData();
  today: Date = new Date();
  currentDay: any;
  constructor(private userService: UserService,private router: Router, private authService: AuthService, private activatedRoute: ActivatedRoute, private snackBar: SnackBarService, private datePipe: DatePipe) {
    this.WelcomeUser();
  }
  
  ngOnInit(): void {
    this.userService.findById(this.authService.currentUserValue.id).subscribe(x => this.user = x);
    this.activatedRoute.paramMap.subscribe( params => {
      if (this.authService.currentUserValue == null || this.authService.currentUserValue.id == 0 || this.authService.currentUserValue.id != Number(params.get('id')))
      {
        this.router.navigate(['/']);
      }
      //Store user in variable
      this.user = this.authService.currentUserValue;
    });
  }
  
  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
  }
  
  avatarLetterCheck(userName: string) {
    if (userName != null) {
      return userName.charAt(0)
    }
    return userName;
  }
  
  transformDate(date: any) {
    return this.datePipe.transform(date, 'HH:mm');
  }
  WelcomeUser() {
    this.currentDay = this.transformDate(this.today);
    if (this.currentDay >= '04:59' && this.currentDay <= '11:59')
    {
      return this.msg = "Good Morning"
    }
    else if (this.currentDay >= '12:00' && this.currentDay <= '15:59')
    {
      return this.msg = "Good Afternoon"
    }
    return this.msg = "Good Evening"
  }
  
  async Logout(): Promise<void> {
    this.authService.logout();
    window.location.reload();
    this.router.navigate(['/login']);
    this.snackBar.openSnackBar('Logged out','','info');
    window.location.reload();
  }
  
  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }
  
  
  
  async uploadImage() {
    if (this.selectedFile) {
      console.log(this.selectedFile)
      const formData = new FormData();
      formData.append('file', this.selectedFile);
    
      this.userService.uploadProfilePicture(this.authService.currentUserValue.id, formData).subscribe();
    }
    await this.delay(500);
    window.location.reload();
  }
}
