using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;


public class PropertyTrace: BaseAuditableEntity
{
    [Key]
    public int IdPropertyTrace { get; set; }

    public DateTime DateSale { get; set; }

    public string Name { get; set; }

    public decimal Value { get; set; }

    public decimal Tax { get; set; }

    public int IdProperty { get; set; }

    [ForeignKey(nameof(IdProperty))]
    public virtual Property IdPropertyNavigation { get; set; } = null!;
}
