import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LayoutComponent } from './../shared/layout/layout.component';
import { HomeComponent } from './home/home.component';
import { StartComponent } from './start/start.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: HomeComponent }
    ]
  },
  { path: 'start', component: StartComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RaffleRoutingModule { }
