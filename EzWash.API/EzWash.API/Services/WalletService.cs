using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EzWash.API.Domain.Models;
using EzWash.API.Domain.Persistence.Repositories;
using EzWash.API.Domain.Services;
using EzWash.API.Domain.Services.Communications;

namespace EzWash.API.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IUnitOfWork _unitOfWork;

        public WalletService(IWalletRepository planRepository, IUnitOfWork unitOfWork = null)
        {
            _walletRepository = planRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<WalletResponse> DeleteAsync(int id)
        {
            //TODO change this-->

            //this can take some time, cause yo have to think about your wallet always exists, so 
            //this maybe can be changed by amount, not by this the whole wallet, 
            //WDYT?
            var existingWallet = await _walletRepository.FindById(id);
            if (existingWallet == null)
                return new WalletResponse("Wallet not found");
            try
            {
                _walletRepository.Remove(existingWallet);
                await _unitOfWork.CompleteAsync();

                return new WalletResponse(existingWallet);
            }
            catch (Exception ex)
            {
                return new WalletResponse($"An error occurred while deleting the wallet: {ex.Message}");
            }
        }

        public async Task<WalletResponse> GetByIdAsync(int id)
        {
            var existingWallet = await _walletRepository.FindById(id);
            if (existingWallet == null)
                return new WalletResponse("Wallet not found");
            return new WalletResponse(existingWallet);
        }

        public async Task<IEnumerable<Wallet>> ListAsync()
        {
            return await _walletRepository.ListAsync();
        }

        public async Task<WalletResponse> SaveAsync(Wallet wallet)
        {
            try
            {
                await _walletRepository.AddAsync(wallet);
                await _unitOfWork.CompleteAsync();

                return new WalletResponse(wallet);
            }
            catch (Exception ex)
            {
                return new WalletResponse($"An error occurred while saving the wallet: {ex.Message}");
            }
        }

        public async Task<WalletResponse> UpdateAsync(int id, Wallet wallet)
        {
            var existingWallet = await _walletRepository.FindById(id);
            if (existingWallet == null)
                return new WalletResponse("Wallet not found");
            existingWallet.Amount = wallet.Amount;
            try
            {
                _walletRepository.Update(existingWallet);
                await _unitOfWork.CompleteAsync();

                return new WalletResponse(existingWallet);
            }
            catch (Exception ex)
            {
                return new WalletResponse($"An error occurred while updating the wallet: {ex.Message}");
            }
        }
    }
}
