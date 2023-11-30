import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from '../Models/UserModel';
import { AddonRoles } from '../Models/AddonRole';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly apiUrl = environment.apiUrl + 'User';
  constructor(private http: HttpClient) { }

  getAll(): Observable<User[]>{
    return this.http.get<User[]>(this.apiUrl);
  }

  delete(userId: number): Observable<User> {
    return this.http.delete<User>(this.apiUrl + '/' + userId);
  }

  create(user: User): Observable<User> {
    return this.http.post<User>(this.apiUrl + '/register', user);
  }

  subscribe(userMail: string): Observable<User> {
    return this.http.post<User>(this.apiUrl + '/Newsletter/Subscribe/' + userMail, userMail);
  }

  unsubscribe(userMail: string): Observable<User> {
    return this.http.post<User>(this.apiUrl + '/Newsletter/Unsubscribe/' + userMail, userMail);
  }

  update(user: User): Observable<User> {
    return this.http.put<User>(this.apiUrl + '/' + user.id, user);
  }

  findById(userId: number): Observable<User> {
    return this.http.get<User>(this.apiUrl + '/' + userId);
  }

  findByEmail(userMail: string): Observable<User> {
    return this.http.get<User>(this.apiUrl + '/Email/' + userMail);
  }

  uploadProfilePicture(userId: number, file: FormData): Observable<User> {
    return this.http.post<User>(this.apiUrl + '/upload-profile-picture/' + userId, file);
  }
}