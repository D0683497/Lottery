import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { RaffleRoutingModule } from './raffle-routing.module';
import { SharedModule } from './../shared/shared.module';
import { HomeComponent } from './home/home.component';
import { StartComponent } from './start/start.component';
import { AddComponent } from './add/add.component';

@NgModule({
  declarations: [HomeComponent, StartComponent, AddComponent],
  imports: [
    RaffleRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class RaffleModule { }
