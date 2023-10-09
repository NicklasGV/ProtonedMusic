import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-panel',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './user-panel.component.html',
  styles: []
})
export class UserPanelComponent implements OnInit {
  /* message: string = "";
  accounts: User[] = [];
  account: User = resetUser();
  roles = Role;
  keys = Object.keys(this.roles); */
  
    constructor(/* private userService: UserService */) { }
  
    ngOnInit(): void {
      //this.accountService.getAll().subscribe(x => this.accounts = x);
    }
  
  
    /* editAccount(account: User): void {
      Object.assign(this.account, account);
    }
  
    cancel(): void {
      this.account = resetUser();
    }
  
    save(): void {
      this.message = "";
      if (this.account.id == 0) {
        //create
        this.accountService.createAccount(this.account)
        .subscribe({
          next: (x) => {
            this.accounts.push(x);
            this.account = resetUser();
          },
          error: (err) => {
            console.log(err);
            this.message = Object.values(err.error.errors).join(", ");
          }
        });
      } else {
        //update
        this.accountService.updateAccount(this.account.id, this.account)
        .subscribe({
          error: (err) => {
            this.message = Object.values(err.error.errors).join(", ");
          },
          complete: () => {
            this.accountService.getAll().subscribe(x => this.accounts = x);
            this.account = resetUser();
          }
        });
      }
      this.account = resetUser();
    } */
}
