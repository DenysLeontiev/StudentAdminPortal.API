using AutoMapper;
using StudnetAdminPortal.API.DataModels;
using StudnetAdminPortal.API.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudnetAdminPortal.API.Profiles.AfterMaps
{
	public class UpdateStudentRequestAfterMap : IMappingAction<UpdateStundetRequest, Student>  // Customa map configuration
	{
		public void Process(UpdateStundetRequest source, Student destination, ResolutionContext context)
		{
			destination.Address = new Address()
			{
				PostalAddress = source.PostalAddress,
				PhysicalAddress = source.PhysicalAddress,
			};
		}
	}
}
