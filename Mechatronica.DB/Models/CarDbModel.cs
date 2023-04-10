
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

        [Column("name")]
        public string? Name { get; set; }

        [Column("date")]
        public string? Date { get; set; }
    }
}
