import { Role } from "./role";

export interface User {
  id: number;
  firstname: string;
  lastname: string;
  email: string;
  password: string;
  phonenumber: number;
  address: string;
  city: string;
  postal: number;
  country: string;
  role?: Role;
  token?: string;
}

export function resetUser() {
  return { 
    id: 0, 
    firstname: '', 
    lastname: '', 
    email: '', 
    password: 0, 
    phonenumber: '', 
    address: '', 
    city: '', 
    postal: 0, 
    country: '',
};
}