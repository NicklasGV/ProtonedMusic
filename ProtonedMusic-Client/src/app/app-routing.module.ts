
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './Services/Guard/auth.guard';
import { FamilyGuard } from './Services/Guard/family.guard';

const routes: Routes = [
  {path: '', loadComponent: () =>
  import('./Components/home/home.component').then(it => it.HomepageComponent)},

  {path: 'login', loadComponent: () =>
  import('./Components/login/login.component').then(it => it.LoginComponent)},

  {path: 'music', loadComponent: () =>
  import('./Components/musicplayer/musicplayer.component').then(it => it.MusicplayerComponent)},

  {path: 'merchandise', loadComponent: () =>
  import('./Components/merchandise/merchandise.component').then(it => it.MerchandiseComponent)},

  {path: 'upcoming', loadComponent: () =>
  import('./Components/upcoming/upcoming.component').then(it => it.UpcomingComponent)},

  {path: 'news', loadComponent: () =>
  import('./Components/news/news.component').then(it => it.NewsComponent)},

  {path: 'cart', loadComponent: () =>
  import('./Components/cart/cart.component').then(it => it.CartComponent)},

  {path: 'unsubscribe', loadComponent: () =>
  import('./Components/unsubscribe/unsubscribe.component').then(it => it.UnsubscribeComponent)},

  {path: 'events', loadComponent: () =>
  import('./Components/events/events.component').then(it => it.EventsComponent)},

  {path: 'syke', loadComponent: () =>
  import('./Components/testing/testing.component').then(it => it.TestingComponent)},

  {path: 'events/:id', loadComponent: () =>
  import('./Components/event/event.component').then(it => it.EventComponent)},


  {path: 'merchandiseProduct/:id', loadComponent: () =>
  import('./Components/merchandise-product/merchandise-product.component').then(it => it.MerchandiseProductComponent)},

  {path: 'newsDetailed/:id', loadComponent: () =>
  import('./Components/news-detailed/news-detailed.component').then(it => it.NewsDetailedComponent)},

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

  {path: 'admin/emailpanel', loadComponent: () =>
  import('./Components/Admin/mailsender/mailsender.component').then(it => it.MailsenderComponent), canActivate: [AuthGuard]},

  {path: 'admin/eventpanel', loadComponent: () =>
  import('./Components/Admin/event-panel/event-panel.component').then(it => it.EventPanelComponent), canActivate: [AuthGuard]},

  {path: 'admin/newspanel', loadComponent: () =>
  import('./Components/Admin/news-panel/news-panel.component').then(it => it.NewsPanelComponent), canActivate: [AuthGuard]},

  {path: 'admin/upcomingpanel', loadComponent: () =>
  import('./Components/Admin/upcoming-panel/upcoming-panel.component').then(it => it.UpcomingPanelComponent), canActivate: [AuthGuard]},

  {path: 'admin/frontpagepostpanel', loadComponent: () =>
  import('./Components/Admin/frontpagepost-panel/frontpagepost-panel.component').then(it => it.FrontpagepostPanelComponent), canActivate: [AuthGuard]},

  {path: 'admin/musicpanel', loadComponent: () =>
  import('./Components/Admin/music-panel/music-panel.component').then(it => it.MusicPanelComponent), canActivate: [AuthGuard]},

];

@NgModule({
  imports: [RouterModule.forRoot(routes, {useHash: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
