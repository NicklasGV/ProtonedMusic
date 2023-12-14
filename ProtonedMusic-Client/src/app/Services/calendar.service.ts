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

  public createEvent(contents: CalendarModel): Observable<CalendarModel> {
    return this.http.post<CalendarModel>(this.url + '/', contents);
  }

  public updateEvent(calendarId:number, contents: CalendarModel): Observable<CalendarModel> {
    return this.http.put<CalendarModel>(this.url + '/' + calendarId, contents);
  }
  
  public deleteEvent(calendarId: number): Observable<CalendarModel> {
    return this.http.delete<CalendarModel>(this.url + '/' + calendarId);
  }

  public getEventById(calendarId: number): Observable<CalendarModel> { 
    return this.http.get<CalendarModel>(this.url + '/' + calendarId);
  }
}
