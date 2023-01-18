using Dapper;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Context;
using SchoolManagment.Models;
using SchoolManagment.Repository.Interface;

namespace SchoolManagment.Repository
{
    public class StudentRepository:IStudentsRepository
    {
        private readonly DapperContext _dapperContext;
        public StudentRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<int> AddNewStudents(StudentsModel studentmodel)
        {   //studName,DOB,schoolId,classId,fatherName,motherName,mobileNo
           
            int result = 0;
            var query = @"insert into tblStudents 
           
              (studName,DOB,schoolId,classId,fatherName,motherName,mobileNo,createdBy,createdDate,isDeleted)

              Values(@studName,@DOB,@schoolId,@classId,@fatherName,@motherName,@mobileNo,@createdBy,GetDate(),0);

               SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connenction = _dapperContext.CreateConnection())
            {
                var checkquery = @"select * from tblStudents where studName=@studName 

                 and fatherName=@fatherName and motherName=@motherName and mobileNo=@mobileNo";

                var Id = await connenction.QueryAsync<StudentsModel>(checkquery, studentmodel);
                if(Id.Count() == 0)
                {
                    result = await connenction.QuerySingleAsync<int>(query, studentmodel);
                }
                else
                {
                    result = -1;
                }
                return result;
            }
        }

        public async Task<int> BulkStudentsInsert(SchoolClass studentmodel)
        {
            List<Student> studlist = new List<Student>();
            var result = 0;
            var query = @"insert into tblStudents 
           
              (studName,DOB,schoolId,classId,fatherName,motherName,mobileNo,createdBy,createdDate,isDeleted)

              Values(@studName,@DOB,@schoolId,@classId,@fatherName,@motherName,@mobileNo,@createdBy,GetDate(),0)";
            studlist = studentmodel.studentlist;

            using(var connection=_dapperContext.CreateConnection())
            {
                foreach(var student in studlist)
                {
                    studentmodel.studName = student.studName;

                    studentmodel.fatherName= student.fatherName;

                    studentmodel.motherName= student.motherName;

                    studentmodel.DOB= student.DOB;

                    studentmodel.mobileNo= student.mobileNo;

                    result =await connection.ExecuteAsync(query, studentmodel);
                }
                return result;
            }

        }

        public async Task<int> UpdateStudent(StudentsModel studentmodel)
        {
            var result = 0;
            var query = @"update tblStudents set studName=@studName,DOB=@DOB,schoolId=@schoolId,

            classId=@classId,fatherName=@fatherName,motherName=@motherName,mobileNo=@mobileNo,modifiedBy=@modifiedBy,modifiedDate=@modifiedDate where Id=@Id";
            using(var connection=_dapperContext.CreateConnection())
            {
                var checkquery = @"select * from tblStudents where studName=@studName 

                 and fatherName=@fatherName and motherName=@motherName and mobileNo=@mobileNo";

                var Id = await connection.QueryAsync<StudentsModel>(checkquery, studentmodel);
                if (Id.Count() == 0)
                {
                    result = await connection.ExecuteAsync(query, studentmodel);
                }
                else
                {
                    result = -1;
                }
                return result;
                
                
            }

        }

        public async Task<int> DeleteStudent(BaseModel.DeleteObj deleteObj)
        {
            var query = @"update tblStudents set isDeleted=1,modifiedBy=@modifiedBy,modifiedDate=@modifiedDate where Id=@Id";
            using(var connection=_dapperContext.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, deleteObj);

                return result;
            }
        }

        public async Task<BaseResponseStatus> GetStudentsPagination(int pageno, int pagesize, int schoolId, int Id, string? searchtext)
        {
            BaseResponseStatus baseresponse=new BaseResponseStatus();
            PaginationModel pagination = new PaginationModel();
            List<GetStudentsPagi> studentlist = new List<GetStudentsPagi>();
            
            var qeuery = @"select std.Id,std.studName,std.dob,std.gender,sch.sName,sch.vilageName as villageName,c.class as className,std.mobileNo from tblStudents std 

                        inner join tblSchools sch on std.schoolId=sch.Id

                        inner join tblClass c on std.classId=c.Id 

                        where (std.schoolId=@schoolId or @schoolId=0) 

                        and (std.Id=@Id or @Id=0)

                        and (std.studName=@searchtext or @searchtext='') 

                        and (std.studName=@searchtext or @searchtext='')

                        and (std.mobileNo=@searchtext or @searchtext='') 

                        and std.isDeleted=0 order by std.Id

                        OFFSET (@pageno - 1) * 

                        @pagesize ROWS FETCH NEXT @pagesize ROWS ONLY;

                        Select @pageno as PageNo,count (distinct std.Id) as TotalPages

                        from tblStudents std 

                        inner join tblSchools sch on std.schoolId=sch.Id

                        inner join tblClass c on std.classId=c.Id 

                        where (std.schoolId=@schoolId or @schoolId=0) 

                        and (std.Id=@Id or @Id=0)

                        and (std.studName=@searchtext or @searchtext='') 

                        and (std.studName=@searchtext or @searchtext='')

                        and (std.mobileNo=@searchtext or @searchtext='') 

                        and std.isDeleted=0";
            using (var connection = _dapperContext.CreateConnection())
            {
                if(pageno==0)
                {
                    pageno = 1;
                }
                if(pagesize==0)
                {
                    pagesize = 10;
                }
                if(searchtext==null)
                {
                    searchtext = "";
                }
                var valuses =new {pageno=pageno,pagesize=pagesize,searchtext=searchtext,schoolId=schoolId,Id=Id};
                var result = await connection.QueryMultipleAsync(qeuery, valuses);
                var studlist = await result.ReadAsync<GetStudentsPagi>();
                var pages = await result.ReadAsync<PaginationModel>();
                pagination = pages.FirstOrDefault();
                int PageCount = 0;
                int last=0;
                last = pagination.TotalPages % pagesize;
                pagination.TotalCount = pagination.TotalPages;
                PageCount=pagination.TotalPages / pagesize;
                pagination.TotalPages=PageCount;
                if(last>0)
                {
                    pagination.TotalPages = PageCount + 1;

                }
                baseresponse.ResponseData1 = studlist;
                baseresponse.ResponseData2 = pagination;
            }

               return baseresponse;
        }
    }
}
