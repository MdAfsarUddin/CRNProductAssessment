using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRNProductAssessment.Domain.Entities;

[Table("Item")]
public partial class Item
{
    [Key]
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Items")]
    public virtual Product Product { get; set; } = null!;
}
