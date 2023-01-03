using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Models;
using SchoolManagment.Repository.Interface;

namespace SchoolManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombineController : ControllerBase
    {

   
        private readonly ILogger _logger;
        private readonly ICombineRepository _combineRepository;
        public CombineController(IConfiguration configuartion, ICombineRepository combineRepository, ILoggerFactory loggerFactory)
        {
            _combineRepository = combineRepository;
            _logger = loggerFactory.CreateLogger<CombineController>();

        }

        [HttpGet]

        public async Task<IActionResult> GetNoVaccancies(int subjectId)
        {
            BaseResponseStatus baseresponse=new BaseResponseStatus();

                var result = await _combineRepository.getNoOfSchoolsAndTeachers(subjectId);

            if (result != null)
            {
              var rtnmsg = string.Format($"Record Fetch successfull with subjectId{subjectId}");

              _logger.LogInformation(rtnmsg);

              _logger.LogDebug(string.Format($"CombineController: Compete Record Fetch"));

              baseresponse.StatusCode = StatusCodes.Status200OK.ToString();

              baseresponse.StatusMessage = rtnmsg;

              baseresponse.ResponseData = result;

              return Ok(baseresponse);

            }
            else
            {

              var rtnmsg = string.Format($"Somthing is Worng");

              baseresponse.StatusCode= StatusCodes.Status409Conflict.ToString();

              baseresponse.StatusMessage = rtnmsg;

              baseresponse.ResponseData = 0;

              return Ok(baseresponse);

            }

        }
        [HttpGet("Get Teachers By Experience and Salary")]
        public async Task<IActionResult> GetTeachersByExpAndSalary(int noOfyears, string yearOrmonth, double salary)
        {
            BaseResponseStatus baseresponse = new BaseResponseStatus();

            var result=await _combineRepository.GetTeachersBySalaryAndExperience(noOfyears, yearOrmonth, salary);
            if(result != null)
            {
                var rtnmsg = string.Format($"Record Fetch Successfully...!");

                _logger.LogInformation(rtnmsg);

                _logger.LogDebug(string.Format($"CombineController: Record Fetch..!"));

                baseresponse.StatusMessage= rtnmsg; 

                baseresponse.StatusCode= StatusCodes.Status200OK.ToString();

                baseresponse.ResponseData= result;

                return Ok(baseresponse);

            }
            else
            {
                var rtnmsg = string.Format($"Something is Wrong");

                baseresponse.StatusMessage = rtnmsg;

                baseresponse.StatusCode = StatusCodes.Status409Conflict.ToString();

                baseresponse.ResponseData = result;

                return Ok(baseresponse);

            }
        }
    }
}
