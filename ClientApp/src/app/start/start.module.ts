import { NgModule } from '@angular/core';

import { StartRoutingModule } from './start-routing.module';
import { SharedModule } from '../shared/shared.module';
import { HomeComponent } from './home/home.component';
import { ResultComponent } from './result/result.component';
import { ClientComponent } from './client/client.component';
import { HostComponent } from './host/host.component';

@NgModule({
  declarations: [HomeComponent, ResultComponent, ClientComponent, HostComponent],
  imports: [
    StartRoutingModule,
    SharedModule
  ],
  entryComponents: [ResultComponent]
})
export class StartModule { }
