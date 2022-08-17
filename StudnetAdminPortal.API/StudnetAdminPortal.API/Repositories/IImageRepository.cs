using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudnetAdminPortal.API.Repositories
{
	public interface IImageRepository
	{
		Task<string> Upload(IFormFile file, string fileName);
	}
}
