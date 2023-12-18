export interface MusicModel {
    id: number;
    songName: string;
    artist: string;
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
      artist: '', 
      album: '', 
      songFile: null,
      songFilePath: '',
      pictureFile: null,
      songPicturePath: '', 
  };
  }