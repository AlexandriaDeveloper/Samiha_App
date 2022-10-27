using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOS
{
    public class FormDto
    {
        public int? Id { get; set; }

        public string Name { get; set; } = "";
        [Required]
        [MaxLength(6)]
        public string Num224 { get; set; } = "";
        [Required]
        public int DailyBoxId { get; set; }


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
    }


    public class FormListDto
    {
        public FormListDto(IReadOnlyList<FormDto> forms)
        {
            this.Items = forms;
        }
        public IReadOnlyList<FormDto> Items { get; set; }
        //كسب عمل
        public decimal TaxNormal { get { return Items.Sum(x => x.TaxNormal); } }
        //دمغه عاديه
        public decimal Stamp { get { return Items.Sum(x => x.Stamp); } }
        //تسويه ضريبيه
        public decimal Taxsettlement { get { return Items.Sum(x => x.Taxsettlement); } }
        // ضريبه اضافيه
        public decimal Tax2 { get { return Items.Sum(x => x.Tax2); } }
        //اتساع او متنوعه
        public decimal Other { get { return Items.Sum(x => x.Other); } }

        public decimal SumTax { get { return Items.Sum(x => x.SumTax); } }
        // تنميه
        public decimal TaxDevelopment { get { return Items.Sum(x => x.TaxDevelopment); } }



        public string Title { get; set; }
        public string Collage { get; set; }
        public string BoxName { get; set; }
    }
}