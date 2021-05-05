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
    public class PlansController : ControllerBase
    {
        private readonly IPlanService _planService;
        private readonly IMapper _mapper;

        public PlansController(IPlanService planService, IMapper mapper)
        {
            _planService = planService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all Plans",
            Description = "List of Plans",
            OperationId = "ListAllPlans")]
        [SwaggerResponse(200, "List of Plans", typeof(IEnumerable<PlanResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PlanResource>), 200)]
        public async Task<IEnumerable<PlanResource>> GetAllAsync()
        {
            var plans = await _planService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<Plan>, IEnumerable<PlanResource>>(plans);
            return resources;
        }

        [SwaggerOperation(
            Summary = "Get Plan by id",
            Description = "Get a Plan",
            OperationId = "GetPlanById")]
        [SwaggerResponse(200, "Get a Plan", typeof(IEnumerable<PlanResource>))]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PlanResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _planService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var planResource = _mapper
                .Map<Plan, PlanResource>(result.Resource);
            return Ok(planResource);
        }

        [SwaggerOperation(
            Summary = "Save Plan",
            Description = "Save a Plan",
            OperationId = "SavePlan")]
        [SwaggerResponse(200, "Save a Plan", typeof(IEnumerable<PlanResource>))]
        [HttpPost]
        [ProducesResponseType(typeof(PlanResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SavePlanResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var plan = _mapper.Map<SavePlanResource, Plan>(resource);
            var result = await _planService.SaveAsync(plan);

            if (!result.Success)
                return BadRequest(result.Message);
            var planResource = _mapper.Map<Plan, PlanResource>(result.Resource);
            return Ok(planResource);
        }

        [SwaggerOperation(
            Summary = "Update Plan By Id",
            Description = "Update a Plan By Id",
            OperationId = "UpdatePlanById")]
        [SwaggerResponse(200, "Update a Plan By Id", typeof(IEnumerable<PlanResource>))]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PlanResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SavePlanResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var plan = _mapper.Map<SavePlanResource, Plan>(resource);
            var result = await _planService.UpdateAsync(id, plan);

            if (!result.Success)
                return BadRequest(result.Message);
            var planResource = _mapper.Map<Plan, PlanResource>(result.Resource);
            return Ok(planResource);
        }

        [SwaggerOperation(
           Summary = "Delete Plan By Id",
           Description = "Delete a Plan By Id",
           OperationId = "DeletePlanById")]
        [SwaggerResponse(200, "Delete a Plan By Id", typeof(IEnumerable<PlanResource>))]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(PlanResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _planService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var planResource = _mapper.Map<Plan, PlanResource>(result.Resource);
            return Ok(planResource);
        }
    }
    
}
