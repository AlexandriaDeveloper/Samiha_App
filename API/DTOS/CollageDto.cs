using System.ComponentModel.DataAnnotations;

namespace API.DTOS
{
    public class CollageDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}