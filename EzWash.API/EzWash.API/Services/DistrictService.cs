using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EzWash.API.Domain.Models;
using EzWash.API.Domain.Persistence.Repositories;
using EzWash.API.Domain.Services;
using EzWash.API.Domain.Services.Communications;

namespace EzWash.API.Services
{
    public class DistrictService: IDistrictService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProvinceRepository _provinceRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DistrictService(IDepartmentRepository departmentRepository, IProvinceRepository provinceRepository, IDistrictRepository districtRepository, IUnitOfWork unitOfWork)
        {
            _departmentRepository = departmentRepository;
            _provinceRepository = provinceRepository;
            _districtRepository = districtRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<District>> ListByProvinceIdAsync(int provinceId)
        {
            return await _districtRepository.ListByProvinceId(provinceId);
        }

        public async Task<DistrictResponse> GetByDepartmentIdAndProvinceIdAndId(int departmentId, int provinceId, int id)
        {
            var existingDepartment = await _departmentRepository.FindById(departmentId);
            if (existingDepartment == null)
                return new DistrictResponse("Department not found");
            
            var existingProvince = await _provinceRepository.FindById(provinceId);
            if (existingProvince == null)
                return new DistrictResponse("Province not found");

            var existingDistrict = await _districtRepository.FindById(id);
            if (existingDistrict == null)
                return new DistrictResponse("District not found");

            return new DistrictResponse(existingDistrict);
        }

        public async Task<DistrictResponse> SaveAsync(int departmentId, int provinceId, District district)
        {
            var existingDepartment = await _departmentRepository.FindById(departmentId);
            if (existingDepartment == null)
                return new DistrictResponse("Department not found");
            
            var existingProvince = await _provinceRepository.FindById(provinceId);
            if (existingProvince == null)
                return new DistrictResponse("Province not found");

            district.ProvinceId = provinceId;
            district.Province = existingProvince;

            try
            {
                await _districtRepository.AddAsync(district);
                await _unitOfWork.CompleteAsync();

                return new DistrictResponse(district);
            }
            catch (Exception ex)
            {
                return new DistrictResponse($"An error occurred while saving the district: {ex.Message}");
            }
        }

        public async Task<DistrictResponse> UpdateAsync(int departmentId, int provinceId, int id, District district)
        {
            var existingDepartment = await _departmentRepository.FindById(departmentId);
            if (existingDepartment == null)
                return new DistrictResponse("Department not found");
            
            var existingProvince = await _provinceRepository.FindById(provinceId);
            if (existingProvince == null)
                return new DistrictResponse("Province not found");

            var existingDistrict = await _districtRepository.FindById(id);
            if (existingDistrict == null)
                return new DistrictResponse("District not found");

            existingDistrict.Name = district.Name;
            
            try
            {
                _districtRepository.Update(existingDistrict);
                await _unitOfWork.CompleteAsync();

                return new DistrictResponse(existingDistrict);
            }
            catch (Exception ex)
            {
                return new DistrictResponse($"An error occurred while updating the district: {ex.Message}");
            }
        }

        public async Task<DistrictResponse> DeleteAsync(int departmentId, int provinceId, int id)
        {
            var existingDepartment = await _departmentRepository.FindById(departmentId);
            if (existingDepartment == null)
                return new DistrictResponse("Department not found");
            
            var existingProvince = await _provinceRepository.FindById(provinceId);
            if (existingProvince == null)
                return new DistrictResponse("Province not found");

            var existingDistrict = await _districtRepository.FindById(id);
            if (existingDistrict == null)
                return new DistrictResponse("District not found");
            
            
            try
            {
                _districtRepository.Delete(existingDistrict);
                await _unitOfWork.CompleteAsync();

                return new DistrictResponse(existingDistrict);
            }
            catch (Exception ex)
            {
                return new DistrictResponse($"An error occurred while deleting the district: {ex.Message}");
            }
        }
    }
}