using Application.Dto;
using Application.Property.Queries;
using AutoMapper;
using Duende.IdentityServer.EntityFramework.Options;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Application.UnitTests.Property.Queries
{
    [TestFixture]
    public class GetPropertiesQueryTest
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;
        private GetPropertiesQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var operationalStoreOptions = Options.Create(new OperationalStoreOptions());
            _context = new ApplicationDbContext(options, operationalStoreOptions, null);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Entities.Property, PropertyDto>();
                cfg.CreateMap<Domain.Entities.PropertyTrace, PropertyTraceDto>();
                cfg.CreateMap<Domain.Entities.PropertyImage, PropertyImageDto>();
            });

            _mapper = configuration.CreateMapper();

            _handler = new GetPropertiesQueryHandler(_context, _mapper);
        }


        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task Handle_WithoutFilters_ReturnsAllProperties()
        {
            var properties = new List<Domain.Entities.Property>()
            {
                new Domain.Entities.Property {Name = "Property 1", Address = "123 Main St", Price = 100000, Year = 2020, CodeInternal = "ABC123"},
                new Domain.Entities.Property {Name = "Property 2", Address = "456 Elm St", Price = 200000, Year = 2021, CodeInternal = "ABC123"},
                new Domain.Entities.Property {Name = "Property 3", Address = "789 Oak St", Price = 300000, Year = 2022, CodeInternal = "ABC123"},
                new Domain.Entities.Property {Name = "Property 4", Address = "321 Pine St", Price = 150000, Year = 2019, CodeInternal = "DEF456"},
                new Domain.Entities.Property {Name = "Property 5", Address = "654 Cedar Ave", Price = 250000, Year = 2017, CodeInternal = "DEF456"}
            };

            _context.Property.AddRange(properties);
            _context.SaveChanges();

            var query = new GetPropertiesQuery();
            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items.Count, Is.EqualTo(properties.Count));

            foreach(var property in properties)
            {
                Assert.IsTrue(result.Items.Any(p=> p.Name.Contains(property.Name) && p.Address.Contains(property.Address) && p.Price == property.Price && p.Year == property.Year));
            }   
        }

        [Test]
        public async Task Handle_WithFilters_ReturnsFilteredProperties()
        {
            var properties = new List<Domain.Entities.Property>
            {
                new Domain.Entities.Property { Name = "Property 1", Address = "123 Main St", Price = 100000, Year = 2020, CodeInternal = "ABC123" },
                new Domain.Entities.Property { Name = "Property 2", Address = "456 Elm St", Price = 200000, Year = 2021, CodeInternal = "ABC123" },
                new Domain.Entities.Property { Name = "Property 3", Address = "789 Oak St", Price = 300000, Year = 2022, CodeInternal = "ABC123" }
            };

            _context.Property.AddRange(properties);
            _context.SaveChanges();

            var query = new GetPropertiesQuery
            {
                Name = "Property 2",
                Year = 2021
            };

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.That(result.Items.Count, Is.EqualTo(1));
            Assert.That(result.Items[0].Name, Is.EqualTo("Property 2"));
            Assert.That(result.Items[0].Address, Is.EqualTo("456 Elm St"));
            Assert.That(result.Items[0].Price, Is.EqualTo(200000));
            Assert.That(result.Items[0].Year, Is.EqualTo(2021));
        }
    }
}
