using Application.Common.Mappings;

namespace Application.Dto;

public class PropertyImageDto : IMapFrom<Domain.Entities.PropertyImage>
{
    public string File { get; set; }

    public bool Enabled { get; set; }
}
