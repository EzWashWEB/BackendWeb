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
    public class ProvinceServiceTest
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public async Task GetByDepartmentIdAndIdAsyncWhenNoDepartmentFoundReturnsDepartmentNotFoundResponse()
        {
            //Arrange
            var mockDepartmentRepository = GetDefaultIDepartmentRepositoryInstance();
            var mockProvinceRepository = GetDefaultIProvinceRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var departmentId = 1;
            var provinceId = 1;
            mockDepartmentRepository.Setup(r => r.FindById(departmentId))
                .Returns(Task.FromResult<Department>(null));
            mockProvinceRepository.Setup(r => r.FindById(provinceId))
                .Returns(Task.FromResult<Province>(null));
            var service = new ProvinceService(mockDepartmentRepository.Object, mockProvinceRepository.Object, mockUnitOfWork.Object);
            //Act
            ProvinceResponse result = await service.GetByDepartmentIdAndIdAsync(departmentId, provinceId);
            var message = result.Message;

            //Assert
            message.Should().Be("Department not found");
        }
        
        [Test]
        public async Task GetByDepartmentIdAndIdAsyncWhenNoProvinceFoundReturnsProvinceNotFoundResponse()
        {
            //Arrange
            var mockDepartmentRepository = GetDefaultIDepartmentRepositoryInstance();
            var mockProvinceRepository = GetDefaultIProvinceRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var departmentId = 1;
            var provinceId = 1;
            var mockDepartment = new Mock<Department>();
            mockDepartment.Object.Id = 1;
            mockDepartment.Object.Name = "Loreto";
            mockDepartment.Object.Provinces = new List<Province>();
            mockDepartmentRepository.Setup(r => r.FindById(departmentId))
                .Returns(Task.FromResult(mockDepartment.Object));
            mockProvinceRepository.Setup(r => r.FindById(provinceId))
                .Returns(Task.FromResult<Province>(null));
            var service = new ProvinceService(mockDepartmentRepository.Object, mockProvinceRepository.Object, mockUnitOfWork.Object);
            //Act
            ProvinceResponse result = await service.GetByDepartmentIdAndIdAsync(departmentId, provinceId);
            var message = result.Message;

            //Assert
            message.Should().Be("Province not found");
        }
        
        [Test]
        public async Task GetByDepartmentIdAndIdAsyncWhenDepartmentAndProvinceFoundReturnsProvinceResponse()
        {
            //Arrange
            var mockDepartmentRepository = GetDefaultIDepartmentRepositoryInstance();
            var mockProvinceRepository = GetDefaultIProvinceRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var departmentId = 1;
            var provinceId = 1;
            var mockDepartment = new Mock<Department>();
            mockDepartment.Object.Id = 1;
            mockDepartment.Object.Name = "Loreto";
            mockDepartment.Object.Provinces = new List<Province>();
            var mockProvince = new Mock<Province>();
            mockProvince.Object.Id = 1;
            mockProvince.Object.Name = "Iquitos";
            mockProvince.Object.DepartmentId = 1;
            mockProvince.Object.Department = mockDepartment.Object;
            mockDepartmentRepository.Setup(r => r.FindById(departmentId))
                .Returns(Task.FromResult(mockDepartment.Object));
            mockProvinceRepository.Setup(r => r.FindById(provinceId))
                .Returns(Task.FromResult(mockProvince.Object));
            var service = new ProvinceService(mockDepartmentRepository.Object, mockProvinceRepository.Object, mockUnitOfWork.Object);
            //Act
            ProvinceResponse result = await service.GetByDepartmentIdAndIdAsync(departmentId, provinceId);
            var id = result.Resource.Id;
            var name = result.Resource.Name;
            var resourceDepartmentId = result.Resource.DepartmentId;
            var resourceDepartment = result.Resource.Department;

            //Assert
            id.Should().Be(1);
            name.Should().Be("Iquitos");
            resourceDepartmentId.Should().Be(1);
            resourceDepartment.Should().Be(mockDepartment.Object);
        }
        
        private Mock<IDepartmentRepository> GetDefaultIDepartmentRepositoryInstance() => new();
        private Mock<IProvinceRepository> GetDefaultIProvinceRepositoryInstance() => new();
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance() => new();
    }
}