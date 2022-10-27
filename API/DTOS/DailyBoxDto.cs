using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOS
{
    public class DailyBoxDto
    {
        public int? Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; } = "";

        public decimal Total { get; set; }

        public decimal TotalTaxDevelopment { get; set; }

        [Required]
        public int BoxId { get; set; }
        [Required]
        public int DailyId { get; set; }
        // public ICollection<FormDto> Forms { get; set; }
        public BoxDto Box { get; set; }
    }



    public class DailyBoxReportDto
    {


        public DateTime DailyDate { get; set; }
        public string Name { get; set; } = "";



        //كسب عمل
        public decimal TotalTaxNormal { get; set; }
        //دمغه عاديه
        public decimal TotalStamp { get; set; }
        //تسويه ضريبيه
        public decimal TotalTaxsettlement { get; set; }
        // ضريبه اضافيه
        public decimal TotalTax2 { get; set; }
        //اتساع او متنوعه
        public decimal TotalOther { get; set; }

        public decimal TotalSumTax { get; set; }
        // تنميه
        public decimal TotalTaxDevelopment { get; set; }


        public ICollection<FormReportDto> data { get; set; }
    }

    public class FormReportDto
    {
        public string Num224 { get; set; } = "";
        //كسب عمل
        public decimal TaxNormal { get; set; }
        //دمغه عاديه
        public decimal Stamp { get; set; }
        //تسويه ضريبيه
        public decimal Taxsettlement { get; set; }
        // ضريبه اضافيه
        public decimal Tax2 { get; set; }
        //اتساع او متنوعه
        public decimal Other { get; set; }

        public decimal SumTax { get; set; }
        // تنميه
        public decimal TaxDevelopment { get; set; }

        public string Collage { get; set; }
        public string Box { get; set; }


    }
}