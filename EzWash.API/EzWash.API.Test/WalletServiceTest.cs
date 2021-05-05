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
    class WalletServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetByIdAsyncWhenNoWalletFoundReturnsWalletNotFoundResponse()
        {
            //Arrange
            var mockWalletRepository = GetDefaultIWalletRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var walletId = 1;
            mockWalletRepository.Setup(r => r.FindById(walletId))
                .Returns(Task.FromResult<Wallet>(null));
            var service = new WalletService(mockWalletRepository.Object, mockUnitOfWork.Object);

            //Act
            WalletResponse result = await service.GetByIdAsync(walletId);
            var message = result.Message;

            //Assert
            message.Should().Be("Wallet not found");
        }

        [Test]
        public async Task GetByIdAsyncWhenWalletFoundReturnsWalletFoundResponse()
        {
            //Arrange
            var mockWalletRepository = GetDefaultIWalletRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockWallet = new Mock<Wallet>();
            mockWallet.Object.Id = 1;
            mockWallet.Object.Amount = 0;
            mockWallet.Object.Currency = "Soles";
            mockWalletRepository.Setup(r => r.AddAsync(mockWallet.Object))
                .Returns(Task.FromResult(mockWallet.Object));
            var service = new WalletService(mockWalletRepository.Object, mockUnitOfWork.Object);


            //Act

            WalletResponse result = await service.SaveAsync(mockWallet.Object);
            var id = result.Resource.Id;
            var amount = result.Resource.Amount;
            var currency = result.Resource.Currency;


            //Assert
            id.Should().Be(1);
            amount.Should().Be(0);
            currency.Should().Be("Soles");

        }


        private Mock<IWalletRepository> GetDefaultIWalletRepositoryInstance() => new();
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance() => new();
    }

    
}
