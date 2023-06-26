using Application.Common.Interfaces;
using Application.Dto;
using AutoMapper;
using MediatR;

namespace Application.Property.Commands.CreateProperty;

public class CreatePropertyCommand : IRequest<PropertyDto>
{
    public string Name { get; set; }

    public string Address { get; set; }

    public decimal Price { get; set; }

    public string CodeInternal { get; set; }

    public int Year { get; set; }
    public OwnerDto Owner { get; set; }

    public List<PropertyImageDto> PropertyImages { get; set; }

    public List<PropertyTraceDto> PropertyTraces { get; set; }
}

public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, PropertyDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreatePropertyCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PropertyDto> Handle(CreatePropertyCommand command, CancellationToken cancellationToken)
    {
        Domain.Entities.Property entity = new Domain.Entities.Property();

        entity.Name = command.Name;
        entity.Address = command.Address;
        entity.Price = command.Price;
        entity.Year = command.Year;
        entity.CodeInternal = command.CodeInternal;

        entity.IdOwnerNavigation = new Domain.Entities.Owner
        {
            Name = command.Owner.Name,
            Address = command.Owner.Address,
            Birthday = command.Owner.Birthday,
            Photo = command.Owner.Photo
        };

        entity.PropertyImages = command.PropertyImages.Select(pi => new Domain.Entities.PropertyImage
        {
            File = pi.File,
            Enabled = pi.Enabled
        }).ToList();

        entity.PropertyTraces = command.PropertyTraces.Select(pt => new Domain.Entities.PropertyTrace
        {
            DateSale = pt.DateSale,
            Name = pt.Name,
            Value = pt.Value,
            Tax = pt.Tax
        }).ToList();

        _context.Property.Add(entity);
        var result = await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<PropertyDto>(entity);

    }
}
