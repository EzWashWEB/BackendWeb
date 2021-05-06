using EzWash.API.Domain.Models;
using EzWash.API.Domain.Persistence.Repositories;
using EzWash.API.Domain.Services.Communications;
using EzWash.API.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzWash.API.Test
{
    public class BenefitServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoBenefitFoundReturnsBenefitNotFoundResponse()
        {
            // Arrange
            var mockBenefitRepository = GetDefaultIBenefitRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            mockBenefitRepository.Setup(r => r.FindById(0))
                .Returns(Task.FromResult<Benefit>(null));

            var service = new BenefitService(mockBenefitRepository.Object, mockUnitOfWork.Object);

            // Act
            BenefitResponse result = await service.GetByIdAsync(0);
            var message = result.Message;
            // Assert
            message.Should().Be("Benefit not found");
        }

        private Mock<IBenefitRepository> GetDefaultIBenefitRepositoryInstance()
        {
            return new Mock<IBenefitRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
