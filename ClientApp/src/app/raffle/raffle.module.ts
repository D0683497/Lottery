import { NgModule } from '@angular/core';

import { RaffleRoutingModule } from './raffle-routing.module';
import { SharedModule } from './../shared/shared.module';
import { HomeComponent } from './home/home.component';
import { StartComponent } from './start/start.component';

@NgModule({
  declarations: [HomeComponent, StartComponent],
  imports: [
    RaffleRoutingModule,
    SharedModule
  ]
})
export class RaffleModule { }
