using EzWash.API.Domain.Models;
using EzWash.API.Domain.Persistence.Repositories;
using EzWash.API.Domain.Services;
using EzWash.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzWash.API.Services
{
    public class BenefitService : IBenefitService
    {
        private readonly IBenefitRepository _benefitRepository;
        private readonly IUnitOfWork _unitOfWork;
        public BenefitService(IBenefitRepository benefitRepository, IUnitOfWork unitOfWork = null)
        {
            _benefitRepository = benefitRepository;
            _unitOfWork = unitOfWork;
        }



        public async Task<BenefitResponse> DeleteAsync(int id)
        {
            var existingBenefit = await _benefitRepository.FindById(id);
            if (existingBenefit == null)
                return new BenefitResponse("Benefit not found");
            try
            {
                _benefitRepository.Remove(existingBenefit);
                await _unitOfWork.CompleteAsync();

                return new BenefitResponse(existingBenefit);
            }
            catch (Exception ex)
            {
                return new BenefitResponse($"An error occurred while deleting the Benefit: {ex.Message}");
            }
        }

        public async Task<BenefitResponse> GetByIdAsync(int id)
        {
            var existingBenefit = await _benefitRepository.FindById(id);
            if (existingBenefit == null)
                return new BenefitResponse("Benefit not found");
            return new BenefitResponse(existingBenefit);
        }

        public async Task<IEnumerable<Benefit>> ListAsync()
        {
            return await _benefitRepository.ListAsync();
        }

        public async Task<BenefitResponse> SaveAsync(Benefit benefit)
        {
            try
            {
                await _benefitRepository.AddAsync(benefit);
                await _unitOfWork.CompleteAsync();

                return new BenefitResponse(benefit);
            }
            catch (Exception ex)
            {
                return new BenefitResponse($"An error occurred while saving the Benefit: {ex.Message}");
            }
        }

        public async Task<BenefitResponse> UpdateAsync(int id, Benefit benefit)
        {
            var existingBenefit = await _benefitRepository.FindById(id);
            if (existingBenefit == null)
                return new BenefitResponse("Benefit not found");
            existingBenefit.Description = benefit.Description;
            try
            {
                _benefitRepository.Update(existingBenefit);
                await _unitOfWork.CompleteAsync();

                return new BenefitResponse(existingBenefit);
            }
            catch (Exception ex)
            {
                return new BenefitResponse($"An error occurred while updating the : {ex.Message}");
            }
        }
    }
}
