using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApiZaliczenie
{
    public class SavedActivities
    {

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
