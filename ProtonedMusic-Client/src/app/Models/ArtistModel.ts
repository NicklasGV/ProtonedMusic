import { MusicModel } from "./MusicModel";
import { User } from "./UserModel";

export interface ArtistModel {
    id: number;
    user: User;
    userId: number;
    name: string;
    info: string;
    pictureFile: File | null;
    songPicturePath: string;
    songs: MusicModel[];
    songIds: number[];
}

export function resetArtist() {
    return { 
      id: 0,
      user: null,
      userId: 0, 
      name: '', 
      info: '', 
      pictureFile: null,
      songPicturePath: '',
      songs: [],
      songIds: []
  };
  }