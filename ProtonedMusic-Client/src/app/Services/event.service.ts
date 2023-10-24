import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { EventModel } from '../Models/EventModel';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private readonly url = environment.apiUrl + 'Events';

  constructor(private http: HttpClient) { }

  public getAllEvents(): Observable<EventModel[]> {
    return this.http.get<EventModel[]>(this.url);
  }

  public createEvent(event: EventModel): Observable<EventModel> {
    return this.http.post<EventModel>(this.url, event);
  }

  public updateEvent(eventId:number, event: EventModel): Observable<EventModel> {
    return this.http.put<EventModel>(this.url + '/' + eventId, event);
  }
  
  public deleteEvent (eventId: number): Observable<EventModel> {
    return this.http.delete<EventModel>(this.url + '/' + eventId);
  }

  public getEventById(eventId: number): Observable<EventModel> { 
    return this.http.get<EventModel>(this.url + '/' + eventId);
  }
}
