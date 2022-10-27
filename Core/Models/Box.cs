namespace Core.Models
{
    public class Box : BaseEntity
    {

        public int CollageId { get; set; }
        public Collage Collage { get; set; }
        public ICollection<DailyBoxes> DailyBoxes { get; set; }



    }
}