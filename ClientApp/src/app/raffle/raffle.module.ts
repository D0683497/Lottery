import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RaffleRoutingModule } from './raffle-routing.module';
import { HomeComponent } from './home/home.component';

@NgModule({
  declarations: [HomeComponent],
  imports: [
    CommonModule,
    RaffleRoutingModule
  ]
})
export class RaffleModule { }
