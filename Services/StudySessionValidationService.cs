using myRestApiApp.Models;

namespace myRestApiApp.Services
{
    public interface IStudySessionValidationService
    {
        bool Validate(StudySession studySession );
    }

    public class StudySessionValidationService : IStudySessionValidationService
    {
        public bool Validate(StudySession studySession )
        {
            if (studySession == null)
            {
                throw new ArgumentException("Study session is null.");
            }

            if (studySession.SessionDate > DateTime.Now)
            {
                throw new ArgumentException("Session date should be a past or current date.");
            }

            if (studySession.SessionTime < 0 || studySession.SessionTime > 24)
            {
                throw new ArgumentException("Session time should be between 0 and 24.");
            }

            if (studySession.Progress < 0 || studySession.Progress > 100 )
            {
                throw new ArgumentException("Progress should be between 0% and 100%.");
            }

            if (studySession.ModuleId <= 0)
            {
                throw new ArgumentException("ModuleId should be greater than 0.");
            }
            return true;
        }
    }

}
