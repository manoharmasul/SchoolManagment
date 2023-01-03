using Dapper;
using SchoolManagment.Context;
using SchoolManagment.Models;
using SchoolManagment.Repository.Interface;

namespace SchoolManagment.Repository
{
    public class TeacherAsyncRepository: ITeacherAsyncRepository
    {
        private readonly DapperContext dapperContext;
        public TeacherAsyncRepository(DapperContext dapperContext)
        {
            this.dapperContext = dapperContext;
        }

        public async Task<int> AddUpdateTeacher(Teachers teachers)
        {
            int result = 0;

            using (var connection = dapperContext.CreateConnection())


                if (teachers.Id == 0)
                {

                    var exist = await connection.QueryAsync<Teachers>(@"select * from tblTeachers where 

                       subjectId=subjectId and schoolId=schoolId");
                    if(exist.Count() > 0)
                    {
                        return -1;
                    }


                         result = await connection.QuerySingleAsync<int>(@"insert into tblTeachers (tName,Salary,createdBy)
             
                              values(@tName,@Salary,@createdBy);SELECT CAST(SCOPE_IDENTITY() as int)", teachers);

                        return result;
                    

                }
                else
                {
                         result = await connection.ExecuteAsync(@"update tblTeachers set tName=@tName,Salary=@Salary,

                  schoolId=@schoolId,modifiedBy=@modifiedBy where Id=@Id and isDeleted=0", teachers);
                        return result;
                    
                }
            
        }

        public async Task<int> DeleteTeacher(int Id)
        {
            var query = @"Update tblTeachers set isDeleted=1,modifiedDate=getDate() where Id=@Id";

            using(var connection = dapperContext.CreateConnection())
            {

                var result =await connection.ExecuteAsync(query, new {Id= Id });    


                return result;  
            }
        }

        public async Task<IEnumerable<Teachers>> GetAllTeachers()
        {
            var query = @"select * from tblTeachers where isDeleted=0";

            using(var connection=dapperContext.CreateConnection())
            {

                var result=await connection.QueryAsync<Teachers>(query);

                return result;
            }
        }

        public async Task<Teachers> GetTeacherById(int Id)
        {
            var query = @"select * from tblTeachers Where Id=@Id and isDeleted=0";

            using(var connection = dapperContext.CreateConnection())
            {
                var result=await connection.QueryAsync<Teachers>(query, new {Id = Id});

                return result.FirstOrDefault();
            }
             
        }
        //Id,tName,Salary,schoolId,createdBy,createdDate,modifiedBy,modifiedDate,isDeleted


    }
}
