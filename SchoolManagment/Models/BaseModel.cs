namespace SchoolManagment.Models
{
    public class BaseModel
    {
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public int modifiedBy { get; set; }
        public DateTime modifiedDate { get; set; }
        public string CreatedDateFormatdate
        {
            get
            {
                DateTime tmp;
                DateTime.TryParse(createdDate.ToString(), out tmp);
                return tmp.ToString("yyyy-MM-dd hh:mm:ss tt");
            }
        }
        public string ModifiedDateFormatdate
        {
            get
            {
                DateTime tmp;
                DateTime.TryParse(modifiedDate.ToString(), out tmp);
                return tmp.ToString("yyyy-MM-dd hh:mm:ss tt");
            }
        }
        public class DeleteObj
        {
            public int Id { get; set; }
            public int modifiedBy { get; set; }
            public DateTime modifiedDate { get; set; }

        }
    }
}
