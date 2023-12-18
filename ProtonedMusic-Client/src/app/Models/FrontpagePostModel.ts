import { NewsModel } from "./NewsModel";

export interface FrontpagePost {
  id: number;
  text: string;
  pictureFile: File | null;
  frontpagePicturePath: string;
  banner: string;
}

export function resetFrontpage() {
  return { 
    id: 0, 
    text: '',
    pictureFile: null,
    frontpagePicturePath: '',
    banner: 'LeftBanner'
};
}