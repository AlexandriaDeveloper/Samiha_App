import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NavComponent } from './layout/nav/nav.component';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from './material.module';
import { SumPipe } from './pipe/sum.pipe';

import { ToasterSuccessComponent } from './components/toaster/toaster-success/toaster-success.component';
import { AppRoutingModule } from '../app-routing.module';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [NavComponent, SumPipe, ToasterSuccessComponent],
  imports: [RouterModule,
    CommonModule,FormsModule,
    ReactiveFormsModule, HttpClientModule,
     MaterialModule

  ], exports: [NavComponent,FormsModule,ReactiveFormsModule, MaterialModule,SumPipe]
})
export class SharedModule { }
