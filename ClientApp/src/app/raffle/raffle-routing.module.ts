import { WinnerComponent } from './winner/winner.component';
import { AttendeeComponent } from './attendee/attendee.component';
import { RoleGuard } from '../services/auth/role.guard';
import { AuthGuard } from '../services/auth/auth.guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from '../shared/layout/layout.component';
import { HomeComponent } from './home/home.component';
import { DetailComponent } from './detail/detail.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      {
        path: '',
        component: HomeComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: {
          allowRole: ['Admin', 'Host']
        },
      },
      {
        path: 'detail/:itemId',
        component: DetailComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: {
          allowRole: ['Admin', 'Host']
        },
      },
      {
        path: 'attendee',
        component: AttendeeComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: {
          allowRole: ['Admin', 'Host']
        }
      },
      {
        path: 'winner',
        component: WinnerComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: {
          allowRole: ['Admin', 'Host']
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RaffleRoutingModule { }
