namespace SchoolManagment.Models
{
    public class StudentsModel: BaseModel
    {
        //studName,DOB,schoolId,classId,fatherName,motherName,mobileNo
        public int Id { get; set; }
        public string  studName { get; set; }
        public DateTime DOB { get; set; }
        public int schoolId { get; set; }
        public int classId { get; set; }
        public string fatherName { get; set; }
        public string motherName { get; set; }
        public string mobileNo { get; set; }
        public string gender { get; set; }
    }
    public class BulkStudentInsert
    {
        public string studName { get; set; }
        public DateTime DOB { get; set; }
        public int schoolId { get; set; }
        public int classId { get; set; }
        public string fatherName { get; set; }
        public string motherName { get; set; }
        public string mobileNo { get; set; }
        public string gender { get; set; }

    }
    public class SchoolClass : BaseModel
    {
        public int schoolId { get; set; }
        public int classId { get; set; }
        public string studName { get; set; }
        public DateTime DOB { get; set; }
        public string fatherName { get; set; }
        public string motherName { get; set; }
        public string mobileNo { get; set; }
        public string gender { get; set; }
        public List<Student> studentlist { get; set; }
    }
    public class Student 
    {
        public string studName { get; set; }
        public DateTime DOB { get; set; }
        public string fatherName { get; set; }
        public string motherName { get; set; }
        public string mobileNo { get; set; }
        public string gender { get; set; }
    }
    public class GetStudentsPagi
    {
        public int Id { get; set; }
        public string? studName { get; set; }
        public DateTime DOB { get; set; }
        public string? className { get; set; }
        public string? mobileNo { get; set; }
        public string? Gender { get; set; }
    }
}
