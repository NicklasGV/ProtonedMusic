import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: '', loadComponent: () => 
  import('./Components/home/home.component').then(it => it.HomepageComponent)},
  {path: 'login', loadComponent: () =>
  import('./Components/login/login.component').then(it => it.LoginComponent)},

  {path: 'merchandise', loadComponent: () =>
  import('./Components/merchandise/merchandise.component').then(it => it.MerchandiseComponent)},
  //KOM NU BIG MAN
  {path: 'merchandiseProduct/:id', loadComponent: () =>
  import('./Components/merchandise-product/merchandise-product.component').then(it => it.MerchandiseProductComponent)},
  
  {path: 'cart', loadComponent: () =>
  import('./Components/cart/cart.component').then(it => it.CartComponent)},

  {path: 'admin/productpanel', loadComponent: () =>
  import('./Components/Admin/product-panel/product-panel.component').then(it => it.ProductPanelComponent)},

  {path: 'admin/categorypanel', loadComponent: () =>
  import('./Components/Admin/category-panel/category-panel.component').then(it => it.CategoryPanelComponent)},

  {path: 'admin/userpanel', loadComponent: () =>
  import('./Components/Admin/user-panel/user-panel.component').then(it => it.UserPanelComponent)},
  
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }