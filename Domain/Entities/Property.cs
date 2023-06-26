using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Property: BaseAuditableEntity
{
    [Key]
    public int IdProperty { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public decimal Price { get; set; }

    public string CodeInternal { get; set; }

    public int Year { get; set; }


    public int IdOwner { get; set; }

    [ForeignKey(nameof(IdOwner))]

    public virtual Owner IdOwnerNavigation { get; set; } = null!;

    public virtual ICollection<PropertyImage> PropertyImages { get; set; } = new List<PropertyImage>();

    public virtual ICollection<PropertyTrace> PropertyTraces { get; set; } = new List<PropertyTrace>();
}
