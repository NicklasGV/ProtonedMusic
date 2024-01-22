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



@Component({
  selector: 'app-artist-detailed',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './artist-detailed.component.html',
  styleUrl: './artist-detailed.component.css'
})
export class ArtistDetailedComponent implements OnInit {
  message: string = "";

  currentUser: User = resetUser();
  currentUserId: number = 0;
  artist: ArtistModel = resetArtist();
  link: LinkModel = resetLink();
  checkEmpty: boolean = false;
  editMode: boolean = false;
  pictureChanged: boolean = false;
  selectedFile: File | undefined;
  chosenLinkId: number = 0;




  constructor(
    private artistService: ArtistService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private snackBar: SnackBarService,
    private dialog: MatDialog
    ) {}

  async ngOnInit(): Promise<void> {
    this.route.params.subscribe(params => {this.artistService.getById(params['id']).subscribe(artist => this.artist = artist);});
    
    this.currentUserId = this.authService.currentUserValue.id;

    await this.delay(200);
    this.checkEmpty = this.checkIfEmpty();
  }

  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
  }

  checkIfEmpty() {
    if (this.artist.songs.length <= 0)
    {
      return true;
    }
    return false;
  }

  editModeChange() {
    if (this.editMode)
    {
      this.editMode = false;
    }
    else
    {
      this.editMode = true;
    }
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    this.artist.pictureFile = file;

  }

  chosenLink(chosenLink: LinkModel)
  {
    this.link = chosenLink;
  }
  
  resetLink()
  {
    this.link = resetLink();
  }



  uploadImage() {
    if(this.artist.pictureFile)
    {
      this.pictureChanged = true;
    }
  }
    
    delete(): void {
      const dialogRef = this.dialog.open(DialogComponent, {
        data: { title: "Delete Artist Profile", message: "Are you sure you want to delete your artist profile?" }
      });
  
      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.artistService.delete(this.artist.id).subscribe(x => {
            
          });
          this.snackBar.openSnackBar('Deletion successful.', '','success');
        } else {
          this.snackBar.openSnackBar('Deletion canceled.', '','warning');
        }
      });
    }
  
    cancel(): void {
      this.artistService.getById(this.artist.id).subscribe(artist => this.artist = artist);
      this.pictureChanged = false;
      this.snackBar.openSnackBar('Artist updating has been canceled.', '','info');
      this.editModeChange();
    }
  
    save(): void {
      this.message = "";
      if (this.artist.user)
      {
        this.artist.userId = this.artist.user?.id;
      }
      console.log(this.artist);
        //update
        this.artistService.update(this.artist.id, this.artist)
        .subscribe({
          error: (err) => {
            this.message = Object.values(err.error.errors).join(", ");
            this.snackBar.openSnackBar(this.message, '', 'error');
          },
          complete: () => {
            this.artistService.getById(this.artist.id).subscribe(artist => this.artist = artist);
            this.snackBar.openSnackBar("Artist updated", '', 'success');
            window.location.reload();
          }
        });   
    }
}
