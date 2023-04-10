
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Mechatronica.DB.Models
{
    [Table("Persons", Schema = "Saratov")]
    public class PersonDbModel
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("Person")]
        public string? Name { get; set; }

        [Column("Date")]
        public string? Date { get; set; }
    }
}
