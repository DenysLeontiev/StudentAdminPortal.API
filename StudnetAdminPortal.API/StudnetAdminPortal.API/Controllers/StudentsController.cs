using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudnetAdminPortal.API.DataModels;
using StudnetAdminPortal.API.DomainModels;
using StudnetAdminPortal.API.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StudnetAdminPortal.API.Controllers
{
	[ApiController]
	public class StudentsController : Controller
	{
		private readonly IStudentRepository studentRepository;
		private readonly IMapper mapper;
		private readonly IImageRepository imageRepository;

		public StudentsController(IStudentRepository studentRepository, IMapper mapper, IImageRepository imageRepository)
		{
			this.studentRepository = studentRepository;
			this.mapper = mapper;
			this.imageRepository = imageRepository;
		}

		[HttpGet]
		[Route("[controller]")]
		public async Task<IActionResult> GetAllStudents()
		{
			var students = await studentRepository.GetStudentsAsync();

			return Ok(mapper.Map<List<StudentDto>>(students));
		}

		[HttpGet]
		[Route("[controller]/{studentId:guid}"), ActionName("GetStudents")]
		public async Task<IActionResult> GetStudents([FromRoute] Guid studentId)
		{
			var student = await studentRepository.GetStudentAsync(studentId);

			if (student == null)
			{
				return NotFound();
			}

			return Ok(mapper.Map<StudentDto>(student));
		}

		[HttpPut]
		[Route("[controller]/{studentId:guid}")]
		public async Task<IActionResult> UpdateStudentsAsync([FromRoute] Guid studentId, [FromBody] UpdateStundetRequest request)
		{
			if (await studentRepository.Exist(studentId))
			{
				var updatedStudent = await studentRepository.UpdateStudent(studentId, mapper.Map<Student>(request));
				if (updatedStudent != null)
				{
					return Ok(mapper.Map<StudentDto>(updatedStudent));
				}
				//stopper here
			}

			return NotFound();
		}

		[HttpDelete]
		[Route("[controller]/{studentId:guid}")]
		public async Task<IActionResult> DeleteStudentAsync(Guid studentId)
		{
			if(await studentRepository.Exist(studentId))
			{
				var student = await studentRepository.DeleteStudent(studentId);
				return Ok(mapper.Map<StudentDto>(student));
			}

			return NotFound();
		}

		//[HttpPost]
		//[Route("[controller]/Add")]
		//public async Task<IActionResult> AddStudent([FromBody] AddStudentRequest request)
		//{
		//	var student = await studentRepository.AddStudent(mapper.Map<Student>(request));
		//	return CreatedAtAction(nameof(GetStudents), new { studentId = student.Id }, mapper.Map<StudentDto>(student));
		//}

		[HttpPost]
		[Route("[controller]/Add")]
		public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)
		{
			var student = await studentRepository.AddStudent(mapper.Map<Student>(request));
			return CreatedAtAction(nameof(GetStudents), new { studentId = student.Id },
				mapper.Map<Student>(student));
		}

		[HttpPost]
		[Route("[controller]/{studentId:guid}/upload-image")]
		public async Task<IActionResult> UploadImage([FromRoute] Guid studentId, IFormFile file)
		{
			if(await studentRepository.Exist(studentId))
			{
				var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
				var fileImagePath = await imageRepository.Upload(file, fileName);

				if(await studentRepository.UpdateProfileImage(studentId, fileImagePath))
				{
					return Ok(fileImagePath);
				}

				return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while uploading the image");
			}

			return NotFound();
		}
	}
}
