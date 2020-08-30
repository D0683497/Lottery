import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule)
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
    path: 'prize',
    loadChildren: () => import('./prize/prize.module').then(m => m.PrizeModule)
  },
  {
    path: 'attendee',
    loadChildren: () => import('./attendee/attendee.module').then(m => m.AttendeeModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
