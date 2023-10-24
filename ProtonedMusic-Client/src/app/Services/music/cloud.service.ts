import { Injectable } from '@angular/core';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CloudService {
  files: any = [
    { url: 'https://ia801504.us.archive.org/3/items/EdSheeranPerfectOfficialMusicVideoListenVid.com/Ed_Sheeran_-_Perfect_Official_Music_Video%5BListenVid.com%5D.mp3', songName: 'Perfect', artist: ' Ed Sheeran', album: 'Perfect'},
    { url: '../../../assets/music/audio.mp3', songName: 'Chatter', artist: 'Connor Price', album: 'Around the world'},
    { url: '../../../assets/music/Audio2.mp3', songName: 'FlipFlop', artist: 'Sigurd', album: 'Bjørn'}
  ];

  getFiles() {
   return of(this.files);
  }
}
