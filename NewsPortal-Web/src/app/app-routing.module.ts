import { NgModule, ModuleWithProviders } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { CreateOrUpdateNewsFeedComponent } from './create-or-update-news-feed/create-or-update-news-feed.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'createnewsfeed', component: CreateOrUpdateNewsFeedComponent },
  { path: 'updatenewsfeed/:id', component: CreateOrUpdateNewsFeedComponent },
  { path: 'about', component: AboutComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
  exports: [RouterModule]
})
export class AppRoutingModule { }