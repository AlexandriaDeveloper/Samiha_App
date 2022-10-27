import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { ToasterService, ToasterType } from '../components/toaster/toaster.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private toaster : ToasterService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
  
    if (request.method === 'POST')
    return next.handle(request).pipe(

      tap(x =>{
        this.toaster.openSuccessSnackBar(ToasterType.success,"تم التسجيل بنجاح");
      }),
      catchError((err :HttpErrorResponse) =>{


          this.toaster.openSuccessSnackBar(ToasterType.fail, err.error.message);
        return throwError(err);


      })
    );
if(request.method==="PUT")

    return next.handle(request).pipe(

      tap(x =>{
        this.toaster.openSuccessSnackBar(ToasterType.success,"تم التعديل بنجاح");
      }),
      catchError((err :HttpErrorResponse) =>{

          this.toaster.openSuccessSnackBar(ToasterType.fail, err.error.message);
        return throwError(err);


      })
    );

    if(request.method==="DELETE")

    return next.handle(request).pipe(

      tap(x =>{
        this.toaster.openSuccessSnackBar(ToasterType.success,"تم الحذف بنجاح");
      }),
      catchError((err) =>{

          this.toaster.openSuccessSnackBar(ToasterType.fail,"يوجد خطأ ");
        return throwError(err);


      })
    );
    return next.handle(request);
  }

}
