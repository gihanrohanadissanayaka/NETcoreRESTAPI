using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myRestApiApp.Data;
using myRestApiApp.Models;
using myRestApiApp.Services;

namespace myRestApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly IFilterResultService _filterResultService;

        public ResultsController(IFilterResultService filterResultService)
        {
            _filterResultService = filterResultService;
        }

        // GET: api/results/filter
        [HttpGet("filter")]
        public ActionResult<IEnumerable<StudySession>> GetFilteredStudySessions(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] int? moduleId,
            [FromQuery] bool? sessionType)
        {
            try
            {
                var filteredStudySessions = _filterResultService.GetFilteredStudySessions(startDate, endDate, moduleId, sessionType);
                return new OkObjectResult(filteredStudySessions);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { Message = ex.Message }) { StatusCode = 500 };
            }
        }
        // GET: api/results/predictions
        [HttpGet("predictions")]
        public ActionResult<string> GetPrediction([FromQuery] DateTime date, [FromQuery] int moduleId)
        {
            try
            {
                var result = _filterResultService.PredictResult(date, moduleId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { Message = ex.Message }) { StatusCode = 500 };
            }
        }
    }
}
