using System.ComponentModel.DataAnnotations;

namespace API.DTOS
{
    public class BoxDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CollageId { get; set; }

        //  public CollageDto Collage { get; set; }
    }
}