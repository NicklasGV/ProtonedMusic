import { SignupComponent } from './Components/signup/signup.component';
import { MerchandiseComponent } from './Components/merchandise/merchandise.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
{path: '', loadComponent: () =>
import('./Components/homepage/homepage.component').then(it => it.HomepageComponent)},


  {path: '', loadComponent: () => 
  import('./Components/homepage/homepage.component').then(it => it.HomepageComponent)},

  {path: 'login', loadComponent: () =>
  import('./Components/login/login.component').then(it => it.LoginComponent)},

  {path: 'signup', loadComponent: () =>
  import('./Components/signup/signup.component').then(it => it.SignupComponent)},

  {path: 'merchandise', loadComponent: () =>
  import('./Components/merchandise/merchandise.component').then(it => it.MerchandiseComponent)},
  

  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
