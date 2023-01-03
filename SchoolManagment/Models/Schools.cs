using System.ComponentModel.DataAnnotations;

namespace SchoolManagment.Models
{
    public class Schools:BaseModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="School Name Is Required")]
        [Display(Name ="School Name")]
        public string sName { get; set; }
        [Required(ErrorMessage ="Vilage Name Is Required")]
        [Display(Name ="Vilage Name")]
        public string vilageName { get; set; }
        [Required(ErrorMessage ="Taluka Id Is Required")]
        [Display(Name="Taluka Id")]
        public int talukaId { get; set; }
       
    }
    public class SchoolsPaginationModel
    {

        public int SrNO { get; set; }

        public int schoolId { get; set; }
        public string sName { get; set; }

        public string vilageName { get; set; }

        public string talukaName { get; set; }
    
        public string districtName { get; set; }
    }
    public class NoOfSchoolsByPopulations
    {
        public double population { get; set; }
        public string districtName { get; set; }
        public int No_of_schools { get; set; }

    }
}
