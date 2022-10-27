import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, of } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CollageService {
url :string= environment.baseUrl+'collage'
  constructor(private http : HttpClient) {


   }
   getCollages(){
    return this.http.get(this.url).pipe(catchError(()=>of(null)));


  }
}
