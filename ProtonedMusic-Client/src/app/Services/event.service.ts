import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { EventModel } from '../Models/EventModel';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private readonly url = environment.apiUrl + 'Event';

  constructor(private http: HttpClient) { }

  public getAllEvents(): Observable<EventModel[]> {
    return this.http.get<EventModel[]>(this.url);
  }

  public createEvent(event: EventModel): Observable<EventModel> {
    const formData = new FormData();
  
    formData.append('title', event.title);
    formData.append('description', event.description);
    formData.append('price', event.price.toString());
    formData.append('eventPicturePath', event.eventPicturePath);
    formData.append('timeofEvent', event.timeofEvent);
    formData.append('dateofEvent', event.dateofEvent);
    formData.append('created', event.created.toISOString());

  
    if (event.pictureFile) {
      formData.append('pictureFile', event.pictureFile, event.pictureFile.name);
    }

    return this.http.post<EventModel>(this.url, formData);
  }

  public updateEvent(eventId:number, event: EventModel): Observable<EventModel> {
    const formData = new FormData();
  
    formData.append('title', event.title);
    formData.append('description', event.description);
    formData.append('price', event.price.toString());
    formData.append('eventPicturePath', event.eventPicturePath);
    formData.append('timeofEvent', event.timeofEvent);
    formData.append('dateofEvent', event.dateofEvent);
    formData.append('created', event.created.toISOString());

  
    if (event.pictureFile) {
      formData.append('pictureFile', event.pictureFile, event.pictureFile.name);
    }

    return this.http.put<EventModel>(this.url + '/' + eventId, formData);
  }
  
  public deleteEvent (eventId: number): Observable<EventModel> {
    return this.http.delete<EventModel>(this.url + '/' + eventId);
  }

  public getEventById(eventId: number): Observable<EventModel> { 
    return this.http.get<EventModel>(this.url + '/' + eventId);
  }

  uploadProductPicture(eventId: number, file: FormData): Observable<EventModel> {
    return this.http.post<EventModel>(this.url + '/upload-event-picture/' + eventId, file);
  }
}
