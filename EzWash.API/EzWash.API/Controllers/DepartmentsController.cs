﻿using System.Collections.Generic;
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
    public class DepartmentsController: ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentsController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all Departments",
            Description = "List of Departments",
            OperationId = "ListAllDepartments")]
        [SwaggerResponse(200, "List of Departments", typeof(IEnumerable<DepartmentResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DepartmentResource>), 200)]
        public async Task<IEnumerable<DepartmentResource>> GetAllAsync()
        {
            var departments = await _departmentService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<Department>, IEnumerable<DepartmentResource>>(departments);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DepartmentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _departmentService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var departmentResource = _mapper
                .Map<Department, DepartmentResource>(result.Resource);
            return Ok(departmentResource);
        }

        [HttpPost]
        [ProducesResponseType(typeof(DepartmentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveDepartmentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var department = _mapper.Map<SaveDepartmentResource, Department>(resource);
            var result = await _departmentService.SaveAsync(department);

            if (!result.Success)
                return BadRequest(result.Message);
            var departmentResource = _mapper.Map<Department, DepartmentResource>(result.Resource);
            return Ok(departmentResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DepartmentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveDepartmentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var department = _mapper.Map<SaveDepartmentResource, Department>(resource);
            var result = await _departmentService.UpdateAsync(id, department);

            if (!result.Success)
                return BadRequest(result.Message);
            var departmentResource = _mapper.Map<Department, DepartmentResource>(result.Resource);
            return Ok(departmentResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DepartmentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _departmentService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var departmentResource = _mapper.Map<Department, DepartmentResource>(result.Resource);
            return Ok(departmentResource);
        }
    }
}