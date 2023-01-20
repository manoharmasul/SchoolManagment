using Dapper;
using Microsoft.VisualBasic;
using SchoolManagment.Context;
using SchoolManagment.Models;
using SchoolManagment.Repository.Interface;

namespace SchoolManagment.Repository
{
    public class AttendanceRepository:IAttendanceRepository
    {
        private readonly DapperContext _dapperContext;
        public AttendanceRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<int> AddAttendance(AttendanceModel attendanceModel)
        {
            //Id,studentId,dayDate,Attendance,subjectId,classId,createdBy,createdDate
            attendanceModel.createdDate=DateTime.Now;

            int result = 0;

            var query = @"insert into tblAttendance 

            (studentId,dayDate,Attendance,subjectId,classId,createdBy,createdDate,schoolId,isDeleted)

            values(@studentId,@dayDate,@Attendance,@subjectId,@classId,

            @createdBy,@createdDate,@schoolId,0)";



            using (var connection =_dapperContext.CreateConnection())
            {

                var count=await connection.QueryAsync<int>
                    
              (@"select id from tblAttendance where cast(dayDate as date)=@dayDate and studentId=@studentId 

               and subjectId=@subjectId and classId=classId and schoolId=@schoolId", 
              
              new {dayDate=attendanceModel.dayDate.Date, subjectId = attendanceModel.subjectId, 
                  
                  classId=attendanceModel.classId,schoolId=attendanceModel.schoolId,

                  studentId=attendanceModel.studentId
              });

                if(count.Count() > 0)
                {
                    return -1;
                }
                
                result = await connection.ExecuteAsync(query, attendanceModel);

                 return result;

            }
        }

        public async Task<int> AddAttendanceBulk (AttendanceBulkInsertModel attendancebultinsert)
        {
            attendancebultinsert.createdDate = DateTime.Now;

            int result = 0;

            var query = @"insert into tblAttendance 

            (studentId,dayDate,Attendance,subjectId,classId,createdBy,createdDate,schoolId,isDeleted)

            values(@studentId,@dayDate,@Attendance,@subjectId,@classId,

            @createdBy,@createdDate,@schoolId,0)";

            List<AttendaceInsertModel> attendancelists = new List<AttendaceInsertModel>();

            attendancelists= attendancebultinsert.attendancelist;



            using (var connection = _dapperContext.CreateConnection())
            {

                var idcount = await connection.QueryAsync<int>
                (@"select Id from tblAttendance where cast(dayDate as date)=@dayDate and  

                subjectId=@subjectId and classId=@classId and schoolId=@schoolId", 
               
               new { dayDate = attendancebultinsert.dayDate.Date,subjectId=attendancebultinsert.subjectId,
                   
                   classId=attendancebultinsert.classId,schoolId=attendancebultinsert.schoolId});

                if (idcount.Count() > 0)
                {
                    return -1;
                }
           
           
                foreach(var item in attendancelists)
                {
                    attendancebultinsert.studentId = item.studentId;
                    attendancebultinsert.Attendance = item.Attendance;



                    result = await connection.ExecuteAsync(query, attendancebultinsert);
                }

                return result;   


            }
        }

        public Task<int> DeleteAttendance(BaseModel.DeleteObj deleteObj)
        {
            throw new NotImplementedException();
        }

        public Task<List<AttendanceModel>> GetAttendance()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetAttendance>> GetAttendanceByDates(DateTime dayDate1, DateTime dayDate2, int classId, int subjectId, int Id,int schooId)
        {
            var query = @"select ROW_NUMBER() OVER (ORDER BY std.Id )as SrNo,std.Id,std.studName,att.Attendance, cast(att.dayDate as date) as Date, CASE

                        WHEN att.Attendance = 1  THEN 'Present'

                        WHEN att.Attendance = 0  THEN 'Absent'

                        ELSE 'Empty'

                        END as Attendance,sub.subjectName from tblStudents std

                        left join tblAttendance att on std.Id=att.studentId

                        left join tblSubjects sub on att.subjectId =sub.Id

                        where  cast(att.dayDate as date) between @dayDate1 and @dayDate2 

                        and (subjectId=@subjectId or @subjectId=0) and (att.classId=@classId) 

                        and (std.Id=@Id or @Id=0) and (std.schoolId=@schoolId or @schoolId=0) order by std.Id";

            using(var connection = _dapperContext.CreateConnection())
            {
                var result=await connection.QueryAsync<GetAttendance>(query,
                    
             new { dayDate1= dayDate1, dayDate2= dayDate2,classId=classId,subjectId=subjectId,Id=Id,schoolId= schooId });
              
                return result.ToList();
            }
        }

        public async Task<int> UpdateAttendance(AttendanceModel attendanceModel)
        {
            int resutl = 0;
            var query = @"update tblAttendance set studentId=@studentId,dayDate=@dayDate,Attendance=@Attendance,subjectId=@subjectId,classId=@classId,

            modifiedBy=@modifiedBy,modifiedDate=@modifiedDate,schoolId=@schoolId where cast(dayDate as date)=cast(@dayDate as date) and studentId=@studentId and subjectId=@subjectId";
            using(var connection=_dapperContext.CreateConnection())
            {
               
                var result=await connection.ExecuteAsync(query,attendanceModel);

                return result;
            }
        }
    }
}
