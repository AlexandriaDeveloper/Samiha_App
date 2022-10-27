import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SortDirection } from '@angular/material/sort';
import { catchError, map, of } from 'rxjs';
import { Daily, DailyBox } from 'src/app/_shared/model/daily.model';
import { DailyBoxParam, DailyParam, FormParam } from 'src/app/_shared/model/param';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class DailyBoxListService {
  url = environment.baseUrl + 'dailyBox'
  constructor(private http: HttpClient) { }

  getDailyBoxByDailyId(param: DailyBoxParam) {

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
  if(param.collageId !==null){
    params= params.append('collageId',param.collageId??null);
  }

  if(param.dailyId!== null)
  {
    params= params.append('dailyId',param.dailyId.toString());
  }
  return this.http.get(this.url,{params}).pipe(catchError(()=>of(null)));

    // return this.http.get(`${this.url}?sort=${param.sort}&order=${param.order}&pageIndex=${param.pageIndex
    //   }`);
  }

  // getDailyBoxByDailyId(id:number){
  //   let params :HttpParams = new HttpParams();
  //   params= params.append('dailyId',id??'');
  // return this.http.get(this.url,{params});
  // }



  getDailyByFormNum224(param :FormParam){
    let params :HttpParams = new HttpParams();
    if(param.sort)
   {params= params.append('sort',param.sort );}
   if(param.order)
   {  params= params.append('order',param.order );}
   params= params.append('pageIndex',param.pageIndex);

  if ( param.num224 !=='')
  {
    params= params.append('num224',param.num224);
  }
  return this.http.get(this.url+'search',{params}).pipe(catchError(()=>of(null)));
  }

  postDailyBox(  model:DailyBox){
    return this.http.post<DailyBox>(this.url,model).pipe(catchError(()=>of(null)));;
  }
  putDailyBox(  model:DailyBox){
    return this.http.put<DailyBox>(`${this.url}/${model.id}`,model).pipe(catchError(()=>of(null)));;
  }

  deleteDailyBox(id : number){
    return this.http.delete<DailyBox>(`${this.url}/${id}`).pipe(catchError(()=>of(null)));;
  }



  exportDailies(param: DailyBoxParam) {

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
   if(param.collageId !==null){
     params= params.append('collageId',param.collageId??null);
   }

   if(param.dailyId!== null)
   {
     params= params.append('dailyId',param.dailyId.toString());
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
