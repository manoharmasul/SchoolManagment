using SchoolManagment.Models;

namespace SchoolManagment.Repository.Interface
{
    public interface ICombineRepository
    {
        Task<CombineClass> getNoOfSchoolsAndTeachers(int subjectId);
        Task<List<Teachers>> GetTeachersBySalaryAndExperience(int noOfyears, string yearOrmonth, double salary);
    }
}
