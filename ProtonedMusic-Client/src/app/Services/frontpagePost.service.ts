import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { FrontpagePost } from '../Models/FrontpagePostModel';

@Injectable({
  providedIn: 'root'
})
export class FrontpagePostService {

  private readonly apiUrl = environment.apiUrl + 'FrontpagePost';
  constructor(private http: HttpClient) { }

  getAll(): Observable<FrontpagePost[]>{
    return this.http.get<FrontpagePost[]>(this.apiUrl);
  }

  delete(frontpagePostId: number): Observable<FrontpagePost> {
    return this.http.delete<FrontpagePost>(this.apiUrl + '/' + frontpagePostId);
  }

  create(frontpagePost: FrontpagePost): Observable<FrontpagePost> {
    const formData = new FormData();
  
    formData.append('text', frontpagePost.text);
    formData.append('frontpagePicturePath', frontpagePost.frontpagePicturePath);
    formData.append('banner', frontpagePost.banner);

  
    if (frontpagePost.pictureFile) {
      formData.append('pictureFile', frontpagePost.pictureFile, frontpagePost.pictureFile.name);
    }

    return this.http.post<FrontpagePost>(this.apiUrl + '/create', formData);
  }

  update(frontpagePost: FrontpagePost): Observable<FrontpagePost> {
    const formData = new FormData();
  
    formData.append('text', frontpagePost.text);
    formData.append('frontpagePicturePath', frontpagePost.frontpagePicturePath);
    formData.append('banner', frontpagePost.banner);

  
    if (frontpagePost.pictureFile) {
      formData.append('pictureFile', frontpagePost.pictureFile, frontpagePost.pictureFile.name);
    }

    return this.http.put<FrontpagePost>(this.apiUrl + '/' + frontpagePost.id, formData);
  }

  findById(frontpagePostId: number): Observable<FrontpagePost> {
    return this.http.get<FrontpagePost>(this.apiUrl + '/' + frontpagePostId);
  }

  uploadFrontpagePicture(frontpagePostId: number, file: FormData): Observable<FrontpagePost> {
    return this.http.post<FrontpagePost>(this.apiUrl + '/upload-frontpage-picture/' + frontpagePostId, file);
  }
}