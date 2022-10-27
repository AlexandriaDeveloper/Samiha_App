using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Daily : BaseEntity
    {
        // [Column(TypeName = "Date")]
        [Required]

        public DateTime DailyDate { get; set; }


        [NotMapped]

        public decimal? Total
        {
            get
            {
                return DailyBoxes == null ? 0 : DailyBoxes.Sum(p => p.Total);
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
                return DailyBoxes == null ? 0 : DailyBoxes.Sum(p => p.TotalTaxDevelopment);
            }
            private set
            {

            }
        }

        public ICollection<DailyBoxes> DailyBoxes { get; set; }
    }
}