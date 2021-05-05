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
    public class DistrictServiceTest
    {
        [SetUp]
        public void Setup()
        {
            
        }
        
        [Test]
        public async Task GetByDepartmentIdAndProvinceIdAndIdAsyncWhenNoDepartmentFoundReturnsDepartmentNotFoundResponse()
        {
            //Arrange
            var mockDepartmentRepository = GetDefaultIDepartmentRepositoryInstance();
            var mockProvinceRepository = GetDefaultIProvinceRepositoryInstance();
            var mockDistrictRepository = GetDefaultIDistrictRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var departmentId = 1;
            var provinceId = 1;
            var districtId = 1;
            mockDepartmentRepository.Setup(r => r.FindById(departmentId))
                .Returns(Task.FromResult<Department>(null));
            mockProvinceRepository.Setup(r => r.FindById(provinceId))
                .Returns(Task.FromResult<Province>(null));
            mockDistrictRepository.Setup(r => r.FindById(districtId))
                .Returns(Task.FromResult<District>(null));
            var service = new DistrictService(mockDepartmentRepository.Object, mockProvinceRepository.Object, mockDistrictRepository.Object, mockUnitOfWork.Object);
            //Act
            DistrictResponse result = await service.GetByDepartmentIdAndProvinceIdAndId(departmentId, provinceId, districtId);
            var message = result.Message;

            //Assert
            message.Should().Be("Department not found");
        }
        
        [Test]
        public async Task GetByDepartmentIdAndProvinceIdAndIdAsyncWhenNoProvinceFoundReturnsProvinceNotFoundResponse()
        {
            //Arrange
            var mockDepartmentRepository = GetDefaultIDepartmentRepositoryInstance();
            var mockProvinceRepository = GetDefaultIProvinceRepositoryInstance();
            var mockDistrictRepository = GetDefaultIDistrictRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var departmentId = 1;
            var provinceId = 1;
            var districtId = 1;
            var mockDepartment = new Mock<Department>();
            mockDepartment.Object.Id = 1;
            mockDepartment.Object.Name = "Loreto";
            mockDepartment.Object.Provinces = new List<Province>();
            mockDepartmentRepository.Setup(r => r.FindById(departmentId))
                .Returns(Task.FromResult(mockDepartment.Object));
            mockProvinceRepository.Setup(r => r.FindById(provinceId))
                .Returns(Task.FromResult<Province>(null));
            mockDistrictRepository.Setup(r => r.FindById(districtId))
                .Returns(Task.FromResult<District>(null));
            var service = new DistrictService(mockDepartmentRepository.Object, mockProvinceRepository.Object, mockDistrictRepository.Object, mockUnitOfWork.Object);
            //Act
            DistrictResponse result = await service.GetByDepartmentIdAndProvinceIdAndId(departmentId, provinceId, districtId);
            var message = result.Message;

            //Assert
            message.Should().Be("Province not found");
        }
        
        [Test]
        public async Task GetByDepartmentIdAndProvinceIdAndIdAsyncWhenNoDistrictFoundReturnsDistrictNotFoundResponse()
        {
            //Arrange
            var mockDepartmentRepository = GetDefaultIDepartmentRepositoryInstance();
            var mockProvinceRepository = GetDefaultIProvinceRepositoryInstance();
            var mockDistrictRepository = GetDefaultIDistrictRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var departmentId = 1;
            var provinceId = 1;
            var districtId = 1;
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
            mockDistrictRepository.Setup(r => r.FindById(districtId))
                .Returns(Task.FromResult<District>(null));
            var service = new DistrictService(mockDepartmentRepository.Object, mockProvinceRepository.Object, mockDistrictRepository.Object, mockUnitOfWork.Object);
            //Act
            DistrictResponse result = await service.GetByDepartmentIdAndProvinceIdAndId(departmentId, provinceId, districtId);
            var message = result.Message;

            //Assert
            message.Should().Be("District not found");
        }
        
        [Test]
        public async Task GetByDepartmentIdAndProvinceIdAndIdAsyncWhenDistrictFoundReturnsDistrictResponse()
        {
            //Arrange
            var mockDepartmentRepository = GetDefaultIDepartmentRepositoryInstance();
            var mockProvinceRepository = GetDefaultIProvinceRepositoryInstance();
            var mockDistrictRepository = GetDefaultIDistrictRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var departmentId = 1;
            var provinceId = 1;
            var districtId = 1;
            var mockDepartment = new Mock<Department>();
            mockDepartment.Object.Id = 1;
            mockDepartment.Object.Name = "Loreto";
            mockDepartment.Object.Provinces = new List<Province>();
            var mockProvince = new Mock<Province>();
            mockProvince.Object.Id = 1;
            mockProvince.Object.Name = "Iquitos";
            mockProvince.Object.DepartmentId = 1;
            mockProvince.Object.Department = mockDepartment.Object;
            var mockDistrict = new Mock<District>();
            mockDistrict.Object.Id = 1;
            mockDistrict.Object.Name = "Belen";
            mockDistrict.Object.ProvinceId = 1;
            mockDistrict.Object.Province = mockProvince.Object;
            mockDepartmentRepository.Setup(r => r.FindById(departmentId))
                .Returns(Task.FromResult(mockDepartment.Object));
            mockProvinceRepository.Setup(r => r.FindById(provinceId))
                .Returns(Task.FromResult(mockProvince.Object));
            mockDistrictRepository.Setup(r => r.FindById(districtId))
                .Returns(Task.FromResult(mockDistrict.Object));
            var service = new DistrictService(mockDepartmentRepository.Object, mockProvinceRepository.Object, mockDistrictRepository.Object, mockUnitOfWork.Object);
            //Act
            DistrictResponse result = await service.GetByDepartmentIdAndProvinceIdAndId(departmentId, provinceId, districtId);
            var id = result.Resource.Id;
            var name = result.Resource.Name;
            var resourceProvinceId = result.Resource.ProvinceId;
            var resourceProvince = result.Resource.Province;

            //Assert
            id.Should().Be(1);
            name.Should().Be("Belen");
            resourceProvinceId.Should().Be(1);
            resourceProvince.Should().Be(mockProvince.Object);
        }
        
        private Mock<IDepartmentRepository> GetDefaultIDepartmentRepositoryInstance() => new();
        private Mock<IProvinceRepository> GetDefaultIProvinceRepositoryInstance() => new();
        private Mock<IDistrictRepository> GetDefaultIDistrictRepositoryInstance() => new();
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance() => new();
    }
}