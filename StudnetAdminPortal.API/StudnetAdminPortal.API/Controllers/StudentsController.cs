using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudnetAdminPortal.API.DomainModels;
using StudnetAdminPortal.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudnetAdminPortal.API.Controllers
{
	[ApiController]
	public class StudentsController : Controller
	{
		private readonly IStudentRepository studentRepository;
		private readonly IMapper mapper;

		public StudentsController(IStudentRepository studentRepository, IMapper mapper)
		{
			this.studentRepository = studentRepository;
			this.mapper = mapper;
		}

		[HttpGet]
		[Route("[controller]")]
		public async Task<IActionResult> GetAllStudents()
		{
			var students = await studentRepository.GetStudentsAsync();

			return Ok(mapper.Map<List<StudentDto>>(students));
		}

		[HttpGet]
		[Route("[controller]/{studentId:guid}")]
		public async Task<IActionResult> GetStudents([FromRoute]  Guid studentId)
		{
			var student = await studentRepository.GetStudentAsync(studentId);

			if(student == null)
			{
				return NotFound();
			}

			return Ok(mapper.Map<StudentDto>(student));
		}
	}
}
