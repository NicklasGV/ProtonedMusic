import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FooterModel } from '../Models/FooterModel';

@Injectable({
  providedIn: 'root'
})
export class FooterService {
  private readonly url = environment.apiUrl + 'Footer';

  constructor(private http: HttpClient) { }

  public getAll(): Observable<FooterModel[]> {
    return this.http.get<FooterModel[]>(this.url);
  }

  public create(footer: FooterModel): Observable<FooterModel> {
    return this.http.post<FooterModel>(this.url, footer);
  }

  public update(footerId:number, footer: FooterModel): Observable<FooterModel> {
    return this.http.put<FooterModel>(this.url + '/' + footerId, footer);
  }
  
  public delete(footerId: number): Observable<FooterModel> {
    return this.http.delete<FooterModel>(this.url + '/' + footerId);
  }

  public getById(footerId: number): Observable<FooterModel> { 
    return this.http.get<FooterModel>(this.url + '/' + footerId);
  }
}
