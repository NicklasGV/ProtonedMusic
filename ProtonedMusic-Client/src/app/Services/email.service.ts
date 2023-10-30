import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { EmailModel } from '../Models/EmailModel';

@Injectable({
  providedIn: 'root'
})
export class EmailService {

  private readonly url = environment.apiUrl + 'Email';

  constructor(private http: HttpClient) { }

  sendEmail(emailData: EmailModel) {
    return this.http.post<EmailModel>(this.url, emailData)
  }
}
