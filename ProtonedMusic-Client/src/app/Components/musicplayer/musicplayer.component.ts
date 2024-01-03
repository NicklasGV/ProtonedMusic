import { MatSliderModule } from '@angular/material/slider';
import * as i2 from '@angular/material/slider';
import { AfterViewInit, Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AudioService } from 'src/app/Services/music/audio.service';
import { AuthService } from 'src/app/Services/auth.service';
import { StreamState } from 'src/app/Models/stream-state';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MusicService } from 'src/app/Services/music/music.service';
import { MusicModel } from 'src/app/Models/MusicModel';

@Component({
  selector: 'app-musicplayer',
  standalone: true,
  imports: [CommonModule, FormsModule, MatSliderModule, MatToolbarModule],
  templateUrl: './musicplayer.component.html',
  styleUrls: ['./musicplayer.component.scss']
})
export class MusicplayerComponent implements OnInit{
  files: MusicModel[] = [];
  state?: StreamState;
  currentFile: any = {};
  currentSong: string = '';
  currentSongName: string = '';
  currentArtist: string[] = [];
  currentPicture: string = "../../../assets/img/1.png";
  volume: number = 50;
  checkEmpty: boolean = false;

  constructor(private audioService: AudioService, private musicService: MusicService, public auth: AuthService) { }
    
    async ngOnInit(): Promise<void> {
      this.musicService.getAll().subscribe(x => this.files = x);
      


    // listen to stream state
    this.audioService.getState().subscribe(state => {this.state = state;});

    await this.delay(200);
    this.checkEmpty = this.checkIfEmpty();
    }

    delay(ms: number) {
      return new Promise( resolve => setTimeout(resolve, ms) );
    }
  
    checkIfEmpty() {
      if (this.files.length <= 0)
      {
        return true;
      }
      return false;
    }

    

  playStream(url: any) {
    this.audioService.playStream(url)
    .subscribe(events => {
    });
  }

  openFile(file: MusicModel, index: number) {
    this.currentFile = { index, file};
    this.audioService.stop();
    this.playStream("https://protonedmusic.com/" + this.currentFile.file.songFilePath);
    this.currentSongName = this.currentFile.file.songName;
    this.currentArtist = this.currentFile.file.artist[0].name;
    if (this.currentFile.file.songPicturePath){
      this.currentPicture = this.currentFile.file.songPicturePath;
    }
    else{
      this.currentPicture = "../../../assets/img/1.png";
    }
    
  }

  pause() {
    this.audioService.pause();
  }

  play() {
    this.audioService.play();
  }

  stop() {
    this.audioService.stop();
  }

  next() {
    const index = this.currentFile.index + 1;
    const file = this.files[index];
    this.openFile(file, index);
  }

  previous() {
    const index = this.currentFile.index - 1;
    const file = this.files[index];
    this.openFile(file, index);
  }

  isFirstPlaying() {
    return this.currentFile.index === 0, this.currentSong;
  }

  isLastPlaying() {
    return this.currentFile.index === this.files.length - 1;
  }

  onSliderChangeEnd(change: any) {
    this.audioService.seekTo(change.value);
  }

  volumeChange(volume: number) {
    this.audioService.setVolume(volume);
  }
}
