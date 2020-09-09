import { NgModule } from '@angular/core';

import { StartRoutingModule } from './start-routing.module';
import { SharedModule } from '../shared/shared.module';
import { HomeComponent } from './home/home.component';
import { ResultComponent } from './result/result.component';
import { ClientComponent } from './client/client.component';
import { HostComponent } from './host/host.component';
import { DrawResultComponent } from './draw-result/draw-result.component';

@NgModule({
  declarations: [HomeComponent, ResultComponent, ClientComponent, HostComponent, DrawResultComponent],
  imports: [
    StartRoutingModule,
    SharedModule
  ],
  entryComponents: [DrawResultComponent]
})
export class StartModule { }
