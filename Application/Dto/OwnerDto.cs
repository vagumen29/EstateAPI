using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dto;

public class OwnerDto: IMapFrom<Owner>
{
    public string Name { get; set; }

    public string Address { get; set; }

    public string Photo { get; set; }    

    public DateTime Birthday { get; set; }
}
