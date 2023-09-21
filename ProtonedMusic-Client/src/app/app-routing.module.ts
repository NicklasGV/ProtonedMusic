import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: '', loadComponent: () => 
  import('./Components/homepage/homepage.component').then(it => it.HomepageComponent)},
  {path: 'login', loadComponent: () =>
  import('./Components/login/login.component').then(it => it.LoginComponent)},
  {path: 'signup', loadComponent: () =>
  import('./Components/signup/signup.component').then(it => it.SignupComponent)},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
