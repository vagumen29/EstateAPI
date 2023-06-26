using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Property.Queries;

public class GetPropertiesQuery : IRequest<PaginatedList<PropertyDto>>
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public decimal? Price { get; set; }
    public int? Year { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, PaginatedList<PropertyDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPropertiesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PaginatedList<PropertyDto>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Property.AsQueryable();

        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(p => p.Name.Contains(request.Name));
        }

        if (!string.IsNullOrEmpty(request.Address))
        {
            query = query.Where(p => p.Address.Contains(request.Address));
        }

        if (request.Price.HasValue)
        {
            query = query.Where(p => p.Price == request.Price);
        }

        if (request.Year.HasValue)
        {
            query = query.Where(p => p.Year == request.Year);
        }


        var result = await query.AsNoTracking().ProjectTo<PropertyDto>(_mapper.ConfigurationProvider).PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);

        return result;
    }
}
