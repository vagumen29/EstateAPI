using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;


public class PropertyImage: BaseAuditableEntity
{
    [Key]
    public int IdPropertyImage { get; set; }

    public int IdProperty { get; set; }

    public string File { get; set; } = null!;

    public bool Enabled { get; set; }

    [ForeignKey(nameof(IdProperty))]
    public virtual Property IdPropertyNavigation { get; set; } = null!;
}
