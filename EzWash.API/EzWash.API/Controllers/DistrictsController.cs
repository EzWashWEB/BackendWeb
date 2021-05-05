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
    [Route("/api/departments/{departmentId}/provinces/{provinceId}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DistrictsController: ControllerBase
    {
        private readonly IDistrictService _districtService;
        private readonly IMapper _mapper;

        public DistrictsController(IDistrictService districtService, IMapper mapper)
        {
            _districtService = districtService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all Districts by ProvinceId",
            Description = "List of Districts by ProvinceId",
            OperationId = "ListAllDistrictsByProvinceId")]
        [SwaggerResponse(200, "List of Districts by ProvinceId", typeof(IEnumerable<DistrictResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DistrictResource>), 200)]
        public async Task<IEnumerable<DistrictResource>> GetAllByProvinceIdAsync(int provinceId)
        {
            var districts = await _districtService.ListByProvinceIdAsync(provinceId);
            var resources = _mapper
                .Map<IEnumerable<District>, IEnumerable<DistrictResource>>(districts);
            return resources;
        }

        [SwaggerOperation(
            Summary = "Get District by DepartmentId, ProvinceId And Id",
            Description = "Get a District by DepartmentId, ProvinceId And Id",
            OperationId = "GetDistrictByDepartmentIdProvinceIdAndId")]
        [SwaggerResponse(200, "Get a District by DepartmentId, ProvinceId And Id", typeof(IEnumerable<DistrictResource>))]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DistrictResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByDepartmentIdAndProvinceIdAndIdAsync(int departmentId, int provinceId, int id)
        {
            var result = await _districtService.GetByDepartmentIdAndProvinceIdAndId(departmentId, provinceId, id);
            if (!result.Success)
                return BadRequest(result.Message);
            var districtResource = _mapper
                .Map<District, DistrictResource>(result.Resource);
            return Ok(districtResource);
        }

        [SwaggerOperation(
            Summary = "Save District by DepartmentId and ProvinceId",
            Description = "Save a District by DepartmentId and ProvinceId",
            OperationId = "SaveDistrictByDepartmentIdAndProvinceId")]
        [SwaggerResponse(200, "Save a District by DepartmentId and ProvinceId", typeof(IEnumerable<DistrictResource>))]
        [HttpPost]
        [ProducesResponseType(typeof(DistrictResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int departmentId, int provinceId, [FromBody] SaveDistrictResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var district = _mapper.Map<SaveDistrictResource, District>(resource);
            var result = await _districtService.SaveAsync(departmentId, provinceId, district);
            if (!result.Success)
                return BadRequest(result.Message);
            var districtResource = _mapper.Map<District, DistrictResource>(result.Resource);
            return Ok(districtResource);
        }

        [SwaggerOperation(
            Summary = "Update District by DepartmentId, ProvinceId And Id",
            Description = "Update a District by DepartmentId, ProvinceId And Id",
            OperationId = "UpdateDistrictByDepartmentIdProvinceIdAndId")]
        [SwaggerResponse(200, "Update a District by DepartmentId, ProvinceId And Id", typeof(IEnumerable<DistrictResource>))]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DistrictResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int departmentId, int provinceId, int id, [FromBody] SaveDistrictResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var district = _mapper.Map<SaveDistrictResource, District>(resource);
            var result = await _districtService.UpdateAsync(departmentId, provinceId, id, district);
            if (!result.Success)
                return BadRequest(result.Message);
            var districtResource = _mapper.Map<District, DistrictResource>(result.Resource);
            return Ok(districtResource);
        }

        [SwaggerOperation(
            Summary = "Delete District by DepartmentId, ProvinceId And Id",
            Description = "Delete a District by DepartmentId, ProvinceId And Id",
            OperationId = "DeleteDistrictByDepartmentIdProvinceIdAndId")]
        [SwaggerResponse(200, "Delete a District by DepartmentId, ProvinceId And Id", typeof(IEnumerable<DistrictResource>))]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DistrictResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int departmentId, int provinceId, int id)
        {
            var result = await _districtService.DeleteAsync(departmentId, provinceId, id);
            if (!result.Success)
                return BadRequest(result.Message);
            var districtResource = _mapper.Map<District, DistrictResource>(result.Resource);
            return Ok(districtResource);
        }
    }
}