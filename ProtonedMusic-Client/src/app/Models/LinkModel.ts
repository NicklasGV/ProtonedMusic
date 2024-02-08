import { ArtistModel } from "./ArtistModel";

export interface LinkModel {
    id: number;
    artist: ArtistModel[] | null;
    artistIds: number[];
    title: string;
    linkAddress: string;
}

export function resetLink() {
    return { 
      id: 0,
      artist: null,
      artistIds: [], 
      title: '', 
      linkAddress: ''
  };
  }