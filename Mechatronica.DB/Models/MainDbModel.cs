
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Mechatronica.DB.Models
{
    [Table("Main", Schema = "Saratov")]
    public class MainDbModel
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("Person")]
        public string? Person { get; set; }

        [Column("Car")]
        public string? Car { get; set; }

        [Column("Date")]
        public string? Date { get; set; }
    }
}
