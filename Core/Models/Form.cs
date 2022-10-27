using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Form : BaseEntity
    {

        public string Num224 { get; set; } = "";

        [Required]
        public int DailyBoxId { get; set; }

        //كسب عمل
        [DefaultValue(0)]
        public decimal TaxNormal { get; set; }
        //دمغه عاديه
        [DefaultValue(0)]
        public decimal Stamp { get; set; }
        //تسويه ضريبيه
        [DefaultValue(0)]
        public decimal Taxsettlement { get; set; }
        // ضريبه اضافيه
        [DefaultValue(0)]
        public decimal Tax2 { get; set; }
        //اتساع او متنوعه
        [DefaultValue(0)]
        public decimal Other { get; set; }
        [DefaultValue(0)]
        public decimal SumTax { get; set; }
        // تنميه
        [DefaultValue(0)]
        public decimal TaxDevelopment { get; set; }


        public DailyBoxes DailyBoxes { get; set; }


    }
}