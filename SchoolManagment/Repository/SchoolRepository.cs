using Dapper;
using SchoolManagment.Context;
using SchoolManagment.Models;
using SchoolManagment.Repository.Interface;
using System.ComponentModel.Design;
using System.Data.Common;
using static SchoolManagment.Models.BaseModel;

namespace SchoolManagment.Repository
{
    public class SchoolRepository: ISchoolRepository
    {
        private readonly DapperContext _dapperContext;  
        public SchoolRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<int> AddNewSchools(Schools schools)
        {
            //Id,sName,vilageName,talukaId,districtId,createdBy,createddate,modifiedBy,modifiedDate
            var query = @"insert into tblSchools
                      
             (sName,vilageName,talukaId,createdBy,isDeleted) 

             values(@sName,@vilageName,@talukaId,@createdBy,0);

             SELECT CAST(SCOPE_IDENTITY() as int)";

            using(var connection=_dapperContext.CreateConnection())
            {

                var ret = await connection.QuerySingleOrDefaultAsync<int>(@"select id from 


                tblSchools where sName=@sName and talukaId=@talukaId and vilageName=@vilageName and isDeleted=0",
                 
                new { sName=schools.sName, talukaId = schools.talukaId, vilageName=schools.vilageName });

                if (ret > 0)
                {
                    return -1;
                }
                else
                {
                    var result = await connection.QuerySingleAsync<int>(query, schools);
                    return result;
                }
            }
        }

        public async Task<int> DeleteSchools(DeleteObj deleteobj)
        {
            var query = @"update tblSchools set modifiedBy=@modifiedBy,

                      modifiedDate=getdate(),isDeleted=1 where Id=@Id";

            using(var connection=_dapperContext.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, deleteobj);

                return result;
            }
        }

        public async Task<BaseResponseStatus> GetAllSchoolsByPagination(int pageno, int pagesize, string? searchText,int? talukaId,int? districtId)
        {
            BaseResponseStatus baseResponse = new BaseResponseStatus();
            PaginationModel pagination = new PaginationModel();
            List<SchoolsPaginationModel> schoollist = new List<SchoolsPaginationModel>();
            var query = @"select  ROW_NUMBER() OVER (ORDER BY s.Id desc)as SrNo,s.id as SchoolId,s.sName,

                        s.vilageName,t.talukaName,d.districtName from tblSchools s inner join tblTaluka t on s.talukaId=t.Id 

                        inner join tblDistrict d on t.districtId=d.Id

                         where (s.sName like '%'+@sName+'%' or @sName='') and s.isDeleted=0 and 

                         (s.vilageName like '%'+@vilageName+'%' or @vilageName='')

                         and (t.Id=@tId or @tId=0) and (d.Id=@dId or @dId=0) order by s.Id desc
  
                        OFFSET(@pageno - 1) *

                        @pagesize ROWS FETCH NEXT @pagesize ROWS ONLY; select @pageno as PageNo, 
 
                        count(distinct s.Id)as TotalPages

                       from tblSchools s inner join tblTaluka t on s.talukaId=t.Id inner join tblDistrict d on t.districtId=d.Id

                         where (s.sName like '%'+@sName+'%' or @sName='') and s.isDeleted=0 and 

                         (s.vilageName like '%'+@vilageName+'%' or @vilageName='')

                         and (t.Id=@tId or @tId=0) and (d.Id=@dId or @dId=0)"; 

            using (var connection =_dapperContext.CreateConnection())
            {
                if (pageno == 0)
                {
                    pageno = 1;
                }
                if (pagesize == 0)
                {
                    pagesize = 10;
                }
                if(searchText == null)
                {
                    searchText ="";
                }
       
                var values = new { pageno = pageno, pagesize = pagesize, sName = searchText,vilageName= searchText, tId= talukaId, dId = districtId };
                var result = await connection.QueryMultipleAsync(query, values);
                var sList = await result.ReadAsync<SchoolsPaginationModel>();
                schoollist = sList.ToList();
                var paginations = await result.ReadAsync<PaginationModel>();
                pagination = paginations.FirstOrDefault();
                int PageCount = 0;
                int last = 0;
                last = pagination.TotalPages % pagesize;
                pagination.PageCount = pagination.TotalPages;
                PageCount = pagination.TotalPages / pagesize;
                pagination.TotalPages = PageCount;
                if (last > 0)
                {
                    pagination.TotalPages = PageCount + 1;
                }

                baseResponse.ResponseData1 = schoollist;
                baseResponse.ResponseData2 = pagination;
            }
            return baseResponse;
        }

        public async Task<IEnumerable<GetSchools>> GetAllSchools()
        {
            //var query = "select * from tblSchools where isDeleted=0";

            var query = @"select s.sName ,t.talukaName ,d.districtName  from tblSchools s 

             inner join  tblTaluka t on s.talukaId=t.Id inner join  tblDistrict d on t.districtId = d.Id";
          
            using(var connections=_dapperContext.CreateConnection())
            {
                var result=await connections.QueryAsync<GetSchools>(query);  
                return result;  

            }
        }

        public async Task<Schools> GetById(int id)
        {
            var query = "select * from tblSchools where Id=@Id";
            using (var connection=_dapperContext.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<Schools>(query, new { Id=id });
                return result;
            }
        }

        public async Task<int> UpdateSchools(Schools schools)
        {
            //Id,sName,vilageName,talukaId,districtId,createdBy,createddate,modifiedBy,modifiedDate
            var query =@"update tblSchools set sName=@sName,vilageName=@vilageName,talukaId=@talukaId,

                districtId=@districtId,modifiedBy=@modifiedBy,modifiedDate=@modifiedDate where Id=@Id";
            using (var connection = _dapperContext.CreateConnection())
            {
                var ret = await connection.QuerySingleOrDefaultAsync<int>(@"select Id from tblSchools where sName=@sName",
                    
                    new { sName = schools.sName });

                if (ret > 0)
                {
                    schools.modifiedDate=DateTime.Now;

                    var result = await connection.ExecuteAsync(query, schools);


                    return result;
                }
                else
                {
                    return 0;
                }

 
            }

        }

        public async Task<NoOfSchoolsByPopulations> GetNoOfSchools(int id)
        {
            var query = @"select sum(t.tpopulation) as population,d.districtName,(select count(s.Id) from 

                        tblSchools s inner join tblTaluka t on s.talukaId=t.Id 

                        inner join tblDistrict d on t.districtId=d.Id where d.Id=@Id) as No_of_schools from tblTaluka t 
                
                        inner join tblDistrict d on t.districtId=d.Id where d.Id=@Id group by d.districtName";
            using(var connection=_dapperContext.CreateConnection())
            {
                var result=await connection.QueryAsync<NoOfSchoolsByPopulations>(query, new { Id = id });


                return result.FirstOrDefault();


            }

        }
    }
}
