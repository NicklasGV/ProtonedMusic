import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArtistService } from 'src/app/Services/artist.service';
import { Router } from '@angular/router';
import { User, resetUser } from 'src/app/Models/UserModel';
import { ArtistModel, resetArtist } from 'src/app/Models/ArtistModel';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';



@Component({
  selector: 'app-artist-detailed',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './artist-detailed.component.html',
  styleUrl: './artist-detailed.component.css'
})
export class ArtistDetailedComponent implements OnInit {
  currentUser: User = resetUser();
  currentUserId: number = 0;
  artist: ArtistModel = resetArtist();
  checkEmpty: boolean = false;



  constructor( private artistService: ArtistService, private authService: AuthService , private route: ActivatedRoute) {}

  async ngOnInit(): Promise<void> {
    this.route.params.subscribe(params => {this.artistService.getById(params['id']).subscribe(artist => this.artist = artist);});
    
    this.currentUserId = this.authService.currentUserValue.id;

    await this.delay(200);
    this.checkEmpty = this.checkIfEmpty();
  }

  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
  }

  checkIfEmpty() {
    if (this.artist.songs.length <= 0)
    {
      return true;
    }
    return false;
  }
}
