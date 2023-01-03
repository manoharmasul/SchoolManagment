using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Models;
using SchoolManagment.Repository;
using SchoolManagment.Repository.Interface;
using System;

namespace SchoolManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly ITeacherAsyncRepository _teacherAsyncRepository;   
        public TeachersController(IConfiguration configuartion,ITeacherAsyncRepository teacherAsyncRepository, ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<TeachersController>();
            _teacherAsyncRepository = teacherAsyncRepository;
        }

        [HttpPost("/AddandUpdate")]
        public async Task<IActionResult> AddTeachers(Teachers teachers)
        {
            BaseResponseStatus baseResponse = new BaseResponseStatus();

            var result = await _teacherAsyncRepository.AddUpdateTeacher(teachers);

            if (result > 0)
            {
                var rtnmsg = string.Format("Record added successfully..");
                logger.LogInformation(rtnmsg);
                logger.LogDebug(string.Format("TeachersController-AddTeachers:Calling By AddTeachers action."));
                baseResponse.StatusCode = StatusCodes.Status200OK.ToString();
                baseResponse.StatusMessage = rtnmsg;
                baseResponse.ResponseData = result;
                return Ok(baseResponse);
            }
            else if(result ==-1)
            {
                var rtnmsg = string.Format($"Teacher Already Exist In School With SchoolId   {teachers.schoolId}");
                baseResponse.StatusCode=StatusCodes.Status409Conflict.ToString();
                baseResponse.StatusMessage= rtnmsg;
                baseResponse.ResponseData = 0;
                return Ok(baseResponse);

            }
            else
            {
                var rtnmsg = string.Format($"Something is Wrong....!");
                baseResponse.StatusCode = StatusCodes.Status400BadRequest.ToString();
                baseResponse.StatusMessage = rtnmsg;
                baseResponse.ResponseData = 0;
                return Ok(baseResponse);
            }
        }
        [HttpGet("/GetTeacherById")]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            BaseResponseStatus baseResponse = new BaseResponseStatus();

            var result = await _teacherAsyncRepository.GetTeacherById(id);

            if (result != null)
            {
                var rtnmsg = string.Format("Records fetch successfully..");
                logger.LogInformation(rtnmsg);
                logger.LogDebug(string.Format("TeachersController-GetTeacherById: Calling  GetTeacherById action."));
                baseResponse.StatusCode = StatusCodes.Status200OK.ToString();
                baseResponse.StatusMessage = rtnmsg;
                baseResponse.ResponseData = result;
                return Ok(baseResponse);
            }
            else
            {
                var rtnmsg = string.Format($"Something is Wrong....!");
                baseResponse.StatusCode = StatusCodes.Status400BadRequest.ToString();
                baseResponse.StatusMessage = rtnmsg;
                baseResponse.ResponseData = 0;
                return Ok(baseResponse);
            }
        }
        [HttpDelete("/DeleteTeacher")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            BaseResponseStatus baseResponse = new BaseResponseStatus();

            var result = await _teacherAsyncRepository.DeleteTeacher(id);

            if (result > 0)
            {
                var rtnmsg = string.Format("Records Deleted successfully..");
                logger.LogInformation(rtnmsg);
                logger.LogDebug(string.Format("TeachersController-DeleteTeacher: Calling  DeleteTeacher action."));
                baseResponse.StatusCode = StatusCodes.Status200OK.ToString();
                baseResponse.StatusMessage = rtnmsg;
                baseResponse.ResponseData = result;
                return Ok(baseResponse);
            }
            else
            {
                var rtnmsg = string.Format($"Something is Wrong....!");
                baseResponse.StatusCode = StatusCodes.Status400BadRequest.ToString();
                baseResponse.StatusMessage = rtnmsg;
                baseResponse.ResponseData = 0;
                return Ok(baseResponse);
            }
        }
        [HttpGet("/GetTeachers")]
        public async Task<IActionResult> GetAllTeachers()
        {
            BaseResponseStatus baseResponse = new BaseResponseStatus();

            var result = await _teacherAsyncRepository.GetAllTeachers();

            if (result.Count() > 0)
            {
                var rtnmsg = string.Format("Records fetch successfully..");
                logger.LogInformation(rtnmsg);
                logger.LogDebug(string.Format("TeachersController-GetAllTeachers: Calling  GetAllTeachers action."));
                baseResponse.StatusCode = StatusCodes.Status200OK.ToString();
                baseResponse.StatusMessage = rtnmsg;
                baseResponse.ResponseData = result;
                return Ok(baseResponse);
            }
            else
            {
                var rtnmsg = string.Format($"Something is Wrong....!");
                baseResponse.StatusCode = StatusCodes.Status400BadRequest.ToString();
                baseResponse.StatusMessage = rtnmsg;
                baseResponse.ResponseData = 0;
                return Ok(baseResponse);
            }
        }
    }
}
