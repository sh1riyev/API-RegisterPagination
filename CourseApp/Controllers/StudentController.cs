using System;
using AutoMapper;
using Serilog;
using Service.Services.Interface;

namespace CourseApp.Controllers
{
	public class StudentController :BaseController
	{
		private readonly IStudentService _studentService;
		private readonly IMapper _mapper;
		public StudentController(IStudentService studentService,
			IMapper mapper)
		{
			_studentService = studentService;
			_mapper = mapper;
		}
	}
}

