export interface MusicModel {
    id?: string;
    songName: string;
    artist: string;
    album: string;
    songFile: FormData;
    songFilePath: string;
    pictureFile: FormData;
    songPicturePath: string;
}

export function resetMusic() {
    return { 
      id: 0, 
      songName: '', 
      artist: '', 
      album: '', 
      songFile: new FormData(),
      songFilePath: '',
      pictureFile: new FormData(),
      songPicturePath: '', 
  };
  }