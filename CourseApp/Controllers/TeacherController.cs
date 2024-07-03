using System;
using AutoMapper;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Room;
using Service.DTOs.Teacher;
using Service.Services;
using Service.Services.Interface;

namespace CourseApp.Controllers
{
	public class TeacherController : BaseController
	{
		private readonly ITeacherService _teacherService;
		private readonly IGroupService _groupService;
		private readonly IMapper _mapper;
		public TeacherController(ITeacherService teacherService,
			IGroupService groupService,
			IMapper mapper)
		{
			_teacherService = teacherService;
			_groupService = groupService;
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromQuery]TeacherCreateDto request)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			foreach (var group in request.GroupId)
			{
				if (!await _groupService.IsExist(m => m.Id == group)) return NotFound("This Group doesnt exist choose another");
			}
			var mappedTeacher = _mapper.Map<Teacher>(request);
			await _teacherService.Create(mappedTeacher);
			return CreatedAtAction(nameof(Create), mappedTeacher);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id is null) return BadRequest();
			var teacher = await _teacherService.GetBy(m => m.Id==id);
			if (teacher is null) return NotFound("Tacher is not exist");
			await _teacherService.Delete(teacher);
			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int? id, [FromBody] TeacherUpdateDto request)
		{
            if (id is null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var teacher = await _teacherService.GetBy(m => m.Id == id,"GroupTeachers");
            if (teacher is null) return NotFound("Tacher is not exist");
            foreach (var group in request.GroupId)
            {
                if (!await _groupService.IsExist(m => m.Id == group)) return NotFound("This Group doesnt exist choose another");
            }
			var mappedTeacher = _mapper.Map(request, teacher);
			await _teacherService.Update(mappedTeacher);
			return Ok(mappedTeacher);
        }

		[HttpGet]
		public async Task<IActionResult> Search([FromQuery] string request)
		{
            if (string.IsNullOrWhiteSpace(request)) return BadRequest("Search term cannot be empty.");
            var teachers = await _teacherService.GetAll(m => m.Name.Contains(request)||m.Surname.Contains(request), "GroupTeachers.Group.Education");
            if (teachers.Count == 0) return NotFound("No teacher records found with the specified name.");
            var mappedTeachers = _mapper.Map<List<TeacherDto>>(teachers);
            return Ok(mappedTeachers);
        }

	}
}

