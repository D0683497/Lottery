import { NgModule } from '@angular/core';

import { WinnerRoutingModule } from './winner-routing.module';
import { SharedModule } from '../shared/shared.module';
import { HomeComponent } from './home/home.component';

@NgModule({
  declarations: [HomeComponent],
  imports: [
    WinnerRoutingModule,
    SharedModule
  ]
})
export class WinnerModule { }
