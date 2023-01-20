using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolManagment.Models;
using SchoolManagment.Repository.Interface;

namespace SchoolManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IAttendanceRepository _attendanceRepository;
        IConfiguration configuration;
        public AttendanceController(ILogger<AttendanceController> logger, IAttendanceRepository attendanceRepository, IConfiguration configuration)
        {
            _logger = logger;
            _attendanceRepository = attendanceRepository;
            this.configuration = configuration;
        }
        [HttpPost("AddAttendance")]
        public async Task<IActionResult> AddAttendance(AttendanceModel attendanceModel)
        {
            BaseResponseStatus baseresponse=new BaseResponseStatus(); 
            try
            {
                var result=await _attendanceRepository.AddAttendance(attendanceModel);
                if(result>0)
                {
                    var rtnmsg = string.Format($"Attendance Registered Successfully with studentId {attendanceModel.studentId}");

                    _logger.LogInformation(rtnmsg);

                    _logger.LogDebug(string.Format($"AttendanceController-AddAttendance : Attendance Registered Successfully with studentId {attendanceModel.studentId}"));

                    baseresponse.StatusMessage = rtnmsg;

                    baseresponse.StatusCode = StatusCodes.Status200OK.ToString();

                    baseresponse.ResponseData = result;

                    return Ok(baseresponse);
                }
                else if(result==-1)
                {
                    var rtnmsg = string.Format($"The Attendance with Date and classId Already Registered and studentId");
                    _logger.LogInformation(rtnmsg); 
                    baseresponse.StatusMessage= rtnmsg; 
                    baseresponse.StatusCode=StatusCodes.Status409Conflict.ToString();
                    return Ok(baseresponse);    

                }
                else
                {
                    var rtnmsg = string.Format($"Error while Adding Attendance");
                    baseresponse.StatusCode= StatusCodes.Status409Conflict.ToString();
                    baseresponse.StatusMessage = rtnmsg;
                    return Ok(baseresponse);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                var rtnmsg = string.Format(ex.Message);
                _logger.LogInformation(rtnmsg);
                baseresponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                baseresponse.StatusMessage = rtnmsg;


                return Ok(baseresponse);

            }
        }
        [HttpPost("AddAttendanceBulk")]
        public async Task<IActionResult> AddAttendanceBulk(AttendanceBulkInsertModel attendance)
        {
            BaseResponseStatus baseresponse = new BaseResponseStatus();
            try
            {
                var result = await _attendanceRepository.AddAttendanceBulk(attendance);
                if (result > 0)
                {
                    var rtnmsg = string.Format($"Attendance Registered Successfully with classId {attendance.classId}");

                    _logger.LogInformation(rtnmsg);

                    _logger.LogDebug(string.Format($"AttendanceController-AddAttendance : Attendance Registered Successfully with classId {attendance.classId}"));

                    baseresponse.StatusMessage = rtnmsg;

                    baseresponse.StatusCode = StatusCodes.Status200OK.ToString();

                    baseresponse.ResponseData = result;

                    return Ok(baseresponse);
                }
                else if(result == -1)
                {
                    var rtnmsg = string.Format($"Attendance Is Already Taken with Date and classId ");
                    baseresponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                    baseresponse.StatusMessage = rtnmsg;
                    return Ok(baseresponse);
                }
                else
                {
                    var rtnmsg = string.Format($"Error while Adding Attendance");
                    baseresponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                    baseresponse.StatusMessage = rtnmsg;
                    return Ok(baseresponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var rtnmsg = string.Format(ex.Message);
                _logger.LogInformation(rtnmsg);
                baseresponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                baseresponse.StatusMessage = rtnmsg;


                return Ok(baseresponse);

            }
        }
        [HttpGet("GetAttendanceByDates")]
        public async Task<IActionResult> GetAttendanceByDates(DateTime dayDate1, DateTime dayDate2, int classId, int subjectId, int Id,int schoolId)
        {
            BaseResponseStatus baseresponse = new BaseResponseStatus();
            try
            {
                var result = await _attendanceRepository.GetAttendanceByDates(dayDate1, dayDate2, classId, subjectId, Id,schoolId);

                if(result.Count() >0)
                {
                    var rtnmsg = string.Format($"All Attendance Record Fetched ");
                    _logger.LogInformation(rtnmsg);
                    _logger.LogDebug(string.Format($"AttendanceController - GetAttendanceByDate : All Attendance Record Fetched"));
                    baseresponse.StatusCode=StatusCodes.Status200OK.ToString();
                    baseresponse.StatusMessage = rtnmsg;
                    baseresponse.ResponseData= result;
                    return Ok(baseresponse);

              
                }
                else
                {
                    var rtnmsg = string.Format($"No Record Founds");
                    _logger.LogInformation(rtnmsg);
                    _logger.LogDebug(string.Format($"AttendanceController - GetAttendanceByDate : No Record Founds"));
                    baseresponse.StatusCode=StatusCodes.Status409Conflict.ToString() ;  
                    baseresponse.StatusMessage = rtnmsg;
                    baseresponse.ResponseData = result;

                    return Ok(baseresponse);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                var rtnmsg = string.Format(ex.Message);
                _logger.LogInformation(rtnmsg);
                baseresponse.StatusMessage= rtnmsg;
                baseresponse.StatusCode = StatusCodes.Status409Conflict.ToString();

                return Ok(baseresponse);
            }
        }
        [HttpPut("UpdateAttendance")]
        public async Task<IActionResult> UpdateAttendance(AttendanceModel attendanceModel)
        {
            BaseResponseStatus baseresponse = new BaseResponseStatus();
            try
            {
                var result = await _attendanceRepository.UpdateAttendance(attendanceModel);
                if (result > 0)
                {
                    var rtnmsg = string.Format($"Attendance Updated Successfully with studentId {attendanceModel.studentId}");

                    _logger.LogInformation(rtnmsg);

                    _logger.LogDebug(string.Format($"AttendanceController-AddAttendance : Attendance Updated Successfully with studentId {attendanceModel.studentId}"));

                    baseresponse.StatusMessage = rtnmsg;

                    baseresponse.StatusCode = StatusCodes.Status200OK.ToString();

                    baseresponse.ResponseData = result;

                    return Ok(baseresponse);
                }
            
                else
                {
                    var rtnmsg = string.Format($"Error while Updating Attendance");
                    baseresponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                    baseresponse.StatusMessage = rtnmsg;
                    return Ok(baseresponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var rtnmsg = string.Format(ex.Message);
                _logger.LogInformation(rtnmsg);
                baseresponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                baseresponse.StatusMessage = rtnmsg;


                return Ok(baseresponse);

            }
        }
    }
}
