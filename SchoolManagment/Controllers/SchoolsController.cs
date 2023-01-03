using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolManagment.Models;
using SchoolManagment.Repository.Interface;
using static SchoolManagment.Models.BaseModel;

namespace SchoolManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly ISchoolRepository _schoolRepository;   
        public SchoolsController(IConfiguration configuartion, ISchoolRepository schoolRepository, ILoggerFactory loggerFactory)
        {
            _schoolRepository = schoolRepository;
            this.logger = loggerFactory.CreateLogger<SchoolsController>();

        }
        [HttpPost("/Add Schoos")]
        public async Task<IActionResult> AddNewSchools(Schools schools)
        {
            BaseResponseStatus baseResponse = new BaseResponseStatus();
            try
            {
                var result = await _schoolRepository.AddNewSchools(schools);
                if (result > 0)
                {
                    var rtnmsg = string.Format($"Record added successfully..with+ {result}");
                    baseResponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                    baseResponse.StatusMessage = rtnmsg;
                    baseResponse.ResponseData = result;
                    return Ok(baseResponse);
                }
                else if (result == -1)
                {
                    var rtnmsg = string.Format($"Record is already exist with same village name{schools.vilageName}....!");
                    baseResponse.StatusCode = StatusCodes.Status400BadRequest.ToString();
                    baseResponse.StatusMessage = rtnmsg;
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
            catch (Exception ex)
            {
                //log error
                logger.LogError(ex.Message);
                var returnMsg = string.Format(ex.Message);
                logger.LogInformation(returnMsg);
                baseResponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                baseResponse.StatusMessage = returnMsg;
                return Ok(baseResponse);
            }
        }
        [HttpPut("/Update Schools")]
        public async Task<IActionResult> UpdateSchools(Schools schools)
        {

            BaseResponseStatus baseResponse = new BaseResponseStatus();

            try
            {


                var result = await _schoolRepository.UpdateSchools(schools);

                if (result != 0)
                {
                    var rtnmsg = string.Format($"School updated successfully....Whith Id {schools.Id}");
                    logger.LogInformation(rtnmsg);
                    logger.LogDebug(string.Format($"SchoolsController : Completed Updating School record"));
                    baseResponse.StatusCode = StatusCodes.Status200OK.ToString();
                    baseResponse.StatusMessage = rtnmsg;
                    baseResponse.ResponseData = result;
                    return Ok(baseResponse);
                }
                else
                {
                    var rtnmsg = string.Format($"Schools Not Updated Something is Wrong");
                    baseResponse.StatusCode = StatusCodes.Status400BadRequest.ToString();
                    baseResponse.StatusMessage = rtnmsg;
                    baseResponse.ResponseData = "empty";
                    return Ok(baseResponse);

                }
            }
            catch(Exception ex)
            {
                //log error
                logger.LogError(ex.Message);
                var returnMsg = string.Format(ex.Message);
                logger.LogInformation(returnMsg);
                baseResponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                baseResponse.StatusMessage = returnMsg;
                return Ok(baseResponse);
            }

        }
        [HttpDelete("/DeleteObject")]
        public async Task<IActionResult> DeleteSchools(DeleteObj deleteobj)
        {
            BaseResponseStatus baseResponse = new BaseResponseStatus();

            try
            {

            
            var result=await _schoolRepository.DeleteSchools(deleteobj);    

            if(result > 0)
            {
                var rtnmsg = string.Format($"School deleted Successfully with Id{deleteobj.Id}");
                logger.LogInformation(rtnmsg);
                logger.LogDebug(string.Format($"SchoolsController : Completed deleting School record"));
                baseResponse.StatusCode = StatusCodes.Status200OK.ToString();
                baseResponse.StatusMessage=(rtnmsg);
                baseResponse.ResponseData = result;
                return Ok(baseResponse);
            }
            else
            {
                var rtnmsg = string.Format($"Schools Not deleted Something is Wrong");
                baseResponse.StatusCode = StatusCodes.Status400BadRequest.ToString();
                baseResponse.StatusMessage = rtnmsg;
                baseResponse.ResponseData = "empty";
                return Ok(baseResponse);
            }
            }
            catch (Exception ex)
            {
                //log error
                logger.LogError(ex.Message);
                var returnMsg = string.Format(ex.Message);
                logger.LogInformation(returnMsg);
                baseResponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                baseResponse.StatusMessage = returnMsg;
                return Ok(baseResponse);
            }
        }
        [HttpGet("/GetAllSchools")]
        public async Task<IActionResult> GetALlSchools()
        {
            BaseResponseStatus baseResponse = new BaseResponseStatus();

            try
            {


                var result = await _schoolRepository.GetAllSchools();
                if (result != null)
                {
                    var rtnmsg = string.Format($"All Records Fetch Successfully...!");
                    logger.LogInformation(rtnmsg);
                    logger.LogDebug(string.Format($"SchoolsController : completed schools records fetchign"));
                    baseResponse.StatusCode = StatusCodes.Status200OK.ToString();
                    baseResponse.StatusMessage = (rtnmsg);
                    baseResponse.ResponseData = result;
                    return Ok(baseResponse);

                }
                else
                {
                    var rtnmsg = string.Format($"No Records Found..!");
                    baseResponse.StatusCode = StatusCodes.Status400BadRequest.ToString();
                    baseResponse.StatusMessage = rtnmsg;
                    baseResponse.ResponseData = "empty";
                    return Ok(baseResponse);
                }
            }
            catch(Exception ex)
            {
                //log error
                logger.LogError(ex.Message);
                var returnMsg = string.Format(ex.Message);
                logger.LogInformation(returnMsg);
                baseResponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                baseResponse.StatusMessage = returnMsg;
                return Ok(baseResponse);

            }
           

        }
        [HttpGet("/GetSchoolsById")]
        public async Task<IActionResult>GetSchoolById(int id)
        {
            var resul=await _schoolRepository.GetById(id);
            return Ok(resul);
        }
        [HttpGet("GetAllSchoolsPagination")]
        public async Task<ActionResult> GetAllCourses(int pageno, int pagesize, string? searchText, int talukaId, int districtId)
        {

            BaseResponseStatus responseDetails = new BaseResponseStatus();


            try
            {
                var schoolslist = await _schoolRepository.GetAllSchoolsByPagination(pageno, pagesize, searchText, talukaId, districtId);

                List<SchoolsPaginationModel> slist = (List<SchoolsPaginationModel>)schoolslist.ResponseData1;
                if (slist.Count == 0)
                {
                    var returnmsg = string.Format("No record found");
                    logger.LogDebug(returnmsg);
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = returnmsg;
                    return Ok(responseDetails);
                }
                var rtnmsg = string.Format("All Schools records are fetched successfully.");
                logger.LogDebug(rtnmsg);
                responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                responseDetails.StatusMessage = rtnmsg;
                responseDetails.ResponseData = slist;
                responseDetails.ResponseData1 = schoolslist.ResponseData2;
                return Ok(responseDetails);
            }
            catch (Exception ex)
            {
                //log error
                logger.LogError(ex.Message);
                var returnMsg = string.Format(ex.Message);
                logger.LogInformation(returnMsg);
                responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                responseDetails.StatusMessage = returnMsg;
                return Ok(responseDetails);

            }
        }
        [HttpGet("/GetNoOfSchools")]
        public async Task<IActionResult> NoOfSchoolsByPopulation(int districtId)
        {
            BaseResponseStatus baseResponse = new BaseResponseStatus();

            try
            {



                var result = await _schoolRepository.GetNoOfSchools(districtId);

                if (result != null)
                {
                    var rtnmsg = string.Format($"All Records Fetch Successfully...!");
                    logger.LogInformation(rtnmsg);
                    logger.LogDebug(string.Format($"SchoolsController : completed schools records fetchign"));
                    baseResponse.StatusCode = StatusCodes.Status200OK.ToString();
                    baseResponse.StatusMessage = (rtnmsg);
                    baseResponse.ResponseData = result;
                    return Ok(baseResponse);

                }

                else
                {
                    var rtnmsg = string.Format($"No Records Found..!");
                    baseResponse.StatusCode = StatusCodes.Status400BadRequest.ToString();
                    baseResponse.StatusMessage = rtnmsg;
                    baseResponse.ResponseData = "empty";
                    return Ok(baseResponse);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var returnMsg = string.Format(ex.Message);
                logger.LogInformation(returnMsg);
                baseResponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                baseResponse.StatusMessage = returnMsg;
                return Ok(baseResponse);
            }

        }
    }
}
