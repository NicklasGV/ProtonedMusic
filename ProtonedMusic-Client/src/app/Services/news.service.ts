import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NewsModel } from '../Models/NewsModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NewsService {
  private readonly url = environment.apiUrl + 'News';

  constructor(private http: HttpClient) { }

  public getAllNews(): Observable<NewsModel[]> {
    return this.http.get<NewsModel[]>(this.url);
  }

  public createNews(news: NewsModel): Observable<NewsModel> {
    return this.http.post<NewsModel>(this.url + '/create', news);
  }

  public updateNews(newsId:number, news: NewsModel): Observable<NewsModel> {
    return this.http.put<NewsModel>(this.url + '/' + newsId, news);
  }
  
  public deleteNews(newsId: number): Observable<NewsModel> {
    return this.http.delete<NewsModel>(this.url + '/' + newsId);
  }

  public getNewsById(newsId: number): Observable<NewsModel> { 
    return this.http.get<NewsModel>(this.url + '/' + newsId);
  }
}


