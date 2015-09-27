using System.Web.Http;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Services;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace CoursesAPI.Controllers
{
	[RoutePrefix("api/courses")]
	public class CoursesController : ApiController
	{
		private readonly CoursesServiceProvider _service;

		public CoursesController()
		{
			_service = new CoursesServiceProvider(new UnitOfWork<AppDataContext>());
		}

		[HttpGet]
		[AllowAnonymous]
		public IHttpActionResult GetCoursesBySemester(string semester = null, int page = 1)
		{
            var languages = Request.Headers.AcceptLanguage;
            const string icelandic = "is";
            const string english = "en";
            string requestedLanguage = "en";
            double? highestCurrentQuality = 0.0;

            if(languages.Count != 0)
            {
                foreach(var language in languages)
                {
                    if (language.Quality == null)
                    {
                        requestedLanguage = language.Value;
                        break;
                    }
                    else if (highestCurrentQuality < language.Quality)
                    {
                        highestCurrentQuality = language.Quality;
                        requestedLanguage = language.Value;
                    }
                }

                if (requestedLanguage.Substring(0, 2) == "en") {
                    requestedLanguage = english;
                }
                else if (requestedLanguage.Substring(0, 2) == "is") {
                    requestedLanguage = icelandic;
                }
                else
                {
                    requestedLanguage = english;
                }
            }

            return Ok(_service.GetCourseInstancesBySemester(semester, page, requestedLanguage));
		}

		/// <summary>
		/// </summary>
		/// <param name="id"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("{id}/teachers")]
		public IHttpActionResult AddTeacher(int id, AddTeacherViewModel model)
		{
			var result = _service.AddTeacherToCourse(id, model);
			return Created("TODO", result);
		}
	}
}
