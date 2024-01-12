import { ArtistModel } from "./ArtistModel";

export interface LinkModel {
    id: number;
    artist: ArtistModel;
    artistId: number;
    title: string;
    linkAddress: string;
}

export function resetLink() {
    return { 
      id: 0,
      artist: null,
      artistId: 0, 
      title: '', 
      linkAddress: ''
  };
  }