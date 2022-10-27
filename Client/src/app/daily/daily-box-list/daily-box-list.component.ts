import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { catchError, map, merge, Observable, of, startWith, switchMap } from 'rxjs';
import { CollageService } from 'src/app/collage/collage.service';
import { ToasterService, ToasterType } from 'src/app/_shared/components/toaster/toaster.service';
import { Collage, DailyBox, ResponseData } from 'src/app/_shared/model/daily.model';
import { DailyBoxParam } from 'src/app/_shared/model/param';
import { AddUpdateDailyBoxDialogComponent } from './add-update-daily-box-dialog/add-update-daily-box-dialog.component';


import { DailyBoxListService } from './dailyBox-list.service';

@Component({
  selector: 'app-daily-box-list',
  templateUrl: './daily-box-list.component.html',
  styleUrls: ['./daily-box-list.component.scss']
})
export class DailyBoxListComponent implements OnInit ,AfterViewInit{

  data: ResponseData<DailyBox>;
  dailyId;
  isLoadingResults = true;
  isRateLimitReached = false;
  resultsLength =0;
  param : DailyBoxParam= new DailyBoxParam();
  @ViewChild(MatPaginator) paginator?: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  filteredOptions?:DailyBox []

  collages:Collage[]=[];


  displayedColumns: string[] = ['id','box','collage', 'total','totalTaxDevelopment'];
  constructor(private router : ActivatedRoute, private dailyBoxService : DailyBoxListService,
    private collageService : CollageService,public dialog: MatDialog) { }
  ngAfterViewInit(): void {
    this.loadCollages();
    this.loadDailyBox();
  }

  ngOnInit(): void {
    this.dailyId =this.router.snapshot.params['id']
console.log(this.dailyId);

    this.param.dailyId=this.dailyId;


  }
  loadDailyBox() {
     return this.dailyBoxService.getDailyBoxByDailyId(
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

  }))


          .subscribe(data =>{
          console.log(this.data);

            this.data=data;
            this.filteredOptions=data.items
          });

      }
      loadCollages(){
        return this.collageService.getCollages().subscribe((x:[]) => this.collages=x
        )
      }

      getCollageById(id : number): string{
        return this.collages.find(x => x.id===id).name;
      }
      openDialog(state,model = null){

        const dialogRef = this.dialog.open(AddUpdateDailyBoxDialogComponent, {
          width: '650px',
          minHeight:'400px',
          hasBackdrop:true,
          disableClose:true,
          data: {state,model,dailyId :this.dailyId },
        });

        dialogRef.afterClosed().subscribe(result => {
          console.log('The dialog was closed');
        //  this.animal = result;

        this.loadDailyBox();
        });
      }
      deleteDailyBox(row){
        if(confirm("انت على وشك حذف صندوق هل انت متأكد ؟؟؟؟!"))
        this.dailyBoxService.deleteDailyBox(row.id).subscribe(x =>{

          this.loadDailyBox();
        });
      }
      CollageSelectionChange(ev){
if (ev===undefined){
//this.filteredOptions= this.data.items;
this.param.collageId=null;

}
else{
       // this.filteredOptions =this.data.items.filter(x => x.box.collageId===ev)
        this.param.collageId = ev;


}

this.loadDailyBox()
      }
      export(){
        this.dailyBoxService.exportDailies(this.param).subscribe();
      }
}
