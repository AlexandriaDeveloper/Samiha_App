using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOS
{
    public class DailyDto
    {

        public int? Id { get; set; }
        [Required]
        [MinLength(3), MaxLength(200)]
        public string Name { get; set; } = "";
        [Required]
        //    [Column(TypeName = "Date")]
        public DateTime DailyDate { get; set; }
        public decimal TaxNormal { get; set; }
        public decimal Stamp { get; set; }
        public decimal Taxsettlement { get; set; }
        public decimal Tax2 { get; set; }
        public decimal Other { get; set; }
        public decimal Total { get; set; }
        public decimal TotalDevelopment { get; set; }

        //  public ICollection<DailyBoxDto> DailyBoxes { get; set; }
    }





}