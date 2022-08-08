using AutoMapper;
using StudnetAdminPortal.API.DataModels;
using StudnetAdminPortal.API.DomainModels;
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
			CreateMap<Student, StudentDto>().ReverseMap();
			CreateMap<Gender, GenderDto>().ReverseMap();
			CreateMap<Address, AddressDto>().ReverseMap();
		}
	}
}
