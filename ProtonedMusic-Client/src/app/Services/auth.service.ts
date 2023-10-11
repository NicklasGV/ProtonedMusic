import { EventEmitter, Injectable, Output} from '@angular/core';
import {HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User, resetUser } from '../Models/UserModel';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<User>;
  currentUser: Observable<User>;

  @Output() tt: EventEmitter<boolean> = new EventEmitter();

  constructor(private http: HttpClient) {
    // fake login useful when testing
    if (sessionStorage.getItem('currentUser') == null) {
      let user: User = resetUser();
      // user.role = Role.Admin;
      sessionStorage.setItem('currentUser', JSON.stringify(user));
    }
    this.currentUserSubject = new BehaviorSubject<User>(
      JSON.parse(sessionStorage.getItem('currentUser') as string)
    );
    this.currentUser = this.currentUserSubject.asObservable();
   }

   public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(email: string, password: string) {
    let authenticateUrl = `${environment.apiUrl}User/authenticate`;
    return this.http.post<any>(authenticateUrl, { "email": email, "password": password })
      .pipe(map(user => {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        sessionStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        return user;
      }));
  }

  logout() {
    // remove user from local storage to log user out
    sessionStorage.removeItem('currentUser');
    // reset CurrentUserSubject, by fetching the value in sessionStorage, which is null at this point
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(sessionStorage.getItem('currentUser') as string));
    // reset CurrentUser to the resat UserSubject, as an obserable
    this.currentUser = this.currentUserSubject.asObservable();
  }

  showpis(show: boolean){
    this.tt.emit(show);
  }

}
