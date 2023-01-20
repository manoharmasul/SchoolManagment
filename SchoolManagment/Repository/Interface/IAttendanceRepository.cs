using SchoolManagment.Models;
using static SchoolManagment.Models.BaseModel;

namespace SchoolManagment.Repository.Interface
{
    public interface IAttendanceRepository
    {
        Task<int> AddAttendance(AttendanceModel attendanceModel);
        Task<int> UpdateAttendance(AttendanceModel attendanceModel);
        Task<int> AddAttendanceBulk(AttendanceBulkInsertModel attendancebultinsert);
        
        Task<int> DeleteAttendance(DeleteObj deleteObj);
        Task<List<AttendanceModel>> GetAttendance();    
        Task<List<GetAttendance>> GetAttendanceByDates(DateTime dayDate1,DateTime dayDate2,int classId,int subjectId,int Id,int schoolId);    
      
    }
}
