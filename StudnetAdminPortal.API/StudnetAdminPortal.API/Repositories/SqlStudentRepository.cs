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

		public async Task<Student> AddStudent(Student student)
		{
			var addedStudent = await _context.Student.AddAsync(student);
			await _context.SaveChangesAsync();
			return addedStudent.Entity;
		}

		public async Task<Student> DeleteStudent(Guid studentId)
		{
			var student = await GetStudentAsync(studentId);
			if(student != null)
			{
				_context.Student.Remove(student);
				await _context.SaveChangesAsync();
				return student;
			}

			return null;
		}

		public async Task<bool> Exist(Guid studentId)
		{
			return await _context.Student.AnyAsync(student => student.Id == studentId);
		}

		public async Task<List<Gender>> GetGendersAsync()
		{
			return await _context.Gender.ToListAsync();
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

		public async Task<Student> UpdateStudent(Guid studentId, Student studentRequest)
		{
			var existingStudent = await GetStudentAsync(studentId);
			if (existingStudent != null)
			{
				existingStudent.FirstName = studentRequest.FirstName;
				existingStudent.LastName = studentRequest.LastName;
				existingStudent.DateOfBirth = studentRequest.DateOfBirth;
				existingStudent.Email = studentRequest.Email;
				existingStudent.Mobile = studentRequest.Mobile;
				existingStudent.GenderId = studentRequest.GenderId;
				existingStudent.Address.PhysicalAddress = studentRequest.Address.PhysicalAddress;
				existingStudent.Address.PostalAddress = studentRequest.Address.PostalAddress;

				//_context.Update(existingStudent);

				await _context.SaveChangesAsync();

				return existingStudent;
			}

			return null;
		}
	}
}
