import { Component, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { UserService } from 'src/app/Services/user.service';
import { User, resetUser } from 'src/app/Models/UserModel';
import { FormControl, FormGroup, FormsModule } from '@angular/forms';
import { EmailModel, resetEmail } from 'src/app/Models/EmailModel';
import { EmailService } from 'src/app/Services/email.service';
import { SnackBarService } from 'src/app/Services/snack-bar.service';


@Component({
  selector: 'app-mailsender',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './mailsender.component.html',
  styleUrls: ['./mailsender.component.css'],
})
export class MailsenderComponent implements OnInit {
  users: User[] = [];
  user: User = resetUser();
  mails: EmailModel[] = [];
  mail: EmailModel = resetEmail();
  message: string = '';
  sendToAll: boolean = false;
  selected: string[] = [];
  date: any = new Date();
  formGroup: FormGroup | undefined;
  footerContent = '<br><br><br><br>' + '<footer><a style="font-size: smaller;" href="https://protonedmusic.com/#/unsubscribe">Unsubscribe</a></footer>';

  constructor(private userService: UserService, private mailService: EmailService, private snackBar: SnackBarService, private datePipe: DatePipe) { }

  ngOnInit(): void {
    this.userService.getAll().subscribe(x => this.users = x);

    this.formGroup = new FormGroup({
      text: new FormControl()
  });
  }

  transformDate(date: any) {
    return this.datePipe.transform(date, 'MMMM of yyyy');
  }

  getNewsletterUsers(): any[] {
    return this.users.filter(user => user.addonRoles == 'Newsletter');
  }

  cancel(): void {
    this.mail = resetEmail();
    this.snackBar.openSnackBar('Mail canceled.', '', 'info');
  }

  sendTo(user: User): void {
    this.mail.to = user.email;
  }

  send(): void {
    this.message = '';
    if (this.sendToAll) {
      this.users.forEach((user) => {
        if (user.addonRoles === 'Newsletter' && user.email) {
          this.mail.to = user.email;
          this.mail.subject = "Newsletter for " + this.transformDate(this.date);
          if (!this.mail.body.endsWith(this.footerContent))
          {
            this.mail.body += this.footerContent
          }
          this.mailService.sendEmail(this.mail).subscribe({
            next: (x) => {
              this.mails.push(x);
              this.snackBar.openSnackBar('Mail sent', '', 'success');
            },
            error: (err) => {
              console.log(err);
              this.message = Object.values(err.error.errors).join(', ');
              this.snackBar.openSnackBar(this.message, '', 'error');
            },
          });
        }
      });
    } else {
      this.mailService.sendEmail(this.mail).subscribe({
        next: (x) => {
          this.mails.push(x);
          this.snackBar.openSnackBar('Mail sent', '', 'success');
        },
        error: (err) => {
          console.log(err);
          this.message = Object.values(err.error.errors).join(', ');
          this.snackBar.openSnackBar(this.message, '', 'error');
        },
      });
    }
    this.mail = resetEmail();
  }
  
  
}
