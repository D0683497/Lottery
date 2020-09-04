import { NgModule } from '@angular/core';
import { RaffleRoutingModule } from './raffle-routing.module';
import { SharedModule } from '../shared/shared.module';
import { HomeComponent } from './home/home.component';
import { AddComponent } from './add/add.component';
import { DetailComponent } from './detail/detail.component';

@NgModule({
  declarations: [HomeComponent, AddComponent, DetailComponent],
  imports: [
    RaffleRoutingModule,
    SharedModule
  ],
  entryComponents: [AddComponent]
})
export class RaffleModule { }
