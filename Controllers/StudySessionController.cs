using Microsoft.AspNetCore.Mvc;
using myRestApiApp.Models;
using myRestApiApp.Services;

namespace myRestApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudySessionsController : ControllerBase
    {
        private readonly IStudySessionService _studySessionService;

        public StudySessionsController(IStudySessionService studySessionService)
        {
            _studySessionService = studySessionService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StudySession>> GetStudySessions()
        {
            var result = _studySessionService.GetStudySessions();
            if (result.Result is ObjectResult errorResult)
            {
                return errorResult;
            }
            return result;
        }

        [HttpGet("{id}")]
        public ActionResult<StudySession> GetModule(int id)
        {
            var result = _studySessionService.GetStudySession(id);
            if (result.Result is ObjectResult errorResult)
            {
                return errorResult;
            }
            return result;
        }

        [HttpPost]
        public ActionResult<StudySession> PostStudySession(StudySession studySession)
        {
            var result = _studySessionService.AddStudySession(studySession);
            if (result.Result is ObjectResult errorResult)
            {
                return errorResult;
            }
            return CreatedAtAction(nameof(GetModule), new { id = studySession.Id }, studySession);
        }

        [HttpPut("{id}")]
        public IActionResult PutStudySession(int id, StudySession studySession)
        {
            if (id != studySession.Id)
            {
                return BadRequest();
            }

            var result = _studySessionService.UpdateStudySession(studySession);
            if (result is ObjectResult errorResult)
            {
                return errorResult;
            }
            return result;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudySession(int id)
        {
            var result = _studySessionService.DeleteStudySession(id);
            if (result is ObjectResult errorResult)
            {
                return errorResult;
            }
            return result;
        }
    }
}
