import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { MusicModel } from 'src/app/Models/MusicModel';

@Injectable({
  providedIn: 'root'
})
export class MusicService {

  private readonly apiUrl = environment.apiUrl + 'Music';
  constructor(private http: HttpClient) { }

  getAll(): Observable<MusicModel[]>{
    return this.http.get<MusicModel[]>(this.apiUrl);
  }

  delete(musicId: number): Observable<MusicModel> {
    return this.http.delete<MusicModel>(this.apiUrl + '/' + musicId);
  }

  create(music: MusicModel): Observable<MusicModel> {
    const formData = new FormData();
  
    formData.append('songName', music.songName);
    formData.append('artist', music.artist);
    formData.append('album', music.album);
    formData.append('songFilePath', music.songFilePath);
    formData.append('songPicturePath', music.songPicturePath);

    if (music.songFile) {
      formData.append('songFile', music.songFile, music.songFile.name);
    }
  
    if (music.pictureFile) {
      formData.append('pictureFile', music.pictureFile, music.pictureFile.name);
    }
  
    return this.http.post<MusicModel>(this.apiUrl + '/create', formData);
  }

  update(music: MusicModel): Observable<MusicModel> {
    const formData = new FormData();
  
    formData.append('songName', music.songName);
    formData.append('artist', music.artist);
    formData.append('album', music.album);
    formData.append('songFilePath', music.songFilePath);
    formData.append('songPicturePath', music.songPicturePath);

    if (music.songFile) {
      formData.append('songFile', music.songFile, music.songFile.name);
    }
  
    if (music.pictureFile) {
      formData.append('pictureFile', music.pictureFile, music.pictureFile.name);
    }
    return this.http.put<MusicModel>(this.apiUrl + '/' + music.id, formData);
  }

  findById(musicId: number): Observable<MusicModel> {
    return this.http.get<MusicModel>(this.apiUrl + '/' + musicId);
  }

  uploadSong(musicId: number, song: FormData): Observable<MusicModel> {
    return this.http.post<MusicModel>(this.apiUrl + '/upload-song/' + musicId, song);
  }

  uploadSongPicture(musicId: number, file: FormData): Observable<MusicModel> {
    return this.http.post<MusicModel>(this.apiUrl + '/upload-song-picture/' + musicId, file);
  }
}