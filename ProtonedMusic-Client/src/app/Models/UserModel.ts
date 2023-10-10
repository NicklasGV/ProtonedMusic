import { Role, constRoles } from "./role";

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
  role?: Role;
  token?: string;
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
    role: constRoles[0]
};
}