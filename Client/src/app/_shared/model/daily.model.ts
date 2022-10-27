export class Daily{
  id? :number;
  name : string='';
  dailyDate : Date=new Date;
  total ?: number;
  totalDevelopment?:number;
}
export class DailyBox{
  id? :number;
  name : string='';
  dailyId : number;

  boxId: number;
  total ?: number;
  totalDevelopment?:number;

  box:Box;
}
export class Collage {
  id :number;
  name : string='';
}
export class Box {
  id :number;
  name : string='';
  collageId:number;
}
export interface ResponseData<T> {
  items: T[];
  total_count: number;
  totalSumTax?:number;
  totalTaxDevelopment?:number;
  totalOther?:number;
  totalTax2?:number;
  totalTaxsettlement?:number;
  totalStamp?:number;
  totalTaxNormal?:number;

}
/** public decimal? TaxNormal { get; set; }
        //دمغه عاديه
        public decimal? Stamp { get; set; }
        //تسويه ضريبيه
        public decimal? Taxsettlement { get; set; }
        // ضريبه اضافيه
        public decimal? TaxAdditional { get; set; }
        //اتساع او متنوعه
        public decimal? TaxVariant { get; set; }

        public decimal? SumTax { get; set; }
        // تنميه
        public decimal? TaxDevelopment { get; set; } */

export class Form {
  id? :number;
  num224:string='';
  name? : string='';
  dailyBoxId?:number;
  taxNormal?:number=0;
  stamp? :number=0;
  taxsettlement?:number=0;
  tax2?:number=0;
  other?:number=0;
  sumTax?:number;
  taxDevelopment?:number=0;
}
export class FormList{
  items : Form[]=[];
  taxNormal?:number;
  stamp? :number;
  taxsettlement?:number;
  tax2?:number;
  other?:number;
  sumTax?:number;
  taxDevelopment?:number;
  title: string;
collage: string;
boxName:string;

}
// export interface Daily {
//   id: number;
//   name: string;
//   dailyDate: string;

// }
