using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GenericRepository.Domain.Abstract;

namespace GenericRepository.Domain
{
    [Table("user")]
    public class User : IEntity
    {
        [Key] [Column("id")] public int Id { get; set; }

        [Column("name")] public string Name { get; set; }

        [Column("status")] public bool Status { get; set; }
    }
}