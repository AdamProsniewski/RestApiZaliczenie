using System.ComponentModel.DataAnnotations;

namespace RestApiZaliczenie
{
    public class Activity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
