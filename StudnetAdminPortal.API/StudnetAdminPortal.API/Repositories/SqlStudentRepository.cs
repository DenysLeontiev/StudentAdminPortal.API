using Microsoft.EntityFrameworkCore;
using StudnetAdminPortal.API.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudnetAdminPortal.API.Repositories
{
	public class SqlStudentRepository : IStudentRepository
	{
		private readonly StudentAdminContext _context;

		public SqlStudentRepository(StudentAdminContext context)
		{
			_context = context;
		}

		public async Task<Student> GetStudentAsync(Guid studentId)
		{
			var student = await _context.Student.Include(nameof(Gender)).Include(nameof(Address)).FirstOrDefaultAsync(student => student.Id == studentId);

			return student;
		}

		public async Task<List<Student>> GetStudentsAsync()
		{
			return await _context.Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
		}
	}
}
