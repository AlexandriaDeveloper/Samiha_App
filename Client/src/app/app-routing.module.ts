import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from './_shared/shared.module';

const routes: Routes = [{
  path: 'daily',
  loadChildren: () => import('./daily/daily.module').then(m => m.DailyModule)
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
