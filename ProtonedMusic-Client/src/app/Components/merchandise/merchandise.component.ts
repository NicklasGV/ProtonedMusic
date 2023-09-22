import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-merchandise',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './merchandise.component.html',
  styleUrls: ['./merchandise.component.css']
})
export class MerchandiseComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
