using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;


public class Owner: BaseAuditableEntity
{
    [Key]
    public int IdOwner { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string? Photo { get; set; }

    public DateTime? Birthday { get; set; }

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
