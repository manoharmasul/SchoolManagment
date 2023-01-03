using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SchoolManagment.Models
{
    public class GetSchools
    {
        public string  sName { get; set; }
        public string talukaName { get; set; }
        public string districtName { get; set; }
        
    }
    public class pagination
    {
        public int pageNo { get; set; }
        public int NoOfRecord { get; set; }
       
    }
}
