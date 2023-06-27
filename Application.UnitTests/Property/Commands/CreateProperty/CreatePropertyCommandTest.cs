using Application.Common.Interfaces;
using Application.Dto;
using Application.Property.Commands.CreateProperty;
using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Property.Commands.CreateProperty;

[TestFixture]
public class CreatePropertyCommandTest
{
    private Mock<IApplicationDbContext> _contextMock;
    private IMapper _mapper;
    private CreatePropertyCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _contextMock = new Mock<IApplicationDbContext>();
        #region Mapper
        var configuration = new MapperConfiguration(cfg => {
            cfg.CreateMap<Domain.Entities.Property, PropertyDto>();
            cfg.CreateMap<Domain.Entities.PropertyTrace, PropertyTraceDto>();
            cfg.CreateMap<Domain.Entities.PropertyImage, PropertyImageDto>();
        });

        _mapper = configuration.CreateMapper();
        #endregion


        _handler = new CreatePropertyCommandHandler(_contextMock.Object, _mapper);
    }

    [Test]
    public async Task Handle_ValidCommand_ReturnsPropertyDto()
    {
        // Arrange
        var command = new CreatePropertyCommand
        {
            Name = "Property 3",
            Address = "678 San Patrick St",
            Price = 100000,
            CodeInternal = "DZX586",
            Year = 2021,
            Owner = new OwnerDto
            {
                Name = "Tim Ruiz",
                Address = "098 Ronkonkoma St",
                Birthday = DateTime.Now,
                Photo = "image1.jpg"
            },
            PropertyImages = new List<PropertyImageDto>
                {
                    new PropertyImageDto
                    {
                        File = "image1.jpg",
                        Enabled = true
                    },
                    new PropertyImageDto
                    {
                        File = "image2.jpg",
                        Enabled = false
                    }
                },
            PropertyTraces = new List<PropertyTraceDto>
                {
                    new PropertyTraceDto
                    {
                        DateSale = new DateTime(2021, 1, 1),
                        Name = "Sale 1",
                        Value = 120000,
                        Tax = 10
                    },
                    new PropertyTraceDto
                    {
                        DateSale = new DateTime(2021, 2, 1),
                        Name = "Sale 2",
                        Value = 450000,
                        Tax = 12
                    }
                }
        };

        var entity = new Domain.Entities.Property();

        _contextMock.Setup(c => c.Property.Add(entity));

        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.IsNotNull(result);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
