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
    return this.http.post<MusicModel>(this.apiUrl + '/create', music);
  }

  update(music: MusicModel): Observable<MusicModel> {
    return this.http.put<MusicModel>(this.apiUrl + '/' + music.id, music);
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