import { ArtistModel } from "./ArtistModel";

export interface MusicModel {
    id: number;
    songName: string;
    artist: ArtistModel[];
    artistIds: number[];
    album: string;
    songFile: File | null;
    songFilePath: string;
    pictureFile: File | null;
    songPicturePath: string;
}

export function resetMusic() {
    return { 
      id: 0, 
      songName: '', 
      artist: [],
      artistIds: [], 
      album: '', 
      songFile: null,
      songFilePath: '',
      pictureFile: null,
      songPicturePath: '', 
  };
  }