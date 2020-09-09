import { NgModule } from '@angular/core';
import { RaffleRoutingModule } from './raffle-routing.module';
import { SharedModule } from '../shared/shared.module';
import { HomeComponent } from './home/home.component';
import { DetailComponent } from './detail/detail.component';
import { UploadComponent } from './upload/upload.component';
import { WinnerComponent } from './winner/winner.component';
import { AttendeeComponent } from './attendee/attendee.component';
import { AttendeeAddComponent } from './attendee-add/attendee-add.component';
import { ItemAddComponent } from './item-add/item-add.component';

@NgModule({
  declarations: [
    HomeComponent,
    DetailComponent,
    UploadComponent,
    WinnerComponent,
    AttendeeComponent,
    AttendeeAddComponent,
    ItemAddComponent
  ],
  imports: [
    RaffleRoutingModule,
    SharedModule
  ],
  entryComponents: [UploadComponent, AttendeeAddComponent, ItemAddComponent]
})
export class RaffleModule { }
