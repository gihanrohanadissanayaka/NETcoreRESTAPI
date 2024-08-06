using Microsoft.AspNetCore.Mvc;
using myRestApiApp.Models;

namespace myRestApiApp.Services
{
    public interface IFilterResultService
    {
        IEnumerable<StudySession> GetFilteredStudySessions(DateTime? startDate, DateTime? endDate, int? moduleId, bool? sessionType);
        string PredictResult(DateTime date, int moduleId);
    }
}
