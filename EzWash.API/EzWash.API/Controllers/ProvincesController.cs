using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EzWash.API.Domain.Models;
using EzWash.API.Domain.Services;
using EzWash.API.Domain.Services.Communications;
using EzWash.API.Extensions;
using EzWash.API.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EzWash.API.Controllers
{
    [Route("/api/deparments/{departmentId}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProvincesController: ControllerBase
    {
        private readonly IProvinceService _provinceService;
        private readonly IMapper _mapper;

        public ProvincesController(IProvinceService provinceService, IMapper mapper)
        {
            _provinceService = provinceService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all Provinces by DepartmentId",
            Description = "List of Provinces by DepartmentId",
            OperationId = "ListAllProvincesByDepartmentId")]
        [SwaggerResponse(200, "List of Provinces by DepartmentId", typeof(IEnumerable<ProvinceResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProvinceResource>), 200)]
        public async Task<IEnumerable<ProvinceResource>> GetAllByDepartmentIdAsync(int departmentId)
        {
            var provinces = await _provinceService.ListByDepartmentIdAsync(departmentId);
            var resources = _mapper
                .Map<IEnumerable<Province>, IEnumerable<ProvinceResource>>(provinces);
            return resources;
        }

        [SwaggerOperation(
            Summary = "Get Province by DepartmentId And Id",
            Description = "Get a Province by DepartmentId And Id",
            OperationId = "GetProvinceByDepartmentIdAndId")]
        [SwaggerResponse(200, "Get a Province by DepartmentId And Id", typeof(IEnumerable<ProvinceResource>))]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProvinceResponse), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByDepartmentIdAndIdAsync(int departmentId, int id)
        {
            var result = await _provinceService.GetByDepartmentIdAndIdAsync(departmentId, id);
            if (!result.Success)
                return BadRequest(result.Message);
            var provinceResource = _mapper
                .Map<Province, ProvinceResource>(result.Resource);
            return Ok(provinceResource);
        }

        [SwaggerOperation(
            Summary = "Save Province by DepartmentId",
            Description = "Save a Province by DepartmentId",
            OperationId = "SaveProvinceByDepartmentId")]
        [SwaggerResponse(200, "Save a Province by DepartmentId", typeof(IEnumerable<ProvinceResource>))]
        [HttpPost]
        [ProducesResponseType(typeof(ProvinceResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int departmentId, [FromBody] SaveProvinceResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var province = _mapper.Map<SaveProvinceResource, Province>(resource);
            var result = await _provinceService.SaveAsync(departmentId, province);
            
            if (!result.Success)
                return BadRequest(result.Message);
            var provinceResource = _mapper.Map<Province, ProvinceResource>(result.Resource);
            return Ok(provinceResource);
        }

        [SwaggerOperation(
            Summary = "Update Province by DepartmentId And Id",
            Description = "Update a Province by DepartmentId And Id",
            OperationId = "UpdateProvinceByDepartmentIdAndId")]
        [SwaggerResponse(200, "Update a Province by DepartmentId And Id", typeof(IEnumerable<ProvinceResource>))]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProvinceResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int departmentId, int id, [FromBody] SaveProvinceResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var province = _mapper.Map<SaveProvinceResource, Province>(resource);
            var result = await _provinceService.UpdateAsync(departmentId, id, province);
            
            if (!result.Success)
                return BadRequest(result.Message);
            var provinceResource = _mapper.Map<Province, ProvinceResource>(result.Resource);
            return Ok(provinceResource);
        }

        [SwaggerOperation(
            Summary = "Delete Province by DepartmentId And Id",
            Description = "Delete a Province by DepartmentId And Id",
            OperationId = "DeleteProvinceByDepartmentIdAndId")]
        [SwaggerResponse(200, "Delete a Province by DepartmentId And Id", typeof(IEnumerable<ProvinceResource>))]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ProvinceResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int departmentId, int id)
        {
            var result = await _provinceService.DeleteAsync(departmentId, id);
            if (!result.Success)
                return BadRequest(result.Message);
            var provinceResource = _mapper.Map<Province, ProvinceResource>(result.Resource);
            return Ok(provinceResource);
        }
    }
}