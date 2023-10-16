import { NgModule } from '@angular/core';
import { RouterModule, Routes, CanActivate } from '@angular/router';
import { AuthGuard } from './Services/Guard/auth.guard';

const routes: Routes = [
  {path: '', loadComponent: () => 
  import('./Components/home/home.component').then(it => it.HomepageComponent)},

  {path: 'login', loadComponent: () =>
  import('./Components/login/login.component').then(it => it.LoginComponent)},

  {path: 'merchandise', loadComponent: () =>
  import('./Components/merchandise/merchandise.component').then(it => it.MerchandiseComponent)},

  {path: 'merchandiseProduct/:id', loadComponent: () =>
  import('./Components/merchandise-product/merchandise-product.component').then(it => it.MerchandiseProductComponent)},

  {path: 'profilmenu/:id', loadComponent: ()=> 
  import('./Components/Profile/profilmenu/profilmenu.component').then( it => it.ProfilmenuComponent)},

  {path: 'profilmenu/:id/editprofil', loadComponent: ()=> 
  import('./Components/Profile/editprofil/editprofil.component').then( it => it.EditprofilComponent)},
  
  {path: 'cart', loadComponent: () =>
  import('./Components/cart/cart.component').then(it => it.CartComponent)},

  {path: 'admin', loadComponent: () =>
  import('./Components/Admin/admin-panel/admin-panel.component').then(it => it.AdminPanelComponent), canActivate: [AuthGuard]},

  {path: 'admin/productpanel', loadComponent: () =>
  import('./Components/Admin/product-panel/product-panel.component').then(it => it.ProductPanelComponent), canActivate: [AuthGuard]},

  {path: 'admin/categorypanel', loadComponent: () =>
  import('./Components/Admin/category-panel/category-panel.component').then(it => it.CategoryPanelComponent), canActivate: [AuthGuard]},

  {path: 'admin/userpanel', loadComponent: () =>
  import('./Components/Admin/user-panel/user-panel.component').then(it => it.UserPanelComponent), canActivate: [AuthGuard]},

  {path: 'admin/imagepanel', loadComponent: () =>
  import('./Components/Admin/image-panel/image-panel.component').then(it => it.ImagePanelComponent), canActivate: [AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }