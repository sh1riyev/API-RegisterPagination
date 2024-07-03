using System;
namespace Service.DTOs.Group
{
	public class GroupCreateDto
	{
		public string Name { get; set; }
		public int Capacity { get; set; }
		public int EducationId { get; set; }
		public int RoomId { get; set; }
	}
}

