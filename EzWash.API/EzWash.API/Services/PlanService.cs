using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EzWash.API.Domain.Models;
using EzWash.API.Domain.Persistence.Repositories;
using EzWash.API.Domain.Services;
using EzWash.API.Domain.Services.Communications;

namespace EzWash.API.Services
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PlanService(IPlanRepository planRepository, IUnitOfWork unitOfWork = null)
        {
            _planRepository = planRepository;
            _unitOfWork = unitOfWork;
        }

        

        public async Task<PlanResponse> DeleteAsync(int id)
        {
            var existingPlan = await _planRepository.FindById(id);
            if (existingPlan == null)
                return new PlanResponse("Plan not found");
            try
            {
                _planRepository.Remove(existingPlan);
                await _unitOfWork.CompleteAsync();

                return new PlanResponse(existingPlan);
            }
            catch (Exception ex)
            {
                return new PlanResponse($"An error occurred while deleting the Plan: {ex.Message}");
            }
        }

        public async Task<PlanResponse> GetByIdAsync(int id)
        {
            var existingPlan = await _planRepository.FindById(id);
            if (existingPlan == null)
                return new PlanResponse("Plan not found");
            return new PlanResponse(existingPlan);
        }

        public async Task<IEnumerable<Plan>> ListAsync()
        {
            return await _planRepository.ListAsync();
        }

        public async Task<PlanResponse> SaveAsync(Plan plan)
        {
            try
            {
                await _planRepository.AddAsync(plan);
                await _unitOfWork.CompleteAsync();

                return new PlanResponse(plan);
            }
            catch (Exception ex)
            {
                return new PlanResponse($"An error occurred while saving the Plan: {ex.Message}");
            }
        }

        public async Task<PlanResponse> UpdateAsync(int id, Plan plan)
        {
            var existingPlan = await _planRepository.FindById(id);
            if (existingPlan == null)
                return new PlanResponse("Plan not found");
            existingPlan.Name = plan.Name;
            try
            {
                _planRepository.Update(existingPlan);
                await _unitOfWork.CompleteAsync();

                return new PlanResponse(existingPlan);
            }
            catch (Exception ex)
            {
                return new PlanResponse($"An error occurred while updating the Plan: {ex.Message}");
            }
        }
    }
}
