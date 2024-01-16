import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './Services/Guard/auth.guard';
import { FamilyGuard } from './Services/Guard/family.guard';

const routes: Routes = [
  {path: '', loadComponent: () =>
  import('./Components/home/home.component').then(it => it.HomepageComponent)},

//#region Navbar
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
//#endregion

  //WIP
  {path: 'order/success', loadComponent: () =>
  import('./Components/ordersuccess/ordersuccess.component').then(it => it.OrdersuccessComponent)},

  {path: 'merchandiseProduct/:id', loadComponent: () =>
  import('./Components/merchandise-product/merchandise-product.component').then(it => it.MerchandiseProductComponent)},

  {path: 'newsDetailed/:id', loadComponent: () =>
  import('./Components/news-detailed/news-detailed.component').then(it => it.NewsDetailedComponent)},

  {path: 'artist/:id', loadComponent: () =>
  import('./Components/artist-detailed/artist-detailed.component').then(it => it.ArtistDetailedComponent)},


  {path: 'family', loadComponent: () =>
  import('./Components/Family/family-panel/family-panel.component').then(it => it.FamilyPanelComponent), canActivate: [FamilyGuard]},

  {path: 'family/familyschedule', loadComponent: () =>
  import('./Components/Family/family-schedule/family-schedule.component').then(it => it.FamilyScheduleComponent), canActivate: [FamilyGuard]},

//#region Profile
  {path: 'profilmenu/:id', loadComponent: ()=>
  import('./Components/Profile/profilmenu/profilmenu.component').then( it => it.ProfilmenuComponent)},

  {path: 'profilmenu/:id/editprofil', loadComponent: ()=>
  import('./Components/Profile/editprofile/editprofile.component').then( it => it.EditprofilComponent)},

  {path: 'profilmenu/:id/orderhistory', loadComponent: ()=>
  import('./Components/Profile/orderHistory/order-history/order-history.component').then( it => it.OrderHistoryComponent)},
//#endregion

//#region Admin interface
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

  {path: 'admin/footerpanel', loadComponent: () =>
  import('./Components/Admin/footer-panel/footer-panel.component').then(it => it.FooterPanelComponent), canActivate: [AuthGuard]},

  {path: 'admin/artistpanel', loadComponent: () =>
  import('./Components/Admin/artist-panel/artist-panel.component').then(it => it.ArtistPanelComponent), canActivate: [AuthGuard]},
  //#endregion

  //! Page not found component. must be at bottom
   {path: '**', loadComponent: () =>
   import ('./Components/page-not-found/page-not-found.component').then(it => it.PageNotFoundComponent)},

];

@NgModule({
  imports: [RouterModule.forRoot(routes, {useHash: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
