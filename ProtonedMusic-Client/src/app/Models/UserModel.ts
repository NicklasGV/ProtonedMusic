import { Role } from "./role";

export interface User {
  id: number;
  username: string;
  email: string;
  firstname: string;
  lastname: string;
  phonenumber: string;
  address: string;
  city: string;
  postal: string;
  country: string;
  role?: Role;
  token?: string;
}

export function resetUser() {
  return { 
    id: 0, 
    username: '', 
    email: '', 
    firstname: '', 
    lastname: '', 
    phonenumber: '', 
    address: '', 
    city: '', 
    postal: '', 
    country: '',
};
}