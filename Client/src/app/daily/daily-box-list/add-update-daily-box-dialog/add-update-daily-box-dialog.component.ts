import { AfterViewInit, Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { BoxService } from 'src/app/box/box.service';
import { CollageService } from 'src/app/collage/collage.service';
import { Box, Collage, DailyBox } from 'src/app/_shared/model/daily.model';
import { BoxParam } from 'src/app/_shared/model/param';
import { DailyListService } from '../../daily-list/daily-list.service';
import { DailyBoxListService } from '../dailyBox-list.service';

@Component({
  selector: 'app-add-update-daily-box-dialog',
  templateUrl: './add-update-daily-box-dialog.component.html',
  styleUrls: ['./add-update-daily-box-dialog.component.scss']
})
export class AddUpdateDailyBoxDialogComponent implements OnInit ,AfterViewInit{
  boxForm!: FormGroup;
  model : DailyBox = new DailyBox();
  collages : Collage[]=[];
  boxes: Box[]=[];


  boxParam : BoxParam= new BoxParam();
  constructor(public dialogRef: MatDialogRef<AddUpdateDailyBoxDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any, private fb : FormBuilder,  private dailyBoxService :DailyBoxListService,private collageService : CollageService,private boxService : BoxService) { }
  ngAfterViewInit(): void {


  this.loadCollages();
  }

  ngOnInit(): void {
    if(this.data.state==='Edit'){
      this.model=this.data.model;
      console.log(this.model);
    this.CollageSelectionChange(this.model.box.collageId);
      }
      if(this.data.state==='Add'){
        this.model=new DailyBox();
        console.log(this.model);
     // this.CollageSelectionChange(this.model.box.collageId);
        }
    this.boxForm=this.initilizeForm();


  }

  initilizeForm(){
    return this.fb.group(
      {
        id:[this.model?.id],
        collageId:[this.model?.box?.collageId],
        dailyId : [this.data.dailyId],

        boxId : [this.model.boxId,[Validators.required]]

    });
  }
  loadCollages(){
    return this.collageService.getCollages().subscribe((x:[]) => this.collages=x
    )
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
  Save(){
    this.model= Object.assign({...this.model},this.boxForm.value)
if(this.data.state ==='Add')
    this.dailyBoxService.postDailyBox(this.model).subscribe();
if(this.data.state ==='Edit')
{console.log(this.boxForm.value);

    this.dailyBoxService.putDailyBox(this.model).subscribe();
}
this.onNoClick();
  }
  test(ev){

  }
  CollageSelectionChange(ev){
this.boxParam.collageId=ev;
this.loadBoxes();
  }

  loadBoxes() {
 this.boxService.getBoxes(this.boxParam).subscribe((x:Box[]) =>
  this.boxes=x
 );

  }
  boxSelectionChange(ev){
    console.log(ev);

    this.boxForm.patchValue({boxId:ev})
    console.log(this.boxForm);


  }
}
