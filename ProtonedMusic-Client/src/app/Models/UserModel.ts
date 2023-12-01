import { AddonRoles } from './AddonRole';
import { NewsModel } from "./NewsModel";

export interface User {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  phoneNumber: number;
  address: string;
  city: string;
  postal: number;
  country: string;
  pictureFile: File | null;
  profilePicturePath: string;
  role?: string;
  addonRoles?: string;
  token?: string;
  newsLikes: NewsModel[];
  newsIds: number[];
}

export function resetUser() {
  return { 
    id: 0, 
    firstName: '', 
    lastName: '', 
    email: '', 
    password: '', 
    phoneNumber: 0, 
    address: '', 
    city: '', 
    postal: 0, 
    country: '',
    pictureFile: null,
    profilePicturePath: '',
    newsLikes: [],
    newsIds: []
};
}