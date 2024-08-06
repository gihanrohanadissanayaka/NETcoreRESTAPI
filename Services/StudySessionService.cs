using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myRestApiApp.Data;
using myRestApiApp.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace myRestApiApp.Services
{
    public class StudySessionService : IStudySessionService
    {
        private readonly AppDbContext _context;
        private readonly IStudySessionValidationService _studySessionValidationService;

        public StudySessionService( AppDbContext context, IStudySessionValidationService validationService )
        {
            _context = context;
            _studySessionValidationService = validationService;
        }

        public ActionResult<IEnumerable<StudySession>> GetStudySessions()
        {
            try
            {

                    var studySessions = _context.StudySessions.Include(ss => ss.Module).ToList();
                    return new OkObjectResult(studySessions);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { Message = ex.Message }) { StatusCode = 500 };
            }
        }

        public ActionResult<StudySession> GetStudySession(int id)
        {
            try
            {
                    var studySession = _context.StudySessions.Include(ss => ss.Module).FirstOrDefault(ss => ss.Id == id);
                    if (studySession == null)
                    {
                        return new NotFoundResult();
                    }
                    return new OkObjectResult(studySession);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { Message = ex.Message }) { StatusCode = 500 };
            }
        }

        public ActionResult<StudySession> AddStudySession(StudySession studySession)
        {
            try
            {
                _studySessionValidationService.Validate( studySession );

                    var module = _context.Modules.FirstOrDefault(m => m.Id == studySession.ModuleId);
                    if (module == null)
                    {
                        return new NotFoundResult();
                    }

                // existing module to the context
                _context.Entry(module).State = EntityState.Unchanged;

                studySession.Module = module;
                _context.StudySessions.Add(studySession);
                _context.SaveChanges();
                return new OkObjectResult(studySession);
            }
            catch (ValidationException ex)
            {
                return new BadRequestObjectResult(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message }) { StatusCode = 500 };
            }
        }

        public ActionResult UpdateStudySession(StudySession studySession)
        {
            try
            {
                _studySessionValidationService.Validate(studySession);

                    var existingSession = _context.StudySessions.Find(studySession.Id);
                    if (existingSession == null)
                    {
                        return new NotFoundResult();
                    }

                    existingSession.Module = studySession.Module;
                    existingSession.SessionDate = studySession.SessionDate;
                    existingSession.SessionTime = studySession.SessionTime;
                    existingSession.SessionType = studySession.SessionType;
                    existingSession.Progress = studySession.Progress;

                    _context.SaveChanges();
                    return new OkResult();
            }
            catch (ValidationException ex)
            {
                return new BadRequestObjectResult(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { Message = ex.Message }) { StatusCode = 500 };
            }
        }

        public ActionResult DeleteStudySession(int id)
        {
            try
            {
                    var studySession = _context.StudySessions.Find(id);
                    if (studySession == null)
                    {
                        return new NotFoundResult();
                    }

                    _context.StudySessions.Remove(studySession);
                    _context.SaveChanges();
                    return new OkResult();
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { Message = ex.Message }) { StatusCode = 500 };
            }
        }
    }


}
