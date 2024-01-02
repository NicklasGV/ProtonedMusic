import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from '../Models/UserModel';

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
    const formData = new FormData();
  
    formData.append('firstName', user.firstName);
    formData.append('lastName', user.lastName);
    formData.append('email', user.email);
    formData.append('password', user.password);
    formData.append('phoneNumber', user.phoneNumber.toString());
    formData.append('address', user.address);
    formData.append('city', user.city);
    formData.append('postal', user.postal.toString());
    formData.append('country', user.country);
    if (user.profilePicturePath != null) {
      formData.append('profilePicturePath', user.profilePicturePath);
    }

    if (user.role) {
      formData.append('role', user.role);
    }

    if (user.addonRoles) {
      formData.append('addonRoles', user.addonRoles);
    }

    if (user.token) {
      formData.append('token', user.token);
    }
  
    if (user.pictureFile) {
      formData.append('pictureFile', user.pictureFile, user.pictureFile.name);
    }

    return this.http.post<User>(this.apiUrl + '/register', formData);
  }

  subscribe(userMail: string): Observable<User> {
    return this.http.post<User>(this.apiUrl + '/Newsletter/Subscribe/' + userMail, userMail);
  }

  unsubscribe(userMail: string): Observable<User> {
    return this.http.post<User>(this.apiUrl + '/Newsletter/Unsubscribe/' + userMail, userMail);
  }

  update(user: User): Observable<User> {
    const formData = new FormData();
  
    formData.append('firstName', user.firstName);
    formData.append('lastName', user.lastName);
    formData.append('email', user.email);
    formData.append('password', user.password);
    formData.append('phoneNumber', user.phoneNumber.toString());
    formData.append('address', user.address);
    formData.append('city', user.city);
    formData.append('postal', user.postal.toString());
    formData.append('country', user.country);
    formData.append('profilePicturePath', user.profilePicturePath);

    if (user.role) {
      formData.append('role', user.role);
    }

    if (user.addonRoles) {
      formData.append('addonRoles', user.addonRoles);
    }

    if (user.token) {
      formData.append('token', user.token);
    }
  
    if (user.pictureFile) {
      formData.append('pictureFile', user.pictureFile, user.pictureFile.name);
    }

    return this.http.put<User>(this.apiUrl + '/' + user.id, formData);
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