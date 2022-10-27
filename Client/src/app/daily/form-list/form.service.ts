import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { of } from 'rxjs/internal/observable/of';
import { catchError } from 'rxjs/internal/operators/catchError';
import { Daily, Form } from 'src/app/_shared/model/daily.model';
import { DailyParam, FormParam } from 'src/app/_shared/model/param';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FormService {
url:string = environment.baseUrl+'forms';
  constructor(private http : HttpClient) { }

  getForms(param: FormParam) {

    let params :HttpParams = new HttpParams();
    if(param.sort)
   {params= params.append('sort',param.sort );}
   if(param.order)
   {  params= params.append('order',param.order );}

   if(param.dailyBoxId !== null)
   {
     params= params.append('dailyBoxId',param.dailyBoxId??null);
   }
   if(param.name !== '')
   {
     params= params.append('name',param.name??'');
   }
   return this.http.get(this.url,{params}).pipe(catchError(()=>of(null)));
   }

   postForm ( model:Form){
     return this.http.post<Form>(this.url,model).pipe(catchError(()=>of(null)));
   }
   putForm(  model:Form){
     return this.http.put<Form>(`${this.url}/${model.id}`,model).pipe(catchError(()=>of(null)));;
   }

   deleteForm(id : number){
     return this.http.delete<Form>(`${this.url}/${id}`).pipe(catchError(()=>of(null)));;
   }

   exportForms(param: FormParam){
    let params :HttpParams = new HttpParams();
    if(param.sort)
   {params= params.append('sort',param.sort );}
   if(param.order)
   {  params= params.append('order',param.order );}

   if(param.dailyBoxId !== null)
   {
     params= params.append('dailyBoxId',param.dailyBoxId??null);
   }
   if(param.name !== '')
   {
     params= params.append('name',param.name??'');
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
