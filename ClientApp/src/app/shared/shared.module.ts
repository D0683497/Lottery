import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { MaterialSharedModule } from './material-shared.module';
import { LayoutComponent } from './layout/layout.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ReplacePipe } from './helpers/replace.pipe';

@NgModule({
  declarations: [LayoutComponent, ReplacePipe],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialSharedModule
  ],
  exports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialSharedModule,
    ReplacePipe
  ]
})
export class SharedModule { }
