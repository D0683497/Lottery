import { NgModule } from '@angular/core';

import { StartRoutingModule } from './start-routing.module';
import { SharedModule } from '../shared/shared.module';
import { HomeComponent } from './home/home.component';
import { ResultComponent } from './result/result.component';


@NgModule({
  declarations: [HomeComponent, ResultComponent],
  imports: [
    StartRoutingModule,
    SharedModule
  ]
})
export class StartModule { }
