import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FooterModel, resetFooter } from 'src/app/Models/FooterModel';
import { FooterService } from 'src/app/Services/footer.service';
import { SnackBarService } from 'src/app/Services/snack-bar.service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-footer-panel',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './footer-panel.component.html',
  styleUrl: './footer-panel.component.css'
})
export class FooterPanelComponent {
  message: string = '';
  footerpost: FooterModel[] = [];
  footer: FooterModel = resetFooter();
  formData = new FormData();

  constructor(private footerService: FooterService, private snackBar: SnackBarService, private router: Router) { }

  ngOnInit(): void {
    this.footerService.getById(1).subscribe({
      next: (x) => {
        this.footer = x;
      }});
  }

  refresh() {
    window.location.reload();
  }

  editFooter(footer: FooterModel): void {
    Object.assign(this.footer, footer);
  }

  cancel(): void {
    this.router.navigate(['/admin']);
    this.snackBar.openSnackBar('Footer creation canceled.', '','info');
  }

  save(): void {
    this.message = "";
    if (this.footer.id == 0) {
      //create
      this.footerService.create(this.footer)
      .subscribe({
        next: (x) => {
          this.footerpost.push(x);
          this.snackBar.openSnackBar("Footer created", '', 'success');
          this.refresh();
        },
        error: (err) => {
          console.log(err);
          this.message = Object.values(err.error.errors).join(", ");
          this.snackBar.openSnackBar(this.message, '', 'error');
          this.refresh();
        }
      });
    } else {
      //update
      this.footerService.update(this.footer.id, this.footer)
      .subscribe({
        error: (err) => {
          this.message = Object.values(err.error.errors).join(", ");
          this.snackBar.openSnackBar(this.message, '', 'error');
          this.refresh();
        },
        complete: () => {
          this.editFooter(this.footer);
          this.snackBar.openSnackBar("Footer updated", '', 'success');
          this.refresh();
        }
      });
    }
  }

}
