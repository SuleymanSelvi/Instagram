import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DirectComponent } from './components/direct/direct.component';
import { DiscoverComponent } from './components/discover/discover.component';
import { FollowComponent } from './components/follow/follow.component';
import { FollowersComponent } from './components/followers/followers.component';
import { FollowingComponent } from './components/following/following.component';
import { HomeComponent } from './components/home/home.component';
import { LikedPostsComponent } from './components/liked-posts/liked-posts.component';
import { LoginComponent } from './components/login/login.component';
import { PostDetailComponent } from './components/post-detail/post-detail.component';
import { RegisterComponent } from './components/register/register.component';
import { SavedPostsComponent } from './components/saved-posts/saved-posts.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { UsersComponent } from './components/users/users.component';

const routes: Routes = [
  {
    path: "",
    title: "Instagram | Giriş Yap",
    component: LoginComponent
  },
  {
    path: "Register",
    title: "Instagram | Kayıt Ol",
    component: RegisterComponent
  },
  {
    path: "Home",
    component: HomeComponent
  },
  {
    path: "PostDetail/:id",
    component: PostDetailComponent
  },
  {
    path: "Profile/:id",
    component: UserProfileComponent
  },
  {
    path: "Discover",
    component: DiscoverComponent
  },
  {
    path: "Users/:id",
    component: UsersComponent
  },
  {
    path: "Following/:id",
    component: FollowingComponent
  },
  {
    path: "Followers/:id",
    component: FollowersComponent
  },
  {
    path: "Saved/:id",
    component: SavedPostsComponent
  },
  {
    path: "Liked/:id",
    component: LikedPostsComponent
  },
  {
    path: "Direct",
    component: DirectComponent
  },
  {
    path: "Account",
    loadChildren: () => import('./modules/account/account.module').then((m) => m.AccountModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes),],
  exports: [RouterModule]
})
export class AppRoutingModule { }
