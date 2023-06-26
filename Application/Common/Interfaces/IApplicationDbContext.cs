using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Domain.Entities.Owner> Owner { get; set; }
    DbSet<Domain.Entities.Property> Property { get; set; }
    DbSet<Domain.Entities.PropertyImage> PropertyImage { get; set; }
    DbSet<Domain.Entities.PropertyTrace> PropertyTrace { get; set; }
  
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

