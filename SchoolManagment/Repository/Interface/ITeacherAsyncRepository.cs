using SchoolManagment.Models;

namespace SchoolManagment.Repository.Interface
{
    public interface ITeacherAsyncRepository
    {
        Task<int> AddUpdateTeacher(Teachers teachers);
        Task<IEnumerable<Teachers>> GetAllTeachers();
        Task<Teachers> GetTeacherById(int Id);
        Task<int> DeleteTeacher(int Id);
    }
}
