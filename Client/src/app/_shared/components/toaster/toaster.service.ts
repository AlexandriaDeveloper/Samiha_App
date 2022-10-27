import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ToasterSuccessComponent } from './toaster-success/toaster-success.component';

@Injectable({
  providedIn: 'root'
})
export class ToasterService {
  durationInSeconds = 5;
message :string;
  constructor(private _snackBar: MatSnackBar) { }

  openSuccessSnackBar(type :ToasterType,message :string) {
    this.message=message
    this._snackBar.openFromComponent(ToasterSuccessComponent, {
      duration: this.durationInSeconds * 1000,
      panelClass:ToasterType[ type],
      data:{message,type},
      verticalPosition:'top',
      horizontalPosition:'right'
    });
  }
}
export enum  ToasterType{
  success,
  fail,
  warn
}
