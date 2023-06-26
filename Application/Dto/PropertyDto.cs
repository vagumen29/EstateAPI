using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;


namespace Application.Dto;

public class PropertyDto : IMapFrom<Domain.Entities.Property>
{
    public int IdProperty { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public decimal Price { get; set; }

    public int Year { get; set; }

    public string OwnerName { get; set; }

    public List<string>? PropertyPhotos { get; set; }

    public List<PropertyTraceDto>? PropertyTraces { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.Property, PropertyDto>()
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.IdOwnerNavigation.Name))
            .ForMember(dest => dest.PropertyPhotos, opt => opt.MapFrom(src => src.PropertyImages.Select(e => e.File).ToList()))
            .ForMember(dest => dest.PropertyTraces, opt => opt.MapFrom(src => src.PropertyTraces.ToList()));

     
    }
}
