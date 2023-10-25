import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './Services/Guard/auth.guard';

const routes: Routes = [
  {path: '', loadComponent: () => 
  import('./Components/home/home.component').then(it => it.HomepageComponent)},

  {path: 'login', loadComponent: () =>
  import('./Components/login/login.component').then(it => it.LoginComponent)},

  {path: 'music', loadComponent: () =>
  import('./Components/musicplayer/musicplayer.component').then(it => it.MusicplayerComponent)},

  {path: 'merchandise', loadComponent: () =>
  import('./Components/merchandise/merchandise.component').then(it => it.MerchandiseComponent)},

  {path: 'news', loadComponent: () =>
  import('./Components/news/news.component').then(it => it.NewsComponent)},

  {path: 'cart', loadComponent: () =>
  import('./Components/cart/cart.component').then(it => it.CartComponent)},

  {path: 'events', loadComponent: () =>
  import('./Components/events/events.component').then(it => it.EventsComponent)},

  {path: 'merchandiseProduct/:id', loadComponent: () =>
  import('./Components/merchandise-product/merchandise-product.component').then(it => it.MerchandiseProductComponent)},

  {path: 'profilmenu/:id', loadComponent: ()=> 
  import('./Components/Profile/profilmenu/profilmenu.component').then( it => it.ProfilmenuComponent)},

  {path: 'profilmenu/:id/editprofil', loadComponent: ()=> 
  import('./Components/Profile/editprofile/editprofile.component').then( it => it.EditprofilComponent)},

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

  {path: 'admin/eventpanel', loadComponent: () =>
  import('./Components/Admin/event-panel/event-panel.component').then(it => it.EventPanelComponent), canActivate: [AuthGuard]},

  {path: 'admin/newspanel', loadComponent: () =>
  import('./Components/Admin/news-panel/news-panel.component').then(it => it.NewsPanelComponent), canActivate: [AuthGuard]},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }