import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CollageRoutingModule } from './collage-routing.module';
import { SharedModule } from '../_shared/shared.module';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    CollageRoutingModule,
    SharedModule
  ]
})
export class CollageModule { }
