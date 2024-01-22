import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LinkModel } from '../Models/LinkModel';

@Injectable({
  providedIn: 'root'
})
export class LinkService {
  private readonly url = environment.apiUrl + 'Link';

  constructor(private http: HttpClient) { }

  public getAll(): Observable<LinkModel[]> {
    return this.http.get<LinkModel[]>(this.url);
  }

  public create(link: LinkModel): Observable<LinkModel> {
    const formData = new FormData();
  
    formData.append('title', link.title);
    formData.append('linkAddress', link.linkAddress);
    if (link.artistIds)
    {
      link.artistIds.forEach(artistId => {
        formData.append('artistIds', artistId.toString());
      });
    }
    else if (link.artist)
    {
      link.artist.forEach(artistId => {
        formData.append('artistIds', artistId.id.toString());
      });
    }

    return this.http.post<LinkModel>(this.url + '/create', formData);
  }

  public update(linkId:number, link: LinkModel): Observable<LinkModel> {
    const formData = new FormData();
  
    formData.append('title', link.title);
    formData.append('linkAddress', link.linkAddress);
    if (link.artistIds)
    {
      link.artistIds.forEach(artistId => {
        formData.append('artistIds', artistId.toString());
      });
    }
    else if (link.artist)
    {
      link.artist.forEach(artistId => {
        formData.append('artistIds', artistId.id.toString());
      });
    }

    return this.http.put<LinkModel>(this.url + '/' + linkId, formData);
  }
  
  public delete(linkId: number): Observable<LinkModel> {
    return this.http.delete<LinkModel>(this.url + '/' + linkId);
  }

  public getById(linkId: number): Observable<LinkModel> { 
    return this.http.get<LinkModel>(this.url + '/' + linkId);
  }
}


