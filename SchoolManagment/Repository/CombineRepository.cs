using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.OpenApi.Models;
using SchoolManagment.Context;
using SchoolManagment.Models;
using SchoolManagment.Repository.Interface;

namespace SchoolManagment.Repository
{
    public class CombineRepository : ICombineRepository
    {
        private readonly DapperContext _dapperContext;
        public CombineRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<CombineClass> getNoOfSchoolsAndTeachers(int subjectId)
        {
            CombineClass combine = new CombineClass();
            double result = 0;
            double teacherCount = 0;

            using (var connection = _dapperContext.CreateConnection())
            {

                result = await connection.QuerySingleOrDefaultAsync<double>(@"select CAST(Count(Id) AS numeric) from tblSchools");

                combine.noofschools = result;

                teacherCount = await connection.QuerySingleOrDefaultAsync<double>

                (@"select CAST(Count(Id) AS numeric) from tblTeachers 

           where subjectId=@subjectId and isDeleted=0", new { subjectId = subjectId });

                combine.noofteachers = teacherCount;

                double vaccancy = 0;

                vaccancy = result - teacherCount;

                combine.noofvacancy = vaccancy;

                return combine;

            }
        }

        public async Task<List<Teachers>> GetTeachersBySalaryAndExperience(int noOfyears, string yearOrmonth, double salary)
        {
            var query = @"select * from tblTeachers where

            ( CAST(SUBSTRING(experience, 1, 2) AS int)>@exYears or @exYears=0)

            and RIGHT(experience, 5)=@period and (salary>@salary or @salary=0)";

            using (var connection = _dapperContext.CreateConnection())
            {

                var values = new { exYears = noOfyears, period = yearOrmonth, salary = salary };

                var result = await connection.QueryAsync<Teachers>(query, values);
               
                    return result.ToList();
                
            }

        }
    }
}
