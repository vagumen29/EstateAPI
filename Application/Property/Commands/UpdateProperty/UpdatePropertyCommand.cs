using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Dto;
using AutoMapper;
using MediatR;

namespace Application.Property.Commands.UpdateProperty;

public class UpdatePropertyCommand : IRequest<PropertyDto>
{
    public int IdProperty { get; set; }
    public decimal Price { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int Year { get; set; }
    public int IdOwner { get; set; }
}
public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, PropertyDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdatePropertyCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PropertyDto> Handle(UpdatePropertyCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Property
            .FindAsync(new object[] { command.IdProperty }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Property), cancellationToken);
        }

        entity.Name = command.Name;
        entity.Address = command.Address;
        entity.Price = command.Price;
        entity.Year = command.Year;
        entity.IdOwner = command.IdOwner;

        _context.Property.Update(entity);
        var result = await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PropertyDto>(entity);
    }
}
