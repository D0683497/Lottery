import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { PrizeRoutingModule } from './prize-routing.module';
import { SharedModule } from './../shared/shared.module';
import { AddComponent } from './add/add.component';

@NgModule({
  declarations: [AddComponent],
  imports: [
    PrizeRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class PrizeModule { }
