import { AfterViewInit, Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Form } from 'src/app/_shared/model/daily.model';
import { FormService } from '../form.service';

@Component({
  selector: 'app-add-update-form-dialog',
  templateUrl: './add-update-form-dialog.component.html',
  styleUrls: ['./add-update-form-dialog.component.scss']
})
export class AddUpdateFormDialogComponent implements OnInit ,AfterViewInit{
form:FormGroup;
model : Form = new Form();
  constructor(public dialogRef: MatDialogRef<AddUpdateFormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any, private fb : FormBuilder, private formService :FormService) { }
  ngAfterViewInit(): void {

  }

  ngOnInit(): void {
    console.log(this.data);

    this.model.dailyBoxId=this.data.dailyBoxId;
    if(this.data.state ==='Edit'){
      this.model= this.data.model;
    }

    this.form=this.initlizeForm();
    console.log(this.form);

  }

  initlizeForm(){
    return this.fb.group({
      id:[this.model.id],
      name:[this.model.name],
      num224:[this.model.num224,[Validators.required]],
      taxNormal:[this.model.taxNormal,[Validators.required]],
      stamp:[this.model.stamp,[Validators.required]],
      taxsettlement:[this.model.taxsettlement,[Validators.required]],
      tax2:[this.model.tax2,[Validators.required]],
      other:[this.model.other,[Validators.required]],
      taxDevelopment:[this.model.taxDevelopment,[Validators.required] ]

    });
  }

  formValidator(control) {
    return this.form.controls[control].errors
  }
  onNoClick(){    this.dialogRef.close();}
  Save(){
    console.log(this.data.dailyBoxId);

    this.model= Object.assign({...this.model},this.form.value);
      console.log(this.form.value);
if(this.data.state==='Add')
    this.formService.postForm(this.model).subscribe(x=> this.onNoClick())
    if(this.data.state==='Edit'){
      this.formService.putForm(this.model).subscribe(x=> this.onNoClick())
    }

  }
}
