namespace Core.Models
{
    public class Collage : BaseEntity
    {

        public ICollection<Box> Boxes { get; set; }



    }
}