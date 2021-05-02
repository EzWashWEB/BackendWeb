using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EzWash.API.Domain.Models;
using EzWash.API.Domain.Persistence.Repositories;
using EzWash.API.Domain.Services;
using EzWash.API.Domain.Services.Communications;

namespace EzWash.API.Services
{
    public class DepartmentService: IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
        {
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Department>> ListAsync()
        {
            return await _departmentRepository.ListAsync();
        }

        public async Task<DepartmentResponse> GetByIdAsync(int id)
        {
            var existingDepartment = await _departmentRepository.FindById(id);
            if (existingDepartment == null)
                return new DepartmentResponse("Department not found");
            return new DepartmentResponse(existingDepartment);
        }

        public async Task<DepartmentResponse> SaveAsync(Department department)
        {
            try
            {
                await _departmentRepository.AddAsync(department);
                await _unitOfWork.CompleteAsync();

                return new DepartmentResponse(department);
            }
            catch (Exception ex)
            {
                return new DepartmentResponse($"An error occurred while saving the department: {ex.Message}");
            }
        }

        public async Task<DepartmentResponse> UpdateAsync(int id, Department department)
        {
            var existingDepartment = await _departmentRepository.FindById(id);
            if (existingDepartment == null)
                return new DepartmentResponse("Department not found");
            existingDepartment.Name = department.Name;
            try
            {
                _departmentRepository.Update(existingDepartment);
                await _unitOfWork.CompleteAsync();

                return new DepartmentResponse(existingDepartment);
            }
            catch (Exception ex)
            {
                return new DepartmentResponse($"An error occurred while updating the department: {ex.Message}");
            }
        }

        public async Task<DepartmentResponse> DeleteAsync(int id)
        {
            var existingDepartment = await _departmentRepository.FindById(id);
            if (existingDepartment == null)
                return new DepartmentResponse("Department not found");
            try
            {
                _departmentRepository.Remove(existingDepartment);
                await _unitOfWork.CompleteAsync();

                return new DepartmentResponse(existingDepartment);
            }
            catch (Exception ex)
            {
                return new DepartmentResponse($"An error occurred while deleting the department: {ex.Message}");
            }
        }
    }
}