import { HostComponent } from './host/host.component';
import { ClientComponent } from './client/client.component';
import { AuthGuard } from '../services/auth/auth.guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RoleGuard } from '../services/auth/role.guard';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {
      allowRole: ['Admin']
    }
  },
  {
    path: 'host',
    component: HostComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {
      allowRole: ['Admin', 'Host']
    }
  },
  {
    path: 'client',
    component: ClientComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {
      allowRole: ['Admin', 'Client']
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StartRoutingModule { }
