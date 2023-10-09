import { Role, constRoles } from "./role";

export interface User {
  id: number;
  firstname: string;
  lastname: string;
  email: string;
  password: string;
  phoneNumber: number;
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
    password: '', 
    phoneNumber: 0, 
    address: '', 
    city: '', 
    postal: 0, 
    country: '',
    role: constRoles[0]
};
}