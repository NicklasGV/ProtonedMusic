import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ArtistModel } from '../Models/ArtistModel';

@Injectable({
  providedIn: 'root'
})
export class ArtistService {
  private readonly url = environment.apiUrl + 'Artist';

  constructor(private http: HttpClient) { }

  public getAll(): Observable<ArtistModel[]> {
    return this.http.get<ArtistModel[]>(this.url);
  }

  public create(artist: ArtistModel): Observable<ArtistModel> {
    const formData = new FormData();
  
    formData.append('userId', artist.userId.toString());
    formData.append('name', artist.name);
    formData.append('info', artist.info);
    formData.append('picturePath', artist.picturePath);
    if (artist.songIds)
    {
      artist.songIds.forEach(songId => {
        formData.append('songIds', songId.toString());
      });
    }
    else if (artist.songs)
    {
      artist.songs.forEach(songId => {
        formData.append('songIds', songId.id.toString());
      });
    }
    if (artist.linkIds)
    {
      artist.linkIds.forEach(linkId => {
        formData.append('linksIds', linkId.toString());
      });
    }
    else if (artist.links)
    {
      artist.links.forEach(linkId => {
        formData.append('linksIds', linkId.id.toString());
      });
    }

  
    if (artist.pictureFile) {
      formData.append('pictureFile', artist.pictureFile, artist.pictureFile.name);
    }

    return this.http.post<ArtistModel>(this.url + '/register', formData);
  }

  public update(artistId:number, artist: ArtistModel): Observable<ArtistModel> {
    const formData = new FormData();
  
    formData.append('userId', artist.userId.toString());
    formData.append('name', artist.name);
    formData.append('info', artist.info);
    formData.append('picturePath', artist.picturePath);
    if (artist.songIds)
    {
      artist.songIds.forEach(songId => {
        formData.append('songIds', songId.toString());
      });
    }
    else if (artist.songs)
    {
      artist.songs.forEach(songId => {
        formData.append('songIds', songId.id.toString());
      });
    }
    if (artist.linkIds)
    {
      artist.linkIds.forEach(linkId => {
        formData.append('linksIds', linkId.toString());
      });
    }
    else if (artist.links)
    {
      artist.links.forEach(linkId => {
        formData.append('linksIds', linkId.id.toString());
      });
    }

  
    if (artist.pictureFile) {
      formData.append('pictureFile', artist.pictureFile, artist.pictureFile.name);
    }

    return this.http.put<ArtistModel>(this.url + '/' + artistId, formData);
  }
  
  public delete(artistId: number): Observable<ArtistModel> {
    return this.http.delete<ArtistModel>(this.url + '/' + artistId);
  }

  public getById(artistId: number): Observable<ArtistModel> { 
    return this.http.get<ArtistModel>(this.url + '/' + artistId);
  }

  uploadPicture(artistId: number, file: FormData): Observable<ArtistModel> {
    return this.http.post<ArtistModel>(this.url + '/upload-picture/' + artistId, file);
  }
}


