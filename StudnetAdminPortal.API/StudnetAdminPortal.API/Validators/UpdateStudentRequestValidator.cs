using FluentValidation;
using StudnetAdminPortal.API.DomainModels;
using StudnetAdminPortal.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudnetAdminPortal.API.Validators
{
	public class UpdateStudentRequestValidator : AbstractValidator<UpdateStundetRequest> 
	{
		public UpdateStudentRequestValidator(IStudentRepository studentRepository)
		{
			RuleFor(x => x.FirstName).NotEmpty();
			RuleFor(x => x.LastName).NotEmpty();
			RuleFor(x => x.DateOfBirth).NotEmpty();
			RuleFor(x => x.Mobile).GreaterThan(99999).LessThan(100000000000);
			RuleFor(x => x.GenderId).NotEmpty().Must(id =>
			{
				var gender = studentRepository.GetGendersAsync().Result.ToList().FirstOrDefault(s => s.Id == id);

				if (gender != null)
				{
					return true;
				}
				return false;
			}).WithMessage("Please,select the correct gender");
			RuleFor(x => x.PhysicalAddress).NotEmpty();
			RuleFor(x => x.PostalAddress).NotEmpty();
		}
	}
}
