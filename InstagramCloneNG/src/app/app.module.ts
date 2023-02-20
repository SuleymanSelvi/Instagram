import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FollowComponent } from './components/follow/follow.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { PostsComponent } from './components/posts/posts.component';
import { HomeComponent } from './components/home/home.component';
import { PostDetailComponent } from './components/post-detail/post-detail.component';
import { PostCommentsComponent } from './components/post-comments/post-comments.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { UserPostsComponent } from './components/user-posts/user-posts.component';
import { DiscoverComponent } from './components/discover/discover.component';
import { UsersComponent } from './components/users/users.component';
import { FollowersComponent } from './components/followers/followers.component';
import { FollowingComponent } from './components/following/following.component';
import { SavedPostsComponent } from './components/saved-posts/saved-posts.component';
import { LikedPostsComponent } from './components/liked-posts/liked-posts.component';
import { HeaderComponent } from './components/header/header.component';
import { DirectComponent } from './components/direct/direct.component';
import { StorysComponent } from './components/storys/storys.component';

@NgModule({
  declarations: [
    AppComponent,
    FollowComponent,
    LoginComponent,
    RegisterComponent,
    PostsComponent,
    HomeComponent,
    PostDetailComponent,
    PostCommentsComponent,
    UserProfileComponent,
    UserPostsComponent,
    DiscoverComponent,
    UsersComponent,
    FollowersComponent,
    FollowingComponent,
    SavedPostsComponent,
    LikedPostsComponent,
    HeaderComponent,
    DirectComponent,
    StorysComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
