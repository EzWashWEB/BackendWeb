using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using EzWash.API.Domain.Models;
using EzWash.API.Domain.Persistence.Repositories;
using EzWash.API.Domain.Services;
using EzWash.API.Domain.Services.Communications;

namespace EzWash.API.Services
{
    public class ProvinceService: IProvinceService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProvinceRepository _provinceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProvinceService(IDepartmentRepository departmentRepository, IProvinceRepository provinceRepository, IUnitOfWork unitOfWork)
        {
            _departmentRepository = departmentRepository;
            _provinceRepository = provinceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Province>> ListByDepartmentIdAsync(int departmentId)
        {
            return await _provinceRepository.ListByDepartmentId(departmentId);
        }

        public async Task<ProvinceResponse> GetByDepartmentIdAndIdAsync(int departmentId, int id)
        {
            var existingDepartment = await _departmentRepository.FindById(departmentId);
            if (existingDepartment == null)
                return new ProvinceResponse("Department not found");
            
            var existingProvince = await _provinceRepository.FindById(id);
            if (existingProvince == null)
                return new ProvinceResponse("Province not found");
            return new ProvinceResponse(existingProvince);
        }

        public async Task<ProvinceResponse> SaveAsync(int departmentId, Province province)
        {
            var existingDepartment = await _departmentRepository.FindById(departmentId);
            if (existingDepartment == null)
                return new ProvinceResponse("Department not found");
            province.DepartmentId = departmentId;
            province.Department = existingDepartment;
            try
            {
                await _provinceRepository.AddAsync(province);
                await _unitOfWork.CompleteAsync();

                return new ProvinceResponse(province);
            }
            catch (Exception ex)
            {
                return new ProvinceResponse($"An error occurred while saving the province: {ex.Message}");
            }
        }

        public async Task<ProvinceResponse> UpdateAsync(int departmentId, int id, Province province)
        {
            var existingDepartment = await _departmentRepository.FindById(departmentId);
            if (existingDepartment == null)
                return new ProvinceResponse("Department not found");

            var existingProvince = await _provinceRepository.FindById(id);
            if (existingProvince == null)
                return new ProvinceResponse("Province not found");

            existingProvince.Name = province.Name;
            
            try
            {
                _provinceRepository.Update(existingProvince);
                await _unitOfWork.CompleteAsync();

                return new ProvinceResponse(existingProvince);
            }
            catch (Exception ex)
            {
                return new ProvinceResponse($"An error occurred while updating the province: {ex.Message}");
            }
        }

        public async Task<ProvinceResponse> DeleteAsync(int departmentId, int id)
        {
            var existingDepartment = await _departmentRepository.FindById(departmentId);
            if (existingDepartment == null)
                return new ProvinceResponse("Department not found");
            
            var existingProvince = await _provinceRepository.FindById(id);
            if (existingProvince == null)
                return new ProvinceResponse("Province not found");
            
            try
            {
                _provinceRepository.Delete(existingProvince);
                await _unitOfWork.CompleteAsync();

                return new ProvinceResponse(existingProvince);
            }
            catch (Exception ex)
            {
                return new ProvinceResponse($"An error occurred while deleting the province: {ex.Message}");
            }
        }
    }
}