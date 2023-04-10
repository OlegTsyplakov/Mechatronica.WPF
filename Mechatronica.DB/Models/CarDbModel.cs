
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Mechatronica.DB.Models
{
    [Table("Cars", Schema = "Saratov")]
    public class CarDbModel
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("Car")]
        public string? CarName { get; set; }

        [Column("Date")]
        public string? Date { get; set; }
    }
}
