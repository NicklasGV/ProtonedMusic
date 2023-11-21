import { MatLegacySliderModule as MatSliderModule } from '@angular/material/legacy-slider';
import { AfterViewInit, Component} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AudioService } from 'src/app/Services/music/audio.service';
import { CloudService } from 'src/app/Services/music/cloud.service';
import { AuthService } from 'src/app/Services/auth.service';
import { StreamState } from 'src/app/Models/stream-state';
import { MatToolbarModule } from '@angular/material/toolbar';

@Component({
  selector: 'app-musicplayer',
  standalone: true,
  imports: [CommonModule, FormsModule, MatSliderModule, MatToolbarModule],
  templateUrl: './musicplayer.component.html',
  styleUrls: ['./musicplayer.component.scss']
})
export class MusicplayerComponent{
  files: Array<any> = [];
  state?: StreamState;
  currentFile: any = {};
  currentSong: string = '';
  currentSongName: string = '';
  currentArtist: string = '';
  volume: number = 0.2;

  constructor(private audioService: AudioService, cloudService: CloudService, public auth: AuthService) {
    // get media files
    cloudService.getFiles().subscribe(files => {
      this.files = files;
    });

    // listen to stream state
    this.audioService.getState()
    .subscribe(state => {
      this.state = state;
    });
  }

  playStream(url: any) {
    this.audioService.playStream(url)
    .subscribe(events => {
    });
  }

  openFile(file: { url: any; songName: string; }, index: number) {
    this.currentFile = { index, file};
    this.audioService.stop();
    this.playStream(file.url);
    this.currentSongName = this.currentFile.file.songName;
    this.currentArtist = this.currentFile.file.artist;
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

  onSliderChangeEnd(change: { value: any; }) {
    this.audioService.seekTo(change.value);
  }

  volumeChange() {
    let audio = new Audio();
    audio.volume = this.volume;
  }
}
