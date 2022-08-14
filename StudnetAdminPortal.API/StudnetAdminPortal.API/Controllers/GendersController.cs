using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudnetAdminPortal.API.DataModels;
using StudnetAdminPortal.API.DomainModels;
using StudnetAdminPortal.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudnetAdminPortal.API.Controllers
{
	[ApiController]
	public class GendersController : Controller
	{
		private readonly IStudentRepository repository;
		private readonly IMapper mapper;

		public GendersController(IStudentRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		[Route("[controller]")]
		[HttpGet]
		public async Task<IActionResult> GetAllGenders()
		{
			var genderList = await repository.GetGendersAsync();

			if (genderList == null || !genderList.Any())
			{
				return NotFound();
			}


			return Ok(mapper.Map<List<GenderDto>>(genderList));
		}
	}
}
