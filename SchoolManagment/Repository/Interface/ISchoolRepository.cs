using SchoolManagment.Models;
using static SchoolManagment.Models.BaseModel;

namespace SchoolManagment.Repository.Interface
{
    public interface ISchoolRepository
    {
        Task<BaseResponseStatus> GetAllSchoolsByPagination(int pageno, int pagesize, string? searchText, int? talukaId, int? districtId);
        Task<SchoolDetails> GetSchoolDetails(int schoolId);
        Task<IEnumerable<GetSchools>> GetAllSchools();
        Task<int> AddNewSchools(Schools schools);
        Task<int> DeleteSchools(DeleteObj deleteobj); 
        Task<Schools> GetById(int id);
        Task<int> UpdateSchools(Schools schools);
        Task<NoOfSchoolsByPopulations> GetNoOfSchools(int id);    

    }
}
