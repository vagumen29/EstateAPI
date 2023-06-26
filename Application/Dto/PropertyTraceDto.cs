using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto;

public class PropertyTraceDto : IMapFrom<PropertyTrace>
{
    public DateTime DateSale { get; set; }

    public string Name { get; set; }

    public decimal Value { get; set; }

    public decimal Tax { get; set; }
}
