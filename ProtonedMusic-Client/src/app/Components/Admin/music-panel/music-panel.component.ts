import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MusicModel, resetMusic } from 'src/app/Models/MusicModel';
import { MusicService } from 'src/app/Services/music/music.service';
import { FormsModule } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { DialogComponent } from 'src/app/Shared/dialog/dialog.component';

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
  selectedFile: File | undefined;
  formData = new FormData();

  constructor(private musicService: MusicService, private snackBar: SnackBarService, private dialog: MatDialog) { }
    
    ngOnInit(): void {
      this.musicService.getAll().subscribe(x => this.songs = x);

    }

    edit(music: MusicModel): void {
      Object.assign(this.song, music);
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
          data: { title: "Delete Song", message: "Are you sure you want to delete this song?" }
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
        this.snackBar.openSnackBar('Song creation canceled.', '','info');
      }
    
      save(): void {
        this.message = "";
        if (this.song.id == 0) {
          //create
          console.log(this.song)
          this.musicService.create(this.song)
          .subscribe({
            next: (x) => {
              this.songs.push(x);
              this.song = resetMusic();
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
              this.snackBar.openSnackBar("Song updated", '', 'success');
            }
          });
        }
        this.song = resetMusic();
      }
}
