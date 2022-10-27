import { AfterViewInit, Component, Inject, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DATE_FORMATS } from '@angular/material/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import * as moment from 'moment';
import { ToasterService, ToasterType } from 'src/app/_shared/components/toaster/toaster.service';
import { Daily } from 'src/app/_shared/model/daily.model';
import { DailyListService } from '../daily-list.service';
export const MY_FORMATS = {
  parse: {
    dateInput: 'LL'
  },
  display: {
    dateInput: 'DD-MM-YYYY',
    monthYearLabel: 'YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'YYYY'
  }
};
@Component({
  selector: 'app-add-update-dialog',
  templateUrl: './add-update-dialog.component.html',
  styleUrls: ['./add-update-dialog.component.scss'],
  providers: [
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS }
  ]
})
export class AddUpdateDialogComponent implements OnInit,AfterViewInit {
@ViewChild('datePicker') datePicker:HTMLInputElement;
 dailyForm!: FormGroup;
 model : Daily = new Daily();
 name:string='';
  constructor( public dialogRef: MatDialogRef<AddUpdateDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any, private fb : FormBuilder, private dailyService :DailyListService ) { }
  ngAfterViewInit(): void {




  }

  ngOnInit(): void {


    if(this.data.state==='Edit'){

      this.model=this.data.model;


     }
    this.dailyForm=this.initilizeForm();
  }

  initilizeForm (){
    return this.fb.group({
      name : [this.model.name,[Validators.required]],
      dailyDate : [this.model.dailyDate,[Validators.required]]

    })
  }

  Save(){


   this.model= Object.assign({...this.model},this.dailyForm.value)
  //this.model.dailyDate.setDate(this.model.dailyDate.getDate()+1);






if(this.data.state ==='Add')
    this.dailyService.postDaily(this.model).subscribe({

      complete:()=>{
        this.onNoClick();
      }


    });
    if(this.data.state ==='Edit'){
      this.dailyService.putDaily(this.model).subscribe({
        complete:()=>{
          this.onNoClick();
        }


      })
    }

  }
  test(ev :any){
    console.log(ev);

  //var date = moment(ev).add(1,'M')
 // console.log(date);
  this.dailyForm.patchValue({dailyDate:moment(ev).add(2,'hour')});
  console.log(this.dailyForm.value);

  }
  onNoClick(): void {
    this.dialogRef.close();
  }
}
