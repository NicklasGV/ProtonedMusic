import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IPGeolocationService {
  private apiUrl = 'http://ip-api.com/json';

  constructor(private http: HttpClient) { }

  getGeolocation() {
    return this.http.get<any>(`${this.apiUrl}`).pipe(
      tap((data) => console.log('Geolocation data:', data)),
      catchError((error) => {
        console.error('Error getting geolocation:', error);
        return throwError(error);
      })
    );
  }
}
