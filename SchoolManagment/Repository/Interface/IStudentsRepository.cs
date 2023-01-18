using SchoolManagment.Models;
using static SchoolManagment.Models.BaseModel;

namespace SchoolManagment.Repository.Interface
{
    public interface IStudentsRepository
    {
        Task<int> AddNewStudents(StudentsModel studentmodel);
        Task<int> BulkStudentsInsert(SchoolClass studentmodel);
        Task<int> UpdateStudent(StudentsModel studentmodel);
        Task<BaseResponseStatus> GetStudentsPagination(int pageno,int pagesize,int schoolId, int Id,string? searchtext);
        Task<int> DeleteStudent(DeleteObj deleteObj);
    }
}
