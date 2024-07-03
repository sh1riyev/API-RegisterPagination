using System;
using AutoMapper;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.Education;
using Service.Services.Interface;

namespace CourseApp.Controllers
{
	public class EducationController : BaseController
	{
		private readonly IEducationService _eudcationService;
		private readonly IMapper _mapper;
		public EducationController(IEducationService educationService,
			IMapper mapper)
		{
			_eudcationService = educationService;
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromForm] EducationCreateDto request)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var mappedEducation = _mapper.Map<Education>(request);
			await _eudcationService.Create(mappedEducation);
			return CreatedAtAction(nameof(Create), request);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id is null) return BadRequest();
			var education = await _eudcationService.GetBy(m=>m.Id==id);
			if (education is null) return NotFound();
			await _eudcationService.Delete(education);
			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromBody] EducationUpdateDto request, int? id)
		{
            if (id is null) return BadRequest();
            var education = await _eudcationService.GetBy(m => m.Id == id);
            if (education is null) return NotFound();
			var mappedEducation = _mapper.Map(request, education);
			await _eudcationService.Update(mappedEducation);
			return Ok(mappedEducation);
        }

		[HttpGet]
		public async Task<IActionResult> Search([FromQuery] string educationName)
		{
            if (string.IsNullOrWhiteSpace(educationName))
            {
                return BadRequest("Search term cannot be empty.");
            }

			var results = await _eudcationService.GetAll(m => m.Name.Contains(educationName));
			if (results.Count==0) return NotFound("No education records found with the specified name.");
			var mappedEducations = _mapper.Map<List<EducationDto>>(results);
			return Ok(mappedEducations);
        }

		[HttpGet]
		public async Task<IActionResult> Sort([FromQuery] string order = "asc")
		{
            var query = await _eudcationService.GetAll();
			var mappedQuery = _mapper.Map<List<EducationDto>>(query);

            if (order.ToLower() == "desc")
            {
				mappedQuery = mappedQuery.OrderByDescending(m => m.Name).ToList();
            }
            else
            {
                mappedQuery = mappedQuery.OrderBy(m => m.Name).ToList();

            }

            return Ok(mappedQuery);
        }
	}
}

