import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { MaterialSharedModule } from './material-shared.module';
import { LayoutComponent } from './layout/layout.component';

@NgModule({
  declarations: [LayoutComponent],
  imports: [
    CommonModule,
    RouterModule,
    MaterialSharedModule,
  ],
  exports: [
    CommonModule,
    RouterModule,
    MaterialSharedModule
  ]
})
export class SharedModule { }
