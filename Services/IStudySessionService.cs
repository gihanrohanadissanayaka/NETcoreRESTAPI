using Microsoft.AspNetCore.Mvc;
using myRestApiApp.Models;

namespace myRestApiApp.Services
{
    public interface IStudySessionService
    {
        ActionResult<IEnumerable<StudySession>> GetStudySessions();
        ActionResult<StudySession> GetStudySession(int id);
        ActionResult<StudySession> AddStudySession(StudySession studySession);
        ActionResult UpdateStudySession(StudySession studySession);
        ActionResult DeleteStudySession(int id);
    }

}
