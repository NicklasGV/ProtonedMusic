import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArtistService } from 'src/app/Services/artist.service';
import { Router } from '@angular/router';
import { User, resetUser } from 'src/app/Models/UserModel';
import { ArtistModel, resetArtist } from 'src/app/Models/ArtistModel';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { FormsModule } from '@angular/forms';
import { DialogComponent } from 'src/app/Shared/dialog/dialog.component';
import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { MatDialog } from '@angular/material/dialog';
import { LinkModel, resetLink } from 'src/app/Models/LinkModel';
import { LinkService } from 'src/app/Services/link.service';
import { MusicModel, resetMusic } from 'src/app/Models/MusicModel';
import { MusicService } from 'src/app/Services/music/music.service';
import { ImageCroppedEvent, ImageCropperModule, LoadedImage } from 'ngx-image-cropper';
import { DomSanitizer, HammerModule } from '@angular/platform-browser';



@Component({
  selector: 'app-artist-detailed',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, ImageCropperModule, HammerModule],
  templateUrl: './artist-detailed.component.html',
  styleUrl: './artist-detailed.component.css'
})
export class ArtistDetailedComponent implements OnInit {
  message: string = "";

  currentUser: User = resetUser();
  currentUserId: number = 0;
  artist: ArtistModel = resetArtist();
  link: LinkModel = resetLink();
  song: MusicModel = resetMusic();
  checkEmpty: boolean = false;
  editMode: boolean = false;
  pictureChanged: boolean = false;
  linksChanged: boolean = false;
  linkModal: boolean = false;
  songModal: boolean = true;
  chosenLinkId: number = 0;
  cleanupLinkList: LinkModel[] = [];
  imageChangedEvent: any;
  imageSongChangedEvent: any;
  croppedImage: any;
  blobFile: any;
  blobSongFile: any;

  constructor(
    private artistService: ArtistService,
    private linkService: LinkService,
    private musicService: MusicService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router,
    private snackBar: SnackBarService,
    private dialog: MatDialog,
    private sanitizer: DomSanitizer
  ) { }

  async ngOnInit(): Promise<void> {
    this.route.params.subscribe(params => { this.artistService.getById(params['id']).subscribe(artist => this.artist = artist); });

    this.currentUser = this.authService.currentUserValue;

    await this.delay(200);
    this.checkEmpty = this.checkIfEmpty();
  }

