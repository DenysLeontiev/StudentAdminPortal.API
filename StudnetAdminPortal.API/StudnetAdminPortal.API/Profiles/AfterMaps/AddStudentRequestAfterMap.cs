using AutoMapper;
using StudnetAdminPortal.API.DataModels;
using StudnetAdminPortal.API.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudnetAdminPortal.API.Profiles.AfterMaps
{
	public class AddStudentRequestAfterMap : IMappingAction<AddStudentRequest, Student>
	{
		public void Process(AddStudentRequest source, Student destination, ResolutionContext context)
		{
			destination.Id = Guid.NewGuid();

			destination.Address = new Address()
			{
				Id = Guid.NewGuid(),
				PhysicalAddress = source.PhysicalAddress,
				PostalAddress = source.PostalAddress,
			};

		}
	}

	//public class AddStudentRequestAfterMap : IMappingAction<AddStudentRequest, Student>
	//{
	//	public void Process(AddStudentRequest source, Student destination, ResolutionContext context)
	//	{
	//		destination.Id = Guid.NewGuid();
	//		destination.Address = new Address()
	//		{
	//			Id = Guid.NewGuid(),
	//			PhysicalAddress = source.PhysicalAddress,
	//			PostalAddress = source.PostalAddress
	//		};
	//	}
	//}
}
