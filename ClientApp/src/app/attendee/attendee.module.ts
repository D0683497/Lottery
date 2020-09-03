import { NgModule } from '@angular/core';

import { AttendeeRoutingModule } from './attendee-routing.module';
import { AddComponent } from './add/add.component';
import { HomeComponent } from './home/home.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [AddComponent, HomeComponent],
  imports: [
    AttendeeRoutingModule,
    SharedModule
  ]
})
export class AttendeeModule { }
