using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lottery.Data;
using Lottery.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lottery.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StudentRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Student> GetStudentByIdAsync(string studentId)
        {
            return await _applicationDbContext.Students
                .FirstOrDefaultAsync(x => x.StudentId == studentId);
        }

        public async Task<Student> GetStudentForRound(string roundId, string studentId)
        {
            return await _applicationDbContext.Students
                .Where(x => x.RoundId == roundId && x.StudentId == studentId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Student>> GetAllStudentsForRoundAsync(string roundId)
        {
            return await _applicationDbContext.Students
                .Where(x => x.RoundId == roundId)
                .ToListAsync();
        }

        public void AddStudentForRound(string roundId, Student student)
        {
            if (roundId == null)
            {
                throw new ArgumentException(nameof(roundId));
            }
            if (student == null)
            {
                throw new ArgumentException(nameof(student));
            }

            student.RoundId = roundId;
            _applicationDbContext.Students.Add(student);
        }

        public void DeleteStudent(Student student)
        {
            if (student == null)
            {
                throw new ArgumentException(nameof(student));
            }

            _applicationDbContext.Students.Add(student);
        }

        public async Task<bool> StudentExistsAsync(string studentId)
        {
            if (studentId == null)
            {
                throw new ArgumentException(nameof(studentId));
            }

            return await _applicationDbContext.Students
                .AnyAsync(x => x.StudentId == studentId);
        }

        public Student GetRandomStudentForRound(string roundId)
        {
            if (roundId == null)
            {
                throw new ArgumentException(nameof(roundId));
            }

            return _applicationDbContext.Students
                .Where(x => x.RoundId == roundId)
                .AsEnumerable()
                .OrderBy(r => Guid.NewGuid())
                .FirstOrDefault();
        }

        public async Task<bool> SaveAsync()
        {
            return await _applicationDbContext.SaveChangesAsync() >= 0;
        }
    }
}