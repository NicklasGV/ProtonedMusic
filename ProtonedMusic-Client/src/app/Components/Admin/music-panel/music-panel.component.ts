import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MusicModel, resetMusic } from 'src/app/Models/MusicModel';
import { MusicService } from 'src/app/Services/music/music.service';
import { FormsModule } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { DialogComponent } from 'src/app/Shared/dialog/dialog.component';
import { ArtistService } from 'src/app/Services/artist.service';
import { ArtistModel, resetArtist } from 'src/app/Models/ArtistModel';

@Component({
  selector: 'app-music-panel',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './music-panel.component.html',
  styleUrl: './music-panel.component.css'
})
export class MusicPanelComponent implements OnInit{
  message: string = "";
  songs: MusicModel[] = [];
  song: MusicModel = resetMusic();
  artist: ArtistModel = resetArtist();
  artists: ArtistModel[] = [];
  selected: number[] = [];
  selectedFile: File | undefined;
  formData = new FormData();

  constructor(private musicService: MusicService, private artistService: ArtistService, private snackBar: SnackBarService, private dialog: MatDialog) { }
    
    ngOnInit(): void {
      this.musicService.getAll().subscribe(x => this.songs = x);
      this.artistService.getAll().subscribe(x => this.artists = x);
      this.selected = this.song.artist.filter(x => x.checked == true ? x.id : null).map(x => x.id);
    }

    marked(event: any) {
      let value = parseInt(event.target.value);
      if (this.selected.indexOf(value) == -1) {
        this.selected.push(value);
      } else {
        this.selected.splice(this.selected.indexOf(value), 1);
      }
      this.selected.sort((a, b) => a - b);
    }
    resetCheckboxes(): void {
    this.artists.map(artist => artist.checked = false);
    this.selected.length = 0;
  }

    edit(music: MusicModel): void {
      this.resetCheckboxes();
      console.log(music)
      Object.assign(this.song, music);
      this.song.artist.forEach(art => {
        const existingArtist = this.artists.find(a => a.id === art.id);
        if (existingArtist) {
          existingArtist.checked = true;
          this.selected.push(existingArtist.id);
          console.log(this.selected);
        }
      });
    }

    onSongFileSelected(event: any): void {
      const file = event.target.files[0];
      this.song.songFile = file;
    }
  
    onPictureFileSelected(event: any): void {
      const file = event.target.files[0];
      this.song.pictureFile = file;
    }
    
    
    onFileSelected(event: any) {
      this.selectedFile = event.target.files[0];
    }
    
    uploadImage() {
      if (this.selectedFile) {
        const formData = new FormData();
        formData.append('file', this.selectedFile);
    
        this.musicService.uploadSongPicture(this.song.id, formData).subscribe(
          (song: MusicModel) => {
            this.musicService.getAll().subscribe(x => this.songs = x);
              this.song = resetMusic();
              this.snackBar.openSnackBar("Song Pic Updated", '', 'success');
          },
          (error) => {
            this.message = Object.values(error.error.errors).join(", ");
              this.snackBar.openSnackBar(this.message, '', 'error');
          }
        );
      }
    }
      
      delete(music: MusicModel): void {
        const dialogRef = this.dialog.open(DialogComponent, {
          data: { title: "Delete Song", message: "Are you sure you want to delete this song?",
          confirmYes: 'Confirm',
          confirmNo: 'Cancel' }
        });
    
        dialogRef.afterClosed().subscribe(result => {
          if (result) {
            this.musicService.delete(music.id).subscribe(x => {
              this.songs = this.songs.filter(x => x.id != music.id);
            });
            this.snackBar.openSnackBar('Deletion successful.', '','success');
          } else {
            this.snackBar.openSnackBar('Deletion canceled.', '','warning');
          }
        });
      }
    
      cancel(): void {
        this.song = resetMusic();
        this.resetCheckboxes();
        this.snackBar.openSnackBar('Song creation canceled.', '','info');
      }
    
      save(): void {
        this.message = "";
        this.song.artistIds = this.selected;
        if (this.song.id == 0) {
          //create
          this.musicService.create(this.song)
          .subscribe({
            next: (x) => {
              this.songs.push(x);
              this.song = resetMusic();
              this.resetCheckboxes();
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
              this.musicService.getAll().subscribe(x => this.songs = x);
              this.song = resetMusic();
              this.resetCheckboxes();
              this.snackBar.openSnackBar("Song updated", '', 'success');
            }
          });
        }
        this.song = resetMusic();
      }
}
