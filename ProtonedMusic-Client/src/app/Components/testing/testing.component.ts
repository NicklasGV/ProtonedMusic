import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSliderModule } from '@angular/material/slider';
import { YouTubePlayerModule } from '@angular/youtube-player';

@Component({
  selector: 'app-testing',
  standalone: true,
  imports: [CommonModule, MatSliderModule, YouTubePlayerModule],
  templateUrl: './testing.component.html',
  styleUrls: ['./testing.component.css']
})
export class TestingComponent implements OnInit {
  audio = new Audio();
  volume: number = 50;
  apiLoaded = false;

  constructor() {}

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

  volumeChange(volume: number) {
    this.audio.volume = volume / 100;
  }
}
