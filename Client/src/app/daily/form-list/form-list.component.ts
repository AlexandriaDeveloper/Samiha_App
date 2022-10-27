import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { catchError, map, of } from 'rxjs';
import { Form, ResponseData } from 'src/app/_shared/model/daily.model';
import { FormParam } from 'src/app/_shared/model/param';
import { AddUpdateFormDialogComponent } from './add-update-form-dialog/add-update-form-dialog.component';
import { FormService } from './form.service';

@Component({
  selector: 'app-form-list',
  templateUrl: './form-list.component.html',
  styleUrls: ['./form-list.component.scss']
})
export class FormListComponent implements OnInit ,AfterViewInit{

param : FormParam= new FormParam();


data: any;
dailyBoxId;
isLoadingResults = true;
isRateLimitReached = false;
resultsLength =0;

@ViewChild(MatPaginator) paginator?: MatPaginator;
@ViewChild(MatSort) sort: MatSort;
filteredOptions?:Form []


displayedColumns: string[] = ['id','num224', 'name', 'taxNormal','stamp','taxsettlement',
'tax2','other','sumTax','taxDevelopment'];
  constructor(private router : ActivatedRoute,private formService : FormService,public dialog: MatDialog) { }
  ngAfterViewInit(): void {
   // throw new Error('Method not implemented.');
   this.loadForms();
  }

  ngOnInit(): void {
    this.dailyBoxId=this.router.snapshot.params["id"];
    this.param.dailyBoxId=this.dailyBoxId;

  }

  loadForms (){
    return this.formService.getForms(
      this.param
    ).pipe(catchError(() => of(null)),
      map((data: any) => {
    // Flip flag to show that loading has finished.
    this.isLoadingResults = false;
    //this.isRateLimitReached = data.items.length === 0;
    if (data === null) {
      return [];
    }
    // Only refresh the result length if there is new data. In case of rate
    // limit errors, we do not want to reset the paginator to zero, as that
    // would prevent users from re-triggering requests.
    this.resultsLength = data.totalCount;
    return data;
  })) .subscribe(data =>{
            this.data=data;
            this.filteredOptions=data.items
            console.log(this.data);

          });
  }


  CollageSelectionChange(ev){}
  getCollageById(ev){}
  openDialog(state,model=null){

    const dialogRef = this.dialog.open(AddUpdateFormDialogComponent, {
      width: '650px',
      minHeight:'400px',
      hasBackdrop:true,
      disableClose:true,
      data: {state,model,dailyBoxId:this.dailyBoxId },
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    //  this.animal = result;

    this.loadForms();
    });

  }
deleteForm(row){

  if(confirm(`انت على وشك حذف الاستمارة رقم  ${row.num224} هل انت متأكد ؟؟!`)){
    this.formService.deleteForm(row.id).subscribe(x =>{this.loadForms()});
  }
}
sortData(sort: Sort) {
  const data = this.filteredOptions.slice();
  if (!sort.active || sort.direction === '') {
    this.filteredOptions = data;
    return;
  }

  this.filteredOptions = data.sort((a, b) => {
    const isAsc = sort.direction === 'asc';
    switch (sort.active) {
      case 'name':
        return this.compare(a.name, b.name, isAsc);
      case 'num224':
        return this.compare(a.num224, b.num224, isAsc);

      default:
        return 0;
    }
  });
}
  compare(a: number | string, b: number | string, isAsc: boolean) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }
  export(){
    this.formService.exportForms(this.param).subscribe();

  }
}


