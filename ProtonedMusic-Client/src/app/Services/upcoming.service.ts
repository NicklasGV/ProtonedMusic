import { UpcomingModel } from './../Models/UpcomingModel';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UpcomingService {
  private readonly url = environment.apiUrl + 'Upcoming';

  constructor(private http: HttpClient) { }

  public getAllUpcomings(): Observable<UpcomingModel[]> {
    return this.http.get<UpcomingModel[]>(this.url);
  }

  public createUpcoming(upcoming: UpcomingModel): Observable<UpcomingModel> {
    return this.http.post<UpcomingModel>(this.url, upcoming);
  }

  public updateUpcoming(upcomingId:number, upcoming: UpcomingModel): Observable<UpcomingModel> {
    return this.http.put<UpcomingModel>(this.url + '/' + upcomingId, upcoming);
  }
  
  public deleteUpcoming (upcomingId: number): Observable<UpcomingModel> {
    return this.http.delete<UpcomingModel>(this.url + '/' + upcomingId);
  }

  public getUpcomingById(upcomingId: number): Observable<UpcomingModel> { 
    return this.http.get<UpcomingModel>(this.url + '/' + upcomingId);
  }
}
