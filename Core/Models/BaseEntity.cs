using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; } = "";

        public DateTime? CreatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}