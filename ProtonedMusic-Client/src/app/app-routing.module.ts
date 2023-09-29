import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: '', loadComponent: () => 
  import('./Components/home/home.component').then(it => it.HomepageComponent)},
  {path: 'login', loadComponent: () =>
  import('./Components/login/login.component').then(it => it.LoginComponent)},

  {path: 'signup', loadComponent: () =>
  import('./Components/signup/signup.component').then(it => it.SignupComponent)},

  {path: 'merchandise', loadComponent: () =>
  import('./Components/merchandise/merchandise.component').then(it => it.MerchandiseComponent)},
  //KOM NU BIG MAN
  {path: 'merchandiseProduct/:id', loadComponent: () =>
  import('./Components/merchandise-product/merchandise-product.component').then(it => it.MerchandiseProductComponent)},
  
  {path: 'cart', loadComponent: () =>
  import('./Components/cart/cart.component').then(it => it.CartComponent)},
  
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }