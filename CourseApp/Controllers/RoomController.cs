using System;
using AutoMapper;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Education;
using Service.DTOs.Room;
using Service.Services;
using Service.Services.Interface;

namespace CourseApp.Controllers
{
	public class RoomController : BaseController
	{
		private readonly IRoomService _roomService;
		private readonly IMapper _mapper;
		public RoomController(IRoomService roomService,
			IMapper mapper)
		{
			_roomService = roomService;
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] RoomCreateDto request)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var mappedRoom = _mapper.Map<Room>(request);
			await _roomService.Create(mappedRoom);
			return CreatedAtAction(nameof(Create), mappedRoom);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id is null) return BadRequest();
			var room = await _roomService.GetBy(m => m.Id == id);
			if (room is null) return NotFound();
			await _roomService.Delete(room);
			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromQuery] RoomUpdateDto request, int? id)
		{
            if (id is null) return BadRequest();
            var room = await _roomService.GetBy(m => m.Id == id);
            if (room is null) return NotFound();
			var mappedRoom = _mapper.Map(request, room);
			await _roomService.Update(mappedRoom);
			return Ok(mappedRoom);
        }

		[HttpGet]
		public async Task<IActionResult> Search([FromQuery] string roomName)
		{
			if(string.IsNullOrWhiteSpace(roomName)) return BadRequest("Search term cannot be empty.");
			var rooms = await _roomService.GetAll(m=>m.Name.Contains(roomName));
			if (rooms.Count == 0) return NotFound("No education records found with the specified name.");
			var mappedRooms = _mapper.Map < List<RoomDto>>(rooms);
			return Ok(mappedRooms);
        }

		[HttpGet]
		public async Task<IActionResult> Sort([FromQuery] string property, [FromQuery] string order="asc")
		{
            var query = await _roomService.GetAll();
            var mappedQuery = _mapper.Map<List<RoomDto>>(query);

			switch (property.ToLower()) 
			{
				case "name" :
					mappedQuery = order.ToLower() == "desc" ? mappedQuery.OrderByDescending(m => m.Name).ToList()
						: mappedQuery.OrderBy(m => m.Name).ToList();
                    break;
				case "seatcount":
                    mappedQuery = order.ToLower() == "desc" ? mappedQuery.OrderByDescending(m => m.SeatCount).ToList()
                    : mappedQuery.OrderBy(m => m.SeatCount).ToList();
                    break;
                default:
                    return BadRequest("Invalid sortBy parameter. Use 'name' or 'seatcount'.");
			}

			return Ok(mappedQuery);
		}
	}
}

