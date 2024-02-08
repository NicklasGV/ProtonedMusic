import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { Component, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { User, resetUser } from '../../../Models/UserModel';
import { AuthService } from 'src/app/Services/auth.service';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { UserService } from 'src/app/Services/user.service';
import { AvatarModule } from 'primeng/avatar';
import { ArtistService } from 'src/app/Services/artist.service';
import { ArtistModel, resetArtist } from 'src/app/Models/ArtistModel';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ImageCroppedEvent, ImageCropperModule, LoadedImage } from 'ngx-image-cropper';
import { DomSanitizer, HammerModule } from '@angular/platform-browser';


@Component({
  selector: 'app-profilmenu',
  standalone: true,
  imports: [CommonModule, RouterModule, AvatarModule, MatTooltipModule, ImageCropperModule, HammerModule],
  providers: [],
  templateUrl: './profilmenu.component.html',
  styleUrls: ['./profilmenu.component.css']
})
export class ProfilmenuComponent implements OnInit {
  message: string = "";
  user: User = resetUser();
  msg: string = '';
  formData = new FormData();
  today: Date = new Date();
  currentDay: any;
  artists: ArtistModel[] = [];
  artist: any;
  newArtist: ArtistModel = resetArtist();
  imageChangedEvent: any;
  croppedImage: any;
  blobFile: any;

  constructor(private userService: UserService,
    private router: Router,
    private authService: AuthService,
    private activatedRoute: ActivatedRoute,
    private snackBar: SnackBarService,
    private datePipe: DatePipe,
    private artistService: ArtistService,
    private sanitizer: DomSanitizer
  ) {
    this.WelcomeUser();
  }

  async ngOnInit(): Promise<void> {
    this.userService.findById(this.authService.currentUserValue.id).subscribe(x => this.user = x);
    this.activatedRoute.paramMap.subscribe(params => {
      if (this.authService.currentUserValue == null || this.authService.currentUserValue.id == 0 || this.authService.currentUserValue.id != Number(params.get('id'))) {
        this.router.navigate(['/']);
      }
      //Store user in variable
      this.user = this.authService.currentUserValue;
    });

    this.artistService.getAll().subscribe(x => this.artists = x);

    await this.delay(200);
    this.artists.forEach((artist) => {
      if (artist.user?.id == this.user.id) {
        this.artist = artist.id;
      }
    });
  }

  delay(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
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
    if (this.currentDay >= '04:59' && this.currentDay <= '11:59') {
      return this.msg = "Good Morning"
    }
    else if (this.currentDay >= '12:00' && this.currentDay <= '15:59') {
      return this.msg = "Good Afternoon"
    }
    return this.msg = "Good Evening"
  }

  async Logout(): Promise<void> {
    this.authService.logout();
    window.location.reload();
    this.router.navigate(['/login']);
    this.snackBar.openSnackBar('Logged out', '', 'info');
    window.location.reload();
  }




  async uploadImage() {
    if (this.blobFile) {
      const formData = new FormData();
      formData.append('file', this.blobFile);

      this.userService.uploadProfilePicture(this.authService.currentUserValue.id, formData).subscribe();
    }
    await this.delay(500);
    window.location.reload();
  }

  async removeImage() {
    this.userService.removeProfilePicture(this.user.id).subscribe();
    await this.delay(500);
    window.location.reload();
  }

  makeArtistPage() {
    if (this.user.firstName) {
      this.newArtist.name = this.user.firstName;
    }
    else {
      this.newArtist.name = "No Name Yet"
    }
    this.newArtist.info = "..."
    this.newArtist.userId = this.user.id;

    this.artistService.create(this.newArtist)
      .subscribe({
        next: (x) => {
          this.artists.push(x);
          this.newArtist = resetArtist();
          this.snackBar.openSnackBar("Artist page created", '', 'success');
          this.router.navigate(['/artist/', x.id]);
        },
        error: (err) => {
          console.log(err);
          this.message = Object.values(err.error.errors).join(", ");
          this.snackBar.openSnackBar(this.message, '', 'error');
        }
      });
  }



  fileChangeEvent(event: any): void {
    this.imageChangedEvent = event;
  }
  imageCropped(event: ImageCroppedEvent) {
    if (event.objectUrl) {
      this.croppedImage = this.sanitizer.bypassSecurityTrustUrl(event.objectUrl);
      if (event.blob) {
        this.blobFile = new File([event.blob], this.imageChangedEvent.target.files[0].name, { type: 'image/png' });
      }
    }
    

    // event.blob can be used to upload the cropped image
  }
  
  imageLoaded(image: LoadedImage) {
    // show cropper
  }
  cropperReady() {
    // cropper ready
  }
  loadImageFailed() {
    // show message
  }
}
