import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AttendeeRoutingModule } from './attendee-routing.module';
import { SharedModule } from './../shared/shared.module';
import { AddComponent } from './add/add.component';


@NgModule({
  declarations: [AddComponent],
  imports: [
    AttendeeRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class AttendeeModule { }
