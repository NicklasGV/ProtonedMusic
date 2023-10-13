import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ImageModel } from '../Models/ImageModel';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  public readonly url = environment.apiUrl + 'Image/';

  constructor(private http: HttpClient) { }

  add(image: any) {
    let formData = new FormData();
    formData.append("imageFile", image.imageFile);
    formData.append("ProductId", image.ProductId);
    return this.http.post<any>(this.url, formData)
  }

  get(id: string | number): Observable<ImageModel> {
    return this.http.get<ImageModel>(this.url + id);
  }

  getAll(): Observable<ImageModel[]>{
    return this.http.get<ImageModel[]>(this.url);
  }

  delete(id: number) {
    return this.http.delete<ImageModel>(this.url + "delete/" + id);
  }

  update(id: number) {
    return this.http.put<ImageModel>(this.url + id, Image);
  }

  toQueryString(obj: any) {
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
      if (value != null && value != undefined)
      parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }
    return parts.join('&');
  }
}
