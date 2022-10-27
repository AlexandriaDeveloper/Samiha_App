import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DailyBoxListComponent } from './daily-box-list/daily-box-list.component';
import { DailyListComponent } from './daily-list/daily-list.component';
import { FormListComponent } from './form-list/form-list.component';

const routes: Routes = [
  { path: '', component: DailyListComponent },
  { path: 'dailybox-list/:id', component: DailyBoxListComponent },
  { path: 'form-list/:id', component: FormListComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DailyRoutingModule { }
