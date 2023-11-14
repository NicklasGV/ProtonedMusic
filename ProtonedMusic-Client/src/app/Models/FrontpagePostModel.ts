import { NewsModel } from "./NewsModel";

export interface FrontpagePost {
  id: number;
  text: string;
  frontpagePicturePath: string;
  banner?: string;
}

export function resetFrontpage() {
  return { 
    id: 0, 
    text: '', 
    frontpagePicturePath: '',
};
}