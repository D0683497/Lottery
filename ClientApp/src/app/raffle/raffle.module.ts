import { NgModule } from '@angular/core';
import { RaffleRoutingModule } from './raffle-routing.module';
import { SharedModule } from '../shared/shared.module';
import { HomeComponent } from './home/home.component';
import { AddComponent } from './add/add.component';
import { DetailComponent } from './detail/detail.component';
import { UploadComponent } from './upload/upload.component';
import { WinnerComponent } from './winner/winner.component';
import { AttendeeComponent } from './attendee/attendee.component';
import { AttendeeAddComponent } from './attendee-add/attendee-add.component';

@NgModule({
  declarations: [HomeComponent, AddComponent, DetailComponent, UploadComponent, WinnerComponent, AttendeeComponent, AttendeeAddComponent],
  imports: [
    RaffleRoutingModule,
    SharedModule
  ],
  entryComponents: [AddComponent, UploadComponent, AttendeeAddComponent]
})
export class RaffleModule { }
