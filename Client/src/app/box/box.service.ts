import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BoxParam } from '../_shared/model/param';

@Injectable({
  providedIn: 'root'
})
export class BoxService {

  url = environment.baseUrl+'box';
  constructor(private http : HttpClient) { }

  getBoxes(param: BoxParam) {

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

   if(param.collageId!== null)
   {
     params= params.append('collageId',param.collageId.toString());
   }
   return this.http.get(this.url,{params}).pipe(catchError(()=>of(null)));

     // return this.http.get(`${this.url}?sort=${param.sort}&order=${param.order}&pageIndex=${param.pageIndex
     //   }`);
   }


}
