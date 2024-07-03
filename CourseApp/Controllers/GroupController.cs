using System;
using Serilog;
using AutoMapper;
using Service.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Group;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Service.DTOs.Education;
using Service.Services;
using Service.DTOs.Room;

namespace CourseApp.Controllers
{
	public class GroupController : BaseController
	{
		private readonly IGroupService _groupService;
		private readonly IRoomService _roomService;
		private readonly IEducationService _educationService;
		private readonly IMapper _mapper;
		public GroupController(IGroupService groupService,
			IRoomService roomService,
			IEducationService educationService,
			IMapper mapper)
		{
			_groupService = groupService;
			_roomService = roomService;
			_educationService = educationService;
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromForm] GroupCreateDto request)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			if (!await _educationService.IsExist(m => m.Id == request.EducationId)) return NotFound("This education is not exist,choose another one");
			if (!await _roomService.IsExist(m => m.Id == request.RoomId)) return NotFound("This room is not exist, choose another");
			var mappedGroup = _mapper.Map<Group>(request);
			await _groupService.Create(mappedGroup);
			return CreatedAtAction(nameof(Create), request);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id is null) return BadRequest();
			var group = await _groupService.GetBy(m => m.Id==id);
			if (group is null) return NotFound();
			await _groupService.Delete(group);
			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int? id, [FromQuery]GroupUpdateDto request)
		{
            if (id is null) return BadRequest();
            var group = await _groupService.GetBy(m => m.Id == id);
            if (group is null) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!await _educationService.IsExist(m => m.Id == request.EducationId)) return NotFound("This education is not exist,choose another one");
            if (!await _roomService.IsExist(m => m.Id == request.RoomId)) return NotFound("This room is not exist, choose another");
			var mappedGroup = _mapper.Map(request, group);
			await _groupService.Update(mappedGroup);
			return Ok(mappedGroup);
        }

		[HttpGet]
		public async Task<IActionResult> Search([FromQuery] string groupName)
		{
			if(string.IsNullOrWhiteSpace(groupName))  return BadRequest("Search term cannot be empty.");
			var groups = await _groupService.GetAll(m => m.Name.Contains(groupName));
            if (groups.Count == 0) return NotFound("No education records found with the specified name.");
			var mappedGroups = _mapper.Map<List<GroupDto>>(groups);
			return Ok(mappedGroups);
        }

		[HttpGet]
		public async Task<IActionResult> Sort([FromQuery]string property, [FromQuery] string order="asc")
		{
            var query = await _groupService.GetAll();
            var mappedQuery = _mapper.Map<List<GroupDto>>(query);

            switch (property.ToLower())
            {
                case "name":
                    mappedQuery = order.ToLower() == "desc" ? mappedQuery.OrderByDescending(m => m.Name).ToList()
                        : mappedQuery.OrderBy(m => m.Name).ToList();
                    break;
                case "capacity":
                    mappedQuery = order.ToLower() == "desc" ? mappedQuery.OrderByDescending(m => m.Capacity).ToList()
                    : mappedQuery.OrderBy(m => m.Capacity).ToList();
                    break;
                default:
                    return BadRequest("Invalid sortBy parameter. Use 'name' or 'capacity'.");
            }

            return Ok(mappedQuery);
        }
    }
}

