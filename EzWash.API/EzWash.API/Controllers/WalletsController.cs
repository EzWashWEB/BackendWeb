using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EzWash.API.Domain.Models;
using EzWash.API.Domain.Services;
using EzWash.API.Extensions;
using EzWash.API.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EzWash.API.Controllers
{
    [Route("/api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class WalletsController: ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly IMapper _mapper;

        public WalletsController(IWalletService walletService, IMapper mapper)
        {
            _walletService = walletService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all Wallets",
            Description = "List of Wallets",
            OperationId = "ListAllWallets")]
        [SwaggerResponse(200, "List of Wallets", typeof(IEnumerable<WalletResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WalletResource>), 200)]
        public async Task<IEnumerable<WalletResource>> GetAllAsync()
        {
            var wallets = await _walletService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<Wallet>, IEnumerable<WalletResource>>(wallets);
            return resources;
        }

        [SwaggerOperation(
            Summary = "Get Wallet by id",
            Description = "Get a Wallet",
            OperationId = "GetWalletById")]
        [SwaggerResponse(200, "Get a Wallet", typeof(IEnumerable<WalletResource>))]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WalletResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _walletService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var walletResource = _mapper
                .Map<Wallet, WalletResource>(result.Resource);
            return Ok(walletResource);
        }

        [SwaggerOperation(
            Summary = "Save Wallet",
            Description = "Save a Wallet",
            OperationId = "SaveWallet")]
        [SwaggerResponse(200, "Save a Wallet", typeof(IEnumerable<WalletResource>))]
        [HttpPost]
        [ProducesResponseType(typeof(WalletResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveWalletResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var wallet = _mapper.Map<SaveWalletResource, Wallet>(resource);
            var result = await _walletService.SaveAsync(wallet);

            if (!result.Success)
                return BadRequest(result.Message);
            var walletResource = _mapper.Map<Wallet, WalletResource>(result.Resource);
            return Ok(walletResource);
        }

        [SwaggerOperation(
            Summary = "Update Wallet By Id",
            Description = "Update a Wallet By Id",
            OperationId = "UpdateWalletById")]
        [SwaggerResponse(200, "Update a Wallet By Id", typeof(IEnumerable<WalletResource>))]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(WalletResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveWalletResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var wallet = _mapper.Map<SaveWalletResource, Wallet>(resource);
            var result = await _walletService.UpdateAsync(id, wallet);

            if (!result.Success)
                return BadRequest(result.Message);
            var walletResource = _mapper.Map<Wallet, WalletResource>(result.Resource);
            return Ok(walletResource);
        }


        [SwaggerOperation(
            Summary = "Delete Wallet By Id",
            Description = "Delete a Wallet By Id",
            OperationId = "DeleteWalletById")]
        [SwaggerResponse(200, "Delete a Wallet By Id", typeof(IEnumerable<WalletResource>))]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(WalletResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _walletService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var walletResource = _mapper.Map<Wallet, WalletResource>(result.Resource);
            return Ok(walletResource);
        }
    }
}
