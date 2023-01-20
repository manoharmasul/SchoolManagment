using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;
using System.Data.SqlTypes;

namespace SchoolManagment.Models
{
    public class AttendanceModel:BaseModel
    {
        //Id,studentId,dayDate,Attendance,subjectId,classId
        public int Id { get; set; }
        public int studentId { get; set; }
        public DateTime dayDate { get; set; }
        public bool Attendance { get; set; }
        public int subjectId { get; set; }
        public int classId { get; set; }
        public int schoolId { get; set; }      
    }
    public class AttendaceInsertModel
    {
        public int studentId { get; set; }
        public bool Attendance { get; set; }
    }

    public class AttendanceBulkInsertModel : BaseModel
    {
        //Id,studentId,dayDate,Attendance,subjectId,classId
        public int Id { get; set; }
        public int studentId { get; set; }
        public DateTime dayDate { get; set; }
        public bool Attendance { get; set; }
        public int subjectId { get; set; }
        public int classId { get; set; }
        public int schoolId { get; set; }
        public List<AttendaceInsertModel> attendancelist { get; set; }
    }

    public class AttendaceReturn
    {
        public string studName { get; set; }
    }
    public class GetAttendance
    {
        public int SrNo { get; set; }
        public int Id { get; set; }
        public string studName { get; set; }
        public string Attendance { get; set; }
        public DateTime Date { get; set; }
        public string subjectName { get; set; }

    }
}
