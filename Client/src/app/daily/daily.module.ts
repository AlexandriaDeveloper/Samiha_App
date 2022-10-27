import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DailyRoutingModule } from './daily-routing.module';
import { DailyListComponent } from './daily-list/daily-list.component';
import { SharedModule } from '../_shared/shared.module';
import { AddUpdateDialogComponent } from './daily-list/add-update-dialog/add-update-dialog.component';
import { DailyBoxListComponent } from './daily-box-list/daily-box-list.component';

import { AddUpdateDailyBoxDialogComponent } from './daily-box-list/add-update-daily-box-dialog/add-update-daily-box-dialog.component';
import { FormListComponent } from './form-list/form-list.component';
import { AddUpdateFormDialogComponent } from './form-list/add-update-form-dialog/add-update-form-dialog.component';


@NgModule({
  declarations: [
    DailyListComponent,
    AddUpdateDialogComponent,
    DailyBoxListComponent,

    AddUpdateDailyBoxDialogComponent,
     FormListComponent,
     AddUpdateFormDialogComponent
  ],
  imports: [
    CommonModule,
    DailyRoutingModule,
    SharedModule
  ]
})
export class DailyModule { }
