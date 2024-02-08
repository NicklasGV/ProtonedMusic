import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from '../Models/UserModel';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly apiUrl = environment.apiUrl + 'User';
  constructor(private http: HttpClient, private authService: AuthService) { }

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
    if (user.phoneNumber != null) {
      formData.append('phoneNumber', user.phoneNumber.toString());
    }
    if (user.address != null) {
      formData.append('address', user.address);
    }
    if (user.city != null) {
      formData.append('city', user.city);
    }
    if (user.postal != null) {
      formData.append('postal', user.postal.toString()); 
    }
    if (user.country != null) {
      formData.append('country', user.country);
    }
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
    if (user.role != null) {
      formData.append('role', user.role); 
    }
    if (user.phoneNumber != null) {
      formData.append('phoneNumber', user.phoneNumber.toString());
    }
    if (user.address != null) {
      formData.append('address', user.address);
    }
    if (user.city != null) {
      formData.append('city', user.city);
    }
    if (user.postal != null) {
      formData.append('postal', user.postal.toString()); 
    }
    if (user.country != null) {
      formData.append('country', user.country);
    }
    if (user.profilePicturePath != null) {
      formData.append('profilePicturePath', user.profilePicturePath);
    }
    if (user.token) {
      formData.append('token', user.token);
    }
    if (user.pictureFile) {
      formData.append('pictureFile', user.pictureFile, user.pictureFile.name);
    }

    return this.http.put<User>(this.apiUrl + '/' + user.id, formData);
  }

  updateNoPassword(user: User): Observable<User> {
    const formData = new FormData();
    if (user.firstName != null)
    {
      formData.append('firstName', user.firstName);
    }
    if (user.lastName != null)
    {
      formData.append('lastName', user.lastName);
    }

    formData.append('email', user.email);
    
    if (user.role != null) {
      formData.append('role', user.role); 
    }
    if (user.addonRoles != null) {
      formData.append('addonRoles', user.addonRoles); 
    }
    if (user.phoneNumber != null) {
      formData.append('phoneNumber', user.phoneNumber.toString());
    }
    if (user.address != null) {
      formData.append('address', user.address);
    }
    if (user.city != null) {
      formData.append('city', user.city);
    }
    if (user.postal != null) {
      formData.append('postal', user.postal.toString()); 
    }
    if (user.country != null) {
      formData.append('country', user.country);
    }
    if (user.profilePicturePath != null) {
      formData.append('profilePicturePath', user.profilePicturePath);
    }
    if (user.token) {
      formData.append('token', user.token);
    }
    if (user.pictureFile) {
      formData.append('pictureFile', user.pictureFile, user.pictureFile.name);
    }
    
    return this.http.post<User>(this.apiUrl + '/Update/' + user.id, formData);
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

  removeProfilePicture(userId: number): Observable<User> {
    return this.http.delete<User>(this.apiUrl + '/remove-picture/' + userId);
  }
}