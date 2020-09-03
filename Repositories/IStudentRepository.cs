using System.Collections.Generic;
using System.Threading.Tasks;
using Lottery.Entities;

namespace Lottery.Repositories
{
    public interface IStudentRepository
    {
        Task<int> GetLengthStudentsForRoundAsync(string roundId);

        Task<IEnumerable<Student>> GetStudentsForRoundAsync(string roundId, int skipNumber, int takeNumber);

        Task<Student> GetStudentByIdAsync(string studentId);

        Task<Student> GetStudentForRoundAsync(string roundId, string studentId);

        Task<IEnumerable<Student>> GetAllStudentsForRoundAsync(string roundId);

        void AddStudentForRound(string roundId, Student student);

        void DeleteStudent(Student student);

        Task<bool> StudentExistsAsync(string studentId);
        
        Student GetRandomStudentForRound(string roundId);

        Task<bool> SaveAsync();
    }
}