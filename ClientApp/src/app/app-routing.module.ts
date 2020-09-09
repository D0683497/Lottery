import { HomeComponent } from './shared/home/home.component';
import { LayoutComponent } from './shared/layout/layout.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: HomeComponent }
    ]
  },
  {
    path: 'account',
    loadChildren: () => import('./account/account.module').then(m => m.AccountModule)
  },
  {
    path: 'raffle',
    loadChildren: () => import('./raffle/raffle.module').then(m => m.RaffleModule)
  },
  {
    path: 'attendee',
    loadChildren: () => import('./attendee/attendee.module').then(m => m.AttendeeModule)
  },
  {
    path: 'winner',
    loadChildren: () => import('./winner/winner.module').then(m => m.WinnerModule)
  },
  {
    path: 'start',
    loadChildren: () => import('./start/start.module').then(m => m.StartModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
