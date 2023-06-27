using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Dto;
using Application.Property.Commands.UpdateProperty;
using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Property.Commands.UpdateProperty;

public class UpdatePropertyCommandHandlerTests
{

    private Mock<IApplicationDbContext> _contextMock;
    private IMapper _mapper;
    private UpdatePropertyCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _contextMock = new Mock<IApplicationDbContext>();
        var configuration = new MapperConfiguration(cfg => {
            cfg.CreateMap<Domain.Entities.Property, PropertyDto>();
        });
        _mapper = configuration.CreateMapper();

        _handler = new UpdatePropertyCommandHandler(_contextMock.Object, _mapper);
    }

    [Test]
    public async Task Handle_ExistingProperty()
    {
        var command = new UpdatePropertyCommand
        {
            IdProperty = 1,
            Name = "Updated Property",
            Address = "Bulevar Elm St",
            Price = 200000,
            Year = 2022,
            IdOwner = 1
        };

        var existingProperty = new Domain.Entities.Property
        {
            IdProperty = 1,
            Name = "Old Property",
            Address = "Thomson St",
            Price = 100000,
            Year = 2021,
            IdOwner = 1
        };

        _contextMock.Setup(c => c.Property.FindAsync(new object[] { command.IdProperty }, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingProperty);

        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.IsNotNull(result);
        Assert.Multiple(() =>
        {
            Assert.That(result.IdProperty, Is.EqualTo(command.IdProperty));
            Assert.That(result.Name, Is.EqualTo(command.Name));
            Assert.That(result.Address, Is.EqualTo(command.Address));
            Assert.That(result.Price, Is.EqualTo(command.Price));
            Assert.That(result.Year, Is.EqualTo(command.Year));
        });
        _contextMock.Verify(c => c.Property.FindAsync(new object[] { command.IdProperty }, It.IsAny<CancellationToken>()), Times.Once);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void Handle_NonExistingPropertyn()
    {
        var command = new UpdatePropertyCommand
        {
            IdProperty = 1,
        };

        _contextMock.Setup(c => c.Property.FindAsync(new object[] { command.IdProperty }, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Domain.Entities.Property)null);

        Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(command, CancellationToken.None));

        _contextMock.Verify(c => c.Property.FindAsync(new object[] { command.IdProperty }, It.IsAny<CancellationToken>()), Times.Once);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
