import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ImageModel } from '../Models/ImageModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ImageService {
  private readonly url = environment.apiUrl + 'Image';
  imagePath: ImageModel[] = [];

  constructor(private http: HttpClient) {}

  public getImages(): Observable<ImageModel[]> {
    return this.http.get<ImageModel[]>(this.url);
  }
}
