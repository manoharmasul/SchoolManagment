namespace SchoolManagment.Models
{
    public class Teachers:BaseModel
    {
        //Id,tName,salary,createdBy,createdDate,modifiedBy,modifiedDate,isDeleted,schoolId

        public int Id { get; set; }
        public string tName { get; set; }
        public double salary { get; set; }
        public int schoolId { get; set; }
        public int subjectId { get; set; }
        public string experience { get; set; }  

    }
    public class TeachersDetails
    {
        public int Id { get; set; }
        public string tName { get; set; }
        public double salary { get; set; }      
        public string subjectName { get; set; }
        public string experience { get; set; }

    }
}
