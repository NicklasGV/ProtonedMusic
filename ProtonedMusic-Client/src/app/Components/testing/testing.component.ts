import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSliderModule } from '@angular/material/slider';
import { SliderModule } from 'primeng/slider';

@Component({
  selector: 'app-testing',
  standalone: true,
  imports: [CommonModule, MatSliderModule, SliderModule],
  templateUrl: './testing.component.html',
  styleUrls: ['./testing.component.css']
})
export class TestingComponent implements OnInit {
  audio = new Audio();
  volume: number = 50;

  constructor() {}

  ngOnInit(): void {
    this.playSound();
  }

  playSound() {
    this.audio.src = "../../assets/music/WHOS_THAT_BEAUTIFUL_GIRL.mp3";
    this.audio.load();
    this.audio.play();
  }

  pauseSound() {
    this.audio.src = "../../assets/music/WHOS_THAT_BEAUTIFUL_GIRL.mp3";
    this.audio.pause
  }

  startSound() {
    this.audio.src = "../../assets/music/WHOS_THAT_BEAUTIFUL_GIRL.mp3";
    this.audio.play();
  }

  volumeChange(volume: number) {
    this.audio.volume = volume / 100;
  }
}