  delay(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

  checkIfEmpty() {
    if (this.artist.songs.length <= 0) {
      return true;
    }
    return false;
  }

  editModeChange() {
    if (this.editMode) {
      this.editMode = false;
      this.pictureChanged = false;
    }
    else {
      this.editMode = true;
    }
  }

  onSongFileSelected(event: any): void {
    const file = event.target.files[0];
    this.song.songFile = file;
  }

  onPictureFileSelected(event: any): void {
    const file = event.target.files[0];
    this.song.pictureFile = file;
  }

  chosenLink(chosenLink: LinkModel) {
    this.linkModal = true;
    this.link = chosenLink;
  }

  chosenSong(chosenSong: MusicModel) {
    this.songModal = true;
    this.song = chosenSong;
  }

  async resetLink() {
    await this.delay(150);
    this.linkModal = false;
    this.link = resetLink();
  }

  uploadImage() {
    if (this.blobFile) {
      this.pictureChanged = true;
    }
  }

  cleanupLinks() {
    const commonLinks: LinkModel[] = [];
    const differingLinks: LinkModel[] = [];

    this.cleanupLinkList.forEach(link => {
      const linkFound = this.artist.links.find(x => x.id === link.id);
      if (linkFound) {
        commonLinks.push(link);
      } else {
        differingLinks.push(link);
      }
    });

    differingLinks.forEach(link => {

      this.linkService.delete(link.id).subscribe(x => {

      });
    })

  }

  delete(): void {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        title: "Delete Artist Profile", message: "Are you sure you want to delete your artist profile?",
        confirmYes: 'Confirm',
        confirmNo: 'Cancel'
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.artistService.delete(this.artist.id).subscribe(x => {
          this.router.navigate(['/upcoming']);
        });
        this.snackBar.openSnackBar('Deletion successful.', '', 'success');
      } else {
        this.snackBar.openSnackBar('Deletion canceled.', '', 'warning');
      }
    });
  }

  async cancel(): Promise<void> {
    if (this.linksChanged) {
      this.cleanupLinkList = this.artist.links;
    }
    this.artistService.getById(this.artist.id).subscribe(artist => this.artist = artist);
    this.snackBar.openSnackBar('Artist updating has been canceled.', '', 'info');
    this.editModeChange();
    await this.delay(200);
    if (this.cleanupLinkList) {
      this.cleanupLinks();
    }


  }

  save(): void {
    this.message = "";
    if (this.blobFile) {
      this.artist.pictureFile = this.blobFile;
    }
    if (this.artist.user) {
      this.artist.userId = this.artist.user?.id;
    }
    //update
    this.artistService.update(this.artist.id, this.artist)
      .subscribe({
        error: (err) => {
          this.message = Object.values(err.error.errors).join(", ");
          this.snackBar.openSnackBar(this.message, '', 'error');
        },
        complete: () => {
          this.artistService.getById(this.artist.id).subscribe(artist => this.artist = artist);
          this.editModeChange();
          this.resetFileChangeEvent();
          this.snackBar.openSnackBar("Your Artist page is updated", '', 'success');
        }
      });
  }

  // Link Resources

  saveLink(link: LinkModel): void {
    this.message = "";

    if (link.id == 0) {
      //create
      this.linkService.create(link)
        .subscribe({
          next: (x) => {
            this.artist.links = this.artist.links.filter(existingLink => existingLink.id !== link.id);
            this.artist.links.push(x);
          },
          error: (err) => {
            console.log(err);
            this.message = Object.values(err.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message, '', 'error');
          }
        });
    } else {
      //update
      this.linkService.update(link.id, link)
        .subscribe({
          error: (err) => {
            this.message = Object.values(err.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message, '', 'error');
          },
          complete: () => {
          }
        });
    }

    this.resetLink();
    this.linksChanged = true
  }


  deleteLink(link: LinkModel): void {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        title: "Delete Link", message: "Are you sure you want to delete this link? It will be permanent",
        confirmYes: 'Confirm',
        confirmNo: 'Cancel'
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.linkService.delete(link.id).subscribe(x => {
          this.artist.links = this.artist.links.filter(x => x.id != link.id);
          this.resetLink();
        });
        this.snackBar.openSnackBar('Deletion successful.', '', 'success');
      } else {
        this.snackBar.openSnackBar('Deletion canceled.', '', 'warning');
      }
    });
  }

  // Song Resources

  editSong(music: MusicModel): void {
    console.log(music)
    Object.assign(this.song, music);
  }

  deleteSong(music: MusicModel): void {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        title: "Delete Song", message: "Are you sure you want to delete this song? It will be permanent",
        confirmYes: 'Confirm',
        confirmNo: 'Cancel'
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.musicService.delete(music.id).subscribe(x => {
          this.artist.songs = this.artist.songs.filter(x => x.id != music.id);
        });
        this.snackBar.openSnackBar('Deletion successful.', '', 'success');
      } else {
        this.snackBar.openSnackBar('Deletion canceled.', '', 'warning');
      }
    });
  }

  cancelSong(): void {
    this.song = resetMusic();
    this.snackBar.openSnackBar('Song creation canceled.', '', 'info');
  }

  saveSong(): void {
    this.message = "";
    this.song.artistIds = [this.artist.id];
    this.song.pictureFile = this.blobSongFile;
    if (this.song.id == 0) {
      //create
      this.musicService.create(this.song)
        .subscribe({
          next: (x) => {
            this.artist.songs = this.artist.songs.filter(existingSong => existingSong.id !== this.song.id);
            this.artist.songs.push(x);
            this.song = resetMusic();
            this.resetFileSongChangeEvent();
            this.snackBar.openSnackBar("Song created", '', 'success');
          },
          error: (err) => {
            console.log(err);
            this.message = Object.values(err.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message, '', 'error');
          }
        });
    } else {
      //update
      this.musicService.update(this.song)
        .subscribe({
          error: (err) => {
            this.message = Object.values(err.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message, '', 'error');
          },
          complete: () => {
            this.musicService.getAll().subscribe(x => this.artist.songs = x);
            this.song = resetMusic();
            this.resetFileSongChangeEvent();
            this.snackBar.openSnackBar("Song updated", '', 'success');
          }
        });
    }
    this.song = resetMusic();
  }

  fileChangeEvent(event: any): void {
    this.imageChangedEvent = event;
  }
  fileSongChangeEvent(event: any): void {
    this.imageSongChangedEvent = event;
  }
  resetFileChangeEvent(): void {
    this.imageChangedEvent = null;
  }
  resetFileSongChangeEvent(): void {
    this.imageSongChangedEvent = null;
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
  imageSongCropped(event: ImageCroppedEvent) {
    if (event.objectUrl) {
      this.croppedImage = this.sanitizer.bypassSecurityTrustUrl(event.objectUrl);
      if (event.blob) {
        this.blobSongFile = new File([event.blob], this.imageSongChangedEvent.target.files[0].name, { type: 'image/png' });
      }
    }

    // event.blob can be used to upload the cropped image
  }
  imageLoaded(image: LoadedImage) {
    // show cropper
  }
  imageSongLoaded(image: LoadedImage) {
    // show cropper
  }
  cropperReady() {
    // cropper ready
  }
  cropperSongReady() {
    // cropper ready
  }
  loadImageFailed() {
    // show message
  }
}
