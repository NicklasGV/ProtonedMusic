import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CalendarModel } from '../Models/CalendarModel';

@Injectable({
  providedIn: 'root'
})
export class CalendarService {
  private readonly url = environment.apiUrl + 'Calendar';

  constructor(private http: HttpClient) { }

  public getAllContent(): Observable<CalendarModel[]> {
    return this.http.get<CalendarModel[]>(this.url);
  }

  public create(calendarContent: CalendarModel): Observable<CalendarModel> {
    const formData = new FormData();
  
    formData.append('id', calendarContent.id.toString());
    formData.append('title', calendarContent.title);
    formData.append('content', calendarContent.content);
    formData.append('date', calendarContent.date);
    formData.append('artistId', calendarContent.artistId.toString());

    return this.http.post<CalendarModel>(this.url, formData);
  }

  public update(calendarId:number, calendarContent: CalendarModel): Observable<CalendarModel> {
    const formData = new FormData();
  
    formData.append('id', calendarContent.id.toString());
    formData.append('title', calendarContent.title);
    formData.append('content', calendarContent.content);
    formData.append('date', calendarContent.date);
    formData.append('artistId', calendarContent.artistId.toString());

    return this.http.put<CalendarModel>(this.url + '/' + calendarId, formData);
  }

  public deleteEvent(calendarId: number): Observable<CalendarModel> {
    return this.http.delete<CalendarModel>(this.url + '/' + calendarId);
  }

  public getEventById(calendarId: number): Observable<CalendarModel> { 
    return this.http.get<CalendarModel>(this.url + '/' + calendarId);
  }
}
