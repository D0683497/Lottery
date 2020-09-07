import { NgModule } from '@angular/core';

import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from './../shared/shared.module';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { RegisterAdminComponent } from './register-admin/register-admin.component';
import { RegisterHostComponent } from './register-host/register-host.component';
import { RegisterClientComponent } from './register-client/register-client.component';

@NgModule({
  declarations: [RegisterComponent, LoginComponent, RegisterAdminComponent, RegisterHostComponent, RegisterClientComponent],
  imports: [
    AccountRoutingModule,
    SharedModule,
  ]
})
export class AccountModule { }
