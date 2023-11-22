import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-testing',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './testing.component.html',
  styleUrls: ['./testing.component.css']
})
export class TestingComponent implements OnInit {
  audio = new Audio();

  constructor() { }

  ngOnInit(): void {
    this.playSound();
  }

  playSound() {
    this.audio.src = "../../assets/music/Lady.mp3";
    this.audio.load();
    this.audio.play();
  }

  pauseSound() {
    this.audio.src = "../../assets/music/Lady.mp3";
    this.audio.pause
  }

  startSound() {
    this.audio.src = "../../assets/music/Lady.mp3";
    this.audio.play();
  }

}
