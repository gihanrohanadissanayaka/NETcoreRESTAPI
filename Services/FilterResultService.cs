using Microsoft.EntityFrameworkCore;
using myRestApiApp.Data;
using myRestApiApp.Models;

namespace myRestApiApp.Services
{
    public class FilterResultService : IFilterResultService
    {
        private readonly AppDbContext _context;

        public FilterResultService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<StudySession> GetFilteredStudySessions(DateTime? startDate, DateTime? endDate, int? moduleId, bool? sessionType)
        {
            IQueryable<StudySession> query = _context.StudySessions.Include(ss => ss.Module);

            // Apply filters based on optional parameters
            if (startDate.HasValue)
            {
                query = query.Where(ss => ss.SessionDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(ss => ss.SessionDate <= endDate.Value);
            }

            if (moduleId.HasValue)
            {
                query = query.Where(ss => ss.ModuleId == moduleId.Value);
            }

            if (sessionType.HasValue)
            {
                query = query.Where(ss => ss.SessionType == sessionType.Value);
            }

            return query.ToList();
        }
        public string PredictResult(DateTime date, int moduleId)
        {
            var sessions = _context.StudySessions
                .Where(ss => ss.ModuleId == moduleId)
                .ToList();

            double predictedProgress = PredictFutureProgress(sessions, date);

            return "RESULT_KEY" + "$" + predictedProgress + "~" + GetGrade(predictedProgress) + "~" +
                sessions.Where(s => s.SessionType).Sum(s => s.SessionTime) + "~" +
                sessions.Where(s => !s.SessionType).Sum(s => s.SessionTime);
        }

        public static double PredictFutureProgress(List<StudySession> sessions, DateTime futureDate)
        {
            if (sessions.Count == 0)
                throw new ArgumentException("Session list is empty.");

            List<double> dates = sessions.Select(s => s.SessionDate.ToOADate()).ToList();
            List<double> sessionTimes = sessions.Where(s => s.SessionType == true).Select(s => s.SessionTime).ToList();
            List<double> progresses = sessions.Select(s => s.Progress).ToList();

            double avgDate = dates.Average();
            double avgSession = sessionTimes.Average();
            double avgProgress = progresses.Average();

            double numerator1 = dates.Zip(progresses, (x, y) => (x - avgDate) * (y - avgProgress)).Sum();
            double numerator2 = sessionTimes.Zip(progresses, (t, y) => (t - avgSession) * (y - avgProgress)).Sum();
            double denominator1 = dates.Sum(x => Math.Pow(x - avgDate, 2));
            double denominator2 = sessionTimes.Sum(t => Math.Pow(t - avgSession, 2));

            double slope1 = numerator1 / denominator1;
            double slope2 = numerator2 / denominator2;
            double intercept = avgProgress - slope1 * avgDate - slope2 * avgSession;

            double futureDateAsNumber = futureDate.ToOADate();
            double predictedProgress = intercept + slope1 * futureDateAsNumber + slope2 * 1.5;

            // predict between 0 and 100
            predictedProgress = Math.Max(0, Math.Min(100, predictedProgress));

            return predictedProgress;
        }
        public static string GetGrade(double percentage)
        {
            if (percentage >= 90)
            {
                return "A+";
            }
            else if (percentage >= 85)
            {
                return "A";
            }
            else if (percentage >= 80)
            {
                return "A-";
            }
            else if (percentage >= 75)
            {
                return "B+";
            }
            else if (percentage >= 70)
            {
                return "B";
            }
            else if (percentage >= 65)
            {
                return "B-";
            }
            else if (percentage >= 60)
            {
                return "C+";
            }
            else if (percentage >= 55)
            {
                return "C";
            }
            else if (percentage >= 50)
            {
                return "C-";
            }
            else
            {
                return "F";
            }
        }
    }
}