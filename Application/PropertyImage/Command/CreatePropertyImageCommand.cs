using Application.Common.Interfaces;
using MediatR;

namespace Application.PropertyImage.Command;

public class CreatePropertyImageCommand : IRequest<string>
{
    public int IdProperty { get; set; }

    public string File { get; set; }

    public bool Enabled { get; set; }
}

public class CreatePropertyImageHandler : IRequestHandler<CreatePropertyImageCommand, string>
{
    private readonly IApplicationDbContext _context;

    public CreatePropertyImageHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreatePropertyImageCommand command, CancellationToken cancellationToken)
    {
        Domain.Entities.PropertyImage propertyImage = new Domain.Entities.PropertyImage(); ;

        propertyImage.IdProperty = command.IdProperty;
        propertyImage.File = command.File;
        propertyImage.Enabled = command.Enabled;

        _context.PropertyImage.Add(propertyImage);
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result.ToString();
    }
}
