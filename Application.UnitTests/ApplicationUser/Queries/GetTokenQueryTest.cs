using Application.ApplicationUser.Queries.GetToken;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Dto;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.ApplicationUser.Queries
{
    [TestFixture]
    public class GetTokenQueryTest
    {
        private Mock<IIdentityService> _identityServiceMock;
        private Mock<ITokenService> _tokenServiceMock;
        private GetTokenQueryHandler _handler;

        [SetUp]
        public void SetUp() 
        {
            _identityServiceMock= new Mock<IIdentityService>();
            _tokenServiceMock= new Mock<ITokenService>();
            _handler = new GetTokenQueryHandler(_tokenServiceMock.Object, _identityServiceMock.Object);            
        }  

        [Test]
        public async Task Handle_ValidUser_ReturnsLoginDto()
        {
            // Arrange
            var email = "test@example.com";
            var password = "password";

            ApplicationUserDto userDto = new ApplicationUserDto
            {
                Email = "test@example.com",
                Id = "1",
                UserName = "Test"
            };

            var user = new LoginDto
            {
                User = userDto,
                Token = "dummyToken"                
            };
            
            _identityServiceMock.Setup(s => s.CheckUserPassword(email, password))
                .ReturnsAsync(userDto);
          
            _tokenServiceMock.Setup(s => s.CreateJwtToken(userDto.Id))
                .Returns("dummyToken");

            var query = new GetTokenQuery
            {
                Email = email,
                Password = password
            };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.User, Is.EqualTo(userDto));
            Assert.That(result.Token, Is.EqualTo("dummyToken"));         
        }

        [Test]
        public void Handle_InvalidUser_ThrowsForbiddenAccessException()
        {
            // Arrange
            var email = "test@example.com";
            var password = "password";
         
            _identityServiceMock.Setup(s => s.CheckUserPassword(email, password))
                .ReturnsAsync((ApplicationUserDto)null); 

            var tokenServiceMock = new Mock<ITokenService>();

            var query = new GetTokenQuery
            {
                Email = email,
                Password = password
            };

            // Act & Assert
            Assert.ThrowsAsync<ForbiddenAccessException>(async () => await _handler.Handle(query, CancellationToken.None));
        }
    }
}
