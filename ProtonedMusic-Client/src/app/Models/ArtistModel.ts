import { LinkModel } from "./LinkModel";
import { MusicModel } from "./MusicModel";
import { User } from "./UserModel";

export interface ArtistModel {
    id: number;
    user: User | null;
    userId: number;
    name: string;
    info: string;
    pictureFile: File | null;
    picturePath: string;
    songs: MusicModel[];
    songIds: number[];
    links: LinkModel[];
    linkIds: number[];
    checked: boolean;
}

export function resetArtist() {
    return { 
      id: 0,
      user: null,
      userId: 0, 
      name: '', 
      info: '', 
      pictureFile: null,
      picturePath: '',
      songs: [],
      songIds: [],
      links: [],
      linkIds: [],
      checked: false
  };
  }