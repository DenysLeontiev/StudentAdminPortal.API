using AutoMapper;
using StudnetAdminPortal.API.DataModels;
using StudnetAdminPortal.API.DomainModels;
using StudnetAdminPortal.API.Profiles.AfterMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudnetAdminPortal.API.Profiles
{
	public class AutoMapperProfiles : Profile // creates configration fo AutoMapper
	{
		public AutoMapperProfiles()
		{
			CreateMap<Student, StudentDto>().ReverseMap();  // <source, destination>
			CreateMap<Gender, GenderDto>().ReverseMap();
			CreateMap<Address, AddressDto>().ReverseMap();

			CreateMap<UpdateStundetRequest, Student>().AfterMap<UpdateStudentRequestAfterMap>();
			 
			CreateMap<AddStudentRequest, Student>().AfterMap<AddStudentRequestAfterMap>();
		}
	}
}
