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
    public class BenefitsController : ControllerBase
    {
        private readonly IBenefitService _benefitService;
        private readonly IMapper _mapper;

        public BenefitsController(IBenefitService benefitService, IMapper mapper)
        {
            _benefitService =benefitService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all Benefits",
            Description = "List of Benefits",
            OperationId = "ListAllBenefits")]
        [SwaggerResponse(200, "List of Benefits", typeof(IEnumerable<BenefitResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BenefitResource>), 200)]
        public async Task<IEnumerable<BenefitResource>> GetAllAsync()
        {
            var benefits = await _benefitService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<Benefit>, IEnumerable<BenefitResource>>(benefits);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BenefitResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _benefitService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var benefitResource = _mapper
                .Map<Benefit, BenefitResource>(result.Resource);
            return Ok(benefitResource);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BenefitResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveBenefitResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var benefit = _mapper.Map<SaveBenefitResource, Benefit>(resource);
            var result = await _benefitService.SaveAsync(benefit);

            if (!result.Success)
                return BadRequest(result.Message);
            var benefitResource = _mapper.Map<Benefit, BenefitResource>(result.Resource);
            return Ok(benefitResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BenefitResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveBenefitResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var department = _mapper.Map<SaveBenefitResource, Benefit>(resource);
            var result = await _benefitService.UpdateAsync(id, department);

            if (!result.Success)
                return BadRequest(result.Message);
            var benefitResource = _mapper.Map<Benefit, BenefitResource>(result.Resource);
            return Ok(benefitResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BenefitResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _benefitService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var benefitResource = _mapper.Map<Benefit, BenefitResource>(result.Resource);
            return Ok(benefitResource);
        }
    }
}