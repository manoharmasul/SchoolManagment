using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using SchoolManagment.Models;
using SchoolManagment.Repository;
using SchoolManagment.Repository.Interface;
using static SchoolManagment.Models.BaseModel;

namespace SchoolManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IStudentsRepository _studentRepository;
        IConfiguration configuration;
        public StudentsController(ILogger<StudentsController> logger, IStudentsRepository studentRepository, IConfiguration configuration)
        {
            _logger = logger;
            _studentRepository = studentRepository;
            this.configuration = configuration;
        }
        [HttpPost("AddNewStudents")]
        public async Task<IActionResult> AddNewStudents(StudentsModel studentmodel)
        {
            BaseResponseStatus baseresponse =new BaseResponseStatus();
            try
            {
                var student = await _studentRepository.AddNewStudents(studentmodel);
                if(student > 0)
                {
                    var rtnmsg = string.Format($"Student Added Succesfully with Id {student}");
                    _logger.LogInformation(rtnmsg);
                    _logger.LogDebug(string.Format($"StudentsController-AddNewStudents : Student Added Succesfully with Id {student}"));
                    baseresponse.StatusCode = StatusCodes.Status200OK.ToString();
                    baseresponse.StatusMessage = rtnmsg;
                    baseresponse.ResponseData = student;
                    return Ok(baseresponse);
                }
               else if(student == -1)
               {
                    var rtnmsg = string.Format($"The Student Is Already Present");
                    _logger.LogInformation($"StudentsController-AddNewStudents : The Student Is Already Present");
                    baseresponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                    baseresponse.StatusMessage = rtnmsg;
                    return Ok(baseresponse);
               }
               else
                {
                    var rtnmsg = string.Format($"Error While Adding Student");
                    _logger.LogInformation($"StudentsController-AddNewStudents : Error While Adding Student");
                    baseresponse.StatusCode=StatusCodes.Status409Conflict.ToString();
                    baseresponse.StatusMessage = rtnmsg;
                    return Ok(baseresponse);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                var rtnmsg = string.Format(ex.Message);
                _logger.LogInformation(rtnmsg);
                baseresponse.StatusCode=StatusCodes.Status409Conflict.ToString();
                baseresponse.StatusMessage = rtnmsg;


                return Ok(baseresponse);
            }
           
        }
        [HttpPost("BulkStudentsInsert")]
        public async Task<IActionResult> BulkStudentsInsert(SchoolClass studentmodel)
        {
            BaseResponseStatus baseresponse = new BaseResponseStatus();
            try
            {
                  var result= await _studentRepository.BulkStudentsInsert(studentmodel);    
                if(result>0)
                {
                    var rtnmsg = string.Format($"Students Added Successfully Whith School Id {studentmodel.schoolId} and ClassId {studentmodel.classId}");
                    _logger.LogInformation(rtnmsg);
                    _logger.LogDebug(string.Format($"StudentsController - BulkStudentsInsert : Students Added Succesfully "));
                    baseresponse.StatusCode=StatusCodes.Status200OK.ToString();
                    baseresponse.StatusMessage=rtnmsg;
                    baseresponse.ResponseData=result;
                    return Ok(baseresponse);

                }
                else
                {
                    var rtnmsg = string.Format($"Error While Adding Student");
                    _logger.LogInformation($"StudentsController-AddNewStudents : Error While Adding Student");
                    baseresponse.StatusCode = StatusCodes.Status409Conflict.ToString();
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
        [HttpPut("UpdateStudents")]
        public async Task<IActionResult> UpdateStudents(StudentsModel studentmodel)
        {
            BaseResponseStatus baseresponse=new BaseResponseStatus();

            try
            {

                var result=await _studentRepository.UpdateStudent(studentmodel); 
                if(result>0)
                {
                    var rtnmsg = string.Format($"Student Updated Successfully Id {studentmodel.Id}");
                    _logger.LogDebug(string.Format($"StudentsController-UpdateStudent : Student Updated Succefully {studentmodel.Id}"));
                    _logger.LogInformation(rtnmsg);
                    baseresponse.StatusCode=StatusCodes.Status200OK.ToString(); 
                    baseresponse.StatusMessage= rtnmsg;
                    baseresponse.ResponseData = result;
                    return Ok(baseresponse);

                }
                else if(result==-1)
                {
                    var rtnmsg = string.Format($"Student already present with mobileNo,fatherName and motherName");
                    _logger.LogDebug(string.Format($"StudentsController-UpdateStudent : Student Updated Succefully {studentmodel.Id}"));
                    _logger.LogInformation(rtnmsg);
                    baseresponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                    baseresponse.StatusMessage=rtnmsg;
                    return Ok(baseresponse);

                }
                else
                {
                    var rtnmsg = string.Format($"Error While Update Student");
                    _logger.LogInformation($"StudentsController-UpdateStudent : Error While Updating Student");
                    baseresponse.StatusCode = StatusCodes.Status409Conflict.ToString();
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
        [HttpDelete("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(DeleteObj deleteObj)
        {
            BaseResponseStatus baseresponse = new BaseResponseStatus();
            try
            {
                var result=await _studentRepository.DeleteStudent(deleteObj);
                if(result>0)
                {
                    var rtnmsg = string.Format($"Student Deleted With Id {deleteObj.Id}");
                    _logger.LogInformation(rtnmsg);
                    _logger.LogDebug(string.Format($"StudentController-DeleteStudent : Student Deleted With Id {deleteObj.Id}"));
                    baseresponse.StatusCode= StatusCodes.Status200OK.ToString();
                    baseresponse.StatusMessage= rtnmsg; 
                    baseresponse.ResponseData= result;
                    return Ok(baseresponse);

                }
                else 
                {
                    var rtnmsg = string.Format($"Error While Deleting Student");
                    _logger.LogInformation(rtnmsg);
                    baseresponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                    baseresponse.StatusMessage=rtnmsg;
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

        [HttpGet("GetStudentsPagination")]
        public async Task<ActionResult> GetStudentsPagination(int pageno, int pagesize, int schoolId, int Id, string? searchtext)
        {

            BaseResponseStatus responseDetails = new BaseResponseStatus();


            try
            {
                var schoolslist = await _studentRepository.GetStudentsPagination(pageno, pagesize, schoolId, Id, searchtext);

                List<GetStudentsPagi> slist = (List<GetStudentsPagi>)schoolslist.ResponseData1;
                if (slist.Count == 0)
                {
                    var returnmsg = string.Format("No record found");
                    _logger.LogDebug(returnmsg);
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = returnmsg;
                    return Ok(responseDetails);
                }
                var rtnmsg = string.Format("All Students records are fetched successfully.");
                _logger.LogInformation(rtnmsg);
                _logger.LogDebug(string.Format($"StudentController-DeleteStudent : All Students records are fetched successfully."));
                responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                responseDetails.StatusMessage = rtnmsg;
                responseDetails.ResponseData = slist;
                responseDetails.ResponseData1 = schoolslist.ResponseData2;
                return Ok(responseDetails);
            }
            catch (Exception ex)
            {
                //log error
                _logger.LogError(ex.Message);
                var returnMsg = string.Format(ex.Message);
                _logger.LogInformation(returnMsg);
                responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                responseDetails.StatusMessage = returnMsg;
                return Ok(responseDetails);

            }
        }
    }
}
