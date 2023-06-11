using Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Domains
{
    [Table("Category", Schema = "dbo")]
    public class Category : BaseEntity
    {
        public string Name { get; set; }
    }
}