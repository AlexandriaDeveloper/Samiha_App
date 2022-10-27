import { SortDirection } from "@angular/material/sort";

export interface IParam {

  sort: string;
  order: SortDirection;
  pageIndex: number;
  pageSize:number;
}
export class DailyParam implements IParam {
  sort: string;
  order: SortDirection;
  pageIndex: number = 0;
  pageSize:number=30;

  id?: number ;
  name?: string = '';
  dailyDate?: Date;
  inMonth? : Date;

}
export class DailyBoxParam implements IParam {
  sort: string;
  order: SortDirection;
  pageIndex: number = 0;
  pageSize:number=30;


  id?: number ;
  name?: string = '';
  dailyId?: number;
  boxId?: number;
  collageId?: number=null;


}
export class BoxParam implements IParam {
  sort: string;
  order: SortDirection;
  pageIndex: number = 0;
  pageSize:number=30;


  id?: number ;
  name?: string = '';
  collageId?: number;
}


export class FormParam implements IParam{
  sort: string;
  order: SortDirection;
  pageIndex: number;
  pageSize:number=30;


  id?: number ;
  name?: string = '';
  collageId? :number;
  num224:string;
  dailyBoxId?:number;
}
