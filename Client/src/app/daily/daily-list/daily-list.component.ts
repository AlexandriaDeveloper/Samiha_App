import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, SortDirection } from '@angular/material/sort';
import { catchError, debounce, debounceTime, distinct, distinctUntilChanged, fromEvent, map, merge, Observable, of, startWith, switchMap } from 'rxjs';
import { ToasterService, ToasterType } from 'src/app/_shared/components/toaster/toaster.service';
import { Daily, ResponseData } from 'src/app/_shared/model/daily.model';
import { DailyParam } from 'src/app/_shared/model/param';
import { AddUpdateDialogComponent } from './add-update-dialog/add-update-dialog.component';
import { DailyListService } from './daily-list.service';

@Component({
  selector: 'app-daily-list',
  templateUrl: './daily-list.component.html',
  styleUrls: ['./daily-list.component.scss']
})
export class DailyListComponent implements OnInit, AfterViewInit {

  @ViewChild("nameInput") nameInput?: ElementRef;
  @ViewChild("dailyDateInput") dailyDateInput?: ElementRef;
  //dailyDateInput
  nameVal:string='';

  ngOnInit(): void {
  }

  displayedColumns: string[] = ['id', 'name', 'dailyDate', 'taxNormal','stamp','taxsettlement',
  'tax2','other','sumTax','taxDevelopment'];
  //exampleDatabase: ExampleHttpDatabase | null;
  data: ResponseData<Daily>;
  param: DailyParam = new DailyParam();
  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;
  @ViewChild('searchInput')  searchInput :ElementRef;
  @ViewChild(MatPaginator) paginator?: MatPaginator;
  @ViewChild(MatSort) sort?: MatSort;
  filteredOptions?: Observable<any[]>;
  constructor(private dailyListService: DailyListService,public dialog: MatDialog) { }

  ngAfterViewInit() {
    fromEvent(this.searchInput?.nativeElement,'keyup')
    .pipe(debounceTime(600), distinctUntilChanged(), map((x: any) => {
console.log(x.target.value)
return x.target.value;
})).subscribe( x=> this.loadDailyByFormNum(x)
);

fromEvent(this.nameInput?.nativeElement,'keyup').pipe(debounceTime(600),distinctUntilChanged(),map((x: any)=>{
  this.param.name=x.target.value;
  console.log(this.param);
  this.loadDaily();

})).subscribe();

fromEvent(this.dailyDateInput?.nativeElement,'change').pipe(map((x: any)=>{

  this.param.inMonth=x.target.value;
  console.log(this.param);
  this.loadDaily();

})).subscribe();
    //this.exampleDatabase = new ExampleHttpDatabase(this._httpClient);
    this.loadDaily();
    // this.nameInput.nativeElement..subscribe(x => console.log(x)
    // );





  }

  loadDailyByFormNum(num) {


    // If the user changes the sort order, reset back to the first page.
    this.sort?.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

    merge(this.sort.sortChange, this.paginator?.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          this.param.pageIndex = this.paginator?.pageIndex;
          this.param.order = this.sort?.direction;
          this.param.sort = this.sort?.active;


          return this.dailyListService.getDailyByFormNum(
            this.param,num
          ).pipe(catchError(() => of(null)));
        }),
        map((data: any) => {
          // Flip flag to show that loading has finished.
          this.isLoadingResults = false;
       //   this.isRateLimitReached = data.items.length === 0;

          if (data === null) {
            return [];
          }

          // Only refresh the result length if there is new data. In case of rate
          // limit errors, we do not want to reset the paginator to zero, as that
          // would prevent users from re-triggering requests.



          this.resultsLength = data.totalCount;
          return data;

        }),
      )
      .subscribe(data => (this.data = data));

  }
  loadDaily() {


    // If the user changes the sort order, reset back to the first page.
    this.sort?.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

    merge(this.sort.sortChange, this.paginator?.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          this.param.pageIndex = this.paginator?.pageIndex;
          this.param.order = this.sort?.direction;
          this.param.sort = this.sort?.active;


          return this.dailyListService.getDailies(
            this.param
          ).pipe(catchError(() => of(null)));
        }),
        map((data: any) => {
          // Flip flag to show that loading has finished.
          this.isLoadingResults = false;
       //   this.isRateLimitReached = data.items.length === 0;

          if (data === null) {
            return [];
          }

          // Only refresh the result length if there is new data. In case of rate
          // limit errors, we do not want to reset the paginator to zero, as that
          // would prevent users from re-triggering requests.



          this.resultsLength = data.totalCount;
          return data;

        }),
      )
      .subscribe(data => (this.data = data));

  }

  openDialog( state:string,model? :Daily): void {
    const dialogRef = this.dialog.open(AddUpdateDialogComponent, {
      width: '650px',
      minHeight:'400px',
      hasBackdrop:true,
      disableClose:true,
      data: {state,model  },
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    //  this.animal = result;

    this.loadDaily();
    });
  }
deleteDaily(model :Daily){

if(confirm(` انت على وشك حذف يوميه ${model.name} هل انت متأكد ؟؟؟؟`)){
  this.dailyListService.deleteDaily(model.id).subscribe(x => this.loadDaily()
);
}
}


export(){
  this.dailyListService.exportDailies(this.param).subscribe();

}
}




/** An example database that the data source uses to retrieve data for the table. */
// export class ExampleHttpDatabase {
//   constructor(private _httpClient: HttpClient) { }

//   getRepoIssues(sort: string, order: SortDirection, page: number): Observable<GithubApi> {
//     const href = 'https://api.github.com/search/issues';
//     const requestUrl = `${href}?q=repo:angular/components&sort=${sort}&order=${order}&page=${page + 1
//       }`;

//     return this._httpClient.get<GithubApi>(requestUrl);
//   }
// }


