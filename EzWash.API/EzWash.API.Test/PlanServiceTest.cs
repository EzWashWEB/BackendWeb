using NUnit.Framework;
using Moq;
using FluentAssertions;
using EzWash.API.Domain.Models;
using EzWash.API.Domain.Services.Communications;
using EzWash.API.Domain.Persistence.Repositories;
using EzWash.API.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EzWash.API.Test
{
    public class PlanServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoPlanFoundReturnsPlanNotFoundResponse()
        {
            // Arrange
            var mockPlanRepository = GetDefaultIPlanRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            mockPlanRepository.Setup(r => r.FindById(0))
                .Returns(Task.FromResult<Plan>(null));

            var service = new PlanService(mockPlanRepository.Object, mockUnitOfWork.Object);

            // Act
            PlanResponse result = await service.GetByIdAsync(0);
            var message = result.Message;
            // Assert
            message.Should().Be("Plan not found");
        }

        private Mock<IPlanRepository> GetDefaultIPlanRepositoryInstance()
        {
            return new Mock<IPlanRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}