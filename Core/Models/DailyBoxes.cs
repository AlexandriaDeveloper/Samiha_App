using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class DailyBoxes : BaseEntity
    {
        [Required]
        public int BoxId { get; set; }
        [Required]
        public int DailyId { get; set; }

        [NotMapped]

        public decimal? Total
        {
            get
            {
                return Forms == null ? 0 : Forms.Sum(p => p.SumTax);
            }
            private set
            {

            }
        }
        [NotMapped]

        public decimal? TotalTaxDevelopment
        {
            get
            {
                return Forms == null ? 0 : Forms.Sum(p => p.TaxDevelopment);
            }
            private set
            {

            }
        }



        public Box Box { get; set; }
        public Daily Daily { get; set; }

        public ICollection<Form> Forms { get; set; }
    }
}