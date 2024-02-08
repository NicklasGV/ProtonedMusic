import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArtistModel, resetArtist } from 'src/app/Models/ArtistModel';
import { ArtistService } from 'src/app/Services/artist.service';
import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { MatDialog } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { DialogComponent } from 'src/app/Shared/dialog/dialog.component';
import { User } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/Services/user.service';


@Component({
  selector: 'app-artist-panel',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './artist-panel.component.html',
  styleUrl: './artist-panel.component.css'
})
export class ArtistPanelComponent implements OnInit {
  message: string = "";
  artists: ArtistModel[] = [];
  artist: ArtistModel = resetArtist();
  users: User[] = [];
  selectedFile: File | undefined;
  formData = new FormData();

  constructor(private artistService: ArtistService, private userService: UserService, private snackBar: SnackBarService, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.artistService.getAll().subscribe(x => this.artists =x);
    this.userService.getAll().subscribe(users => {
      this.users = users.filter(user => user.role !== 'Customer');
    });

  }

  edit(artist: ArtistModel): void {
    Object.assign(this.artist, artist);
  }

  onPictureFileSelected(event: any): void {
    const file = event.target.files[0];
    this.artist.pictureFile = file;
  }
  
  
  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }
  
  uploadImage() {
    if (this.selectedFile) {
      const formData = new FormData();
      formData.append('file', this.selectedFile);
  
      this.artistService.uploadPicture(this.artist.id, formData).subscribe(
        (artist: ArtistModel) => {
          this.artistService.getAll().subscribe(x => this.artists = x);
            this.artist = resetArtist();
            this.snackBar.openSnackBar("Artist Pic Updated", '', 'success');
        },
        (error) => {
          this.message = Object.values(error.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message, '', 'error');
        }
      );
    }
  }
    
    delete(artist: ArtistModel): void {
      const dialogRef = this.dialog.open(DialogComponent, {
        data: { title: "Delete Artist", 
        message: "Are you sure you want to delete this artist?",
        confirmYes: 'Confirm',
        confirmNo: 'Cancel'
      }
      });
  
      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.artistService.delete(artist.id).subscribe(x => {
            this.artists = this.artists.filter(x => x.id != artist.id);
          });
          this.snackBar.openSnackBar('Deletion successful.', '','success');
        } else {
          this.snackBar.openSnackBar('Deletion canceled.', '','warning');
        }
      });
    }
  
    cancel(): void {
      this.artist = resetArtist();
      this.snackBar.openSnackBar('Artist creation canceled.', '','info');
    }
  
    save(): void {
      this.message = "";
      if (this.artist.user)
      {
        this.artist.userId = this.artist.user?.id;
      }
      console.log(this.artist);
      if (this.artist.id == 0) {
        //create
        this.artistService.create(this.artist)
        .subscribe({
          next: (x) => {
            this.artists.push(x);
            this.artist = resetArtist();
            this.snackBar.openSnackBar("Artist created", '', 'success');
          },
          error: (err) => {
            console.log(err);
            this.message = Object.values(err.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message, '', 'error');
          }
        });
      } else {
        //update
        this.artistService.update(this.artist.id, this.artist)
        .subscribe({
          error: (err) => {
            this.message = Object.values(err.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message, '', 'error');
          },
          complete: () => {
            this.artistService.getAll().subscribe(x => this.artists = x);
            this.artist = resetArtist();
            this.snackBar.openSnackBar("Artist updated", '', 'success');
          }
        });
      }
      this.artist = resetArtist();
    }
}
