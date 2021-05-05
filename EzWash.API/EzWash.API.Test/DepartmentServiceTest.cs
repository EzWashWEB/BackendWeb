using System.Collections.Generic;
using System.Threading.Tasks;
using EzWash.API.Domain.Models;
using EzWash.API.Domain.Persistence.Repositories;
using EzWash.API.Domain.Services.Communications;
using EzWash.API.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace EzWash.API.Test
{
    public class DepartmentServiceTest
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public async Task GetByIdAsyncWhenNoDepartmentFoundReturnsDepartmentNotFoundResponse()
        {
            //Arrange
            var mockDepartmentRepository = GetDefaultIDepartmentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var departmentId = 1;
            mockDepartmentRepository.Setup(r => r.FindById(departmentId))
                .Returns(Task.FromResult<Department>(null));
            var service = new DepartmentService(mockDepartmentRepository.Object, mockUnitOfWork.Object);
            
            //Act
            DepartmentResponse result = await service.GetByIdAsync(departmentId);
            var message = result.Message;

            //Assert
            message.Should().Be("Department not found");
        }
        
        [Test]
        public async Task GetByIdAsyncWhenDepartmentFoundReturnsDepartmentResponse()
        {
            //Arrange
            var mockDepartmentRepository = GetDefaultIDepartmentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var departmentId = 1;
            var mockDepartment = new Mock<Department>();
            mockDepartment.Object.Id = 1;
            mockDepartment.Object.Name = "Loreto";
            mockDepartment.Object.Provinces = new List<Province>();
            mockDepartmentRepository.Setup(r => r.FindById(departmentId))
                .Returns(Task.FromResult(mockDepartment.Object));
            var service = new DepartmentService(mockDepartmentRepository.Object, mockUnitOfWork.Object);
            
            //Act
            DepartmentResponse result = await service.GetByIdAsync(departmentId);
            var id = result.Resource.Id;
            var name = result.Resource.Name;

            //Assert
            id.Should().Be(1);
            name.Should().Be("Loreto");
        }

        [Test]
        public async Task SaveAsyncWhenDepartmentSavedReturnsDepartmentResponse()
        {
            //Arrange
            var mockDepartmentRepository = GetDefaultIDepartmentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockDepartment = new Mock<Department>();
            mockDepartment.Object.Id = 1;
            mockDepartment.Object.Name = "Tumbes";
            mockDepartment.Object.Provinces = new List<Province>();
            mockDepartmentRepository.Setup(r => r.AddAsync(mockDepartment.Object))
                .Returns(Task.FromResult(mockDepartment.Object));
            var service = new DepartmentService(mockDepartmentRepository.Object, mockUnitOfWork.Object);
            
            //Act
            DepartmentResponse result = await service.SaveAsync(mockDepartment.Object);
            var id = result.Resource.Id;
            var name = result.Resource.Name;

            //Assert
            id.Should().Be(1);
            name.Should().Be("Tumbes");
        }
        private Mock<IDepartmentRepository> GetDefaultIDepartmentRepositoryInstance() => new();
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance() => new();
    }
}