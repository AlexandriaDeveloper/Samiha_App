import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SortDirection } from '@angular/material/sort';
import { catchError, map, of, tap } from 'rxjs';
import { ToasterService, ToasterType } from 'src/app/_shared/components/toaster/toaster.service';
import { Daily } from 'src/app/_shared/model/daily.model';
import { DailyParam, FormParam } from 'src/app/_shared/model/param';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class DailyListService {
  url = environment.baseUrl + 'daily'
  constructor(private http: HttpClient , private toasterService : ToasterService) { }

  getDailies(param: DailyParam) {

   let params :HttpParams = new HttpParams();
   if(param.sort)
  {params= params.append('sort',param.sort );}
  if(param.order)
  {  params= params.append('order',param.order );}
  params= params.append('pageIndex',param.pageIndex);
  if(param.name !== '')
  {
    params= params.append('name',param.name??'');
  }
  if(param.inMonth)
  {
    params= params.append('inMonth',param.inMonth.toString());
  }
  if(param.dailyDate)
  {
    params= params.append('dailyDate',param.dailyDate.toString());
  }
  return this.http.get(this.url,{params}).pipe(catchError(()=>of(null)));

    // return this.http.get(`${this.url}?sort=${param.sort}&order=${param.order}&pageIndex=${param.pageIndex
    //   }`);
  }


  getDailyByFormNum(param :DailyParam,num224=null){
    let params :HttpParams = new HttpParams();
    if(param.sort)
   {params= params.append('sort',param.sort );}
   if(param.order)
   {  params= params.append('order',param.order );}
   params= params.append('pageIndex',param.pageIndex);
   if(num224 !== null)
   {
     params= params.append('num224',num224);
   }
   return this.http.get(this.url+'/search',{params}).pipe(catchError(()=>of(null)));

  }
  postDaily(  model:Daily){
    return this.http.post<Daily>(this.url,model).pipe(

      tap((x)=>{
        this.toasterService.openSuccessSnackBar(ToasterType.success,"تم الحفظ بنجاح  ")
      } ));

  //     ,

  //     catchError((err)=>
  //   {

  //   //  this.toasterService.openSuccessSnackBar(ToasterType.fail,"يوجد خطأ")
  //  //   return of(err)

  //   }
  //  ));
  }
  putDaily(  model:Daily){
    return this.http.put<Daily>(`${this.url}/${model.id}`,model).pipe(catchError(()=>of(null)));;
  }

  deleteDaily(id : number){
    return this.http.delete<Daily>(`${this.url}/${id}`).pipe(catchError(()=>of(null)));;
  }


  exportDailies(param: DailyParam) {

    let params :HttpParams = new HttpParams();
    if(param.sort)
   {params= params.append('sort',param.sort );}
   if(param.order)
   {  params= params.append('order',param.order );}
   params= params.append('pageIndex',param.pageIndex);
   if(param.name !== '')
   {
     params= params.append('name',param.name??'');
   }

   if(param.inMonth)
   {
     params= params.append('inMonth',param.inMonth.toString());
   }
   if(param.dailyDate)
   {
     params= params.append('dailyDate',param.dailyDate.toString());
   }
   return this.http.get(this.url+`/export`,{params,observe: 'response', responseType: 'blob' }).pipe(

    map((x: HttpResponse<any>) => {
      let blob = new Blob([x.body], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
      const url = window.URL.createObjectURL(blob);
      window.open(url);

    }),
    catchError(()=>of(null)));
   }


}
