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
            const string ICELANDIC = "is";
            const string ENGLISH = "en";
            var languages = Request.Headers.AcceptLanguage;
            string requestedLanguage = "en";
            double? highestCurrentQuality = 0.0;

            if(languages.Count != 0)
            {
                int i = 0;
                foreach(var language in languages)
                {
                    Debug.WriteLine("i: " + i);
                    Debug.WriteLine("Value" + language.Value);
                    Debug.WriteLine("Quality: " + language.Quality);
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
                    i++;
                }

                if (requestedLanguage.Substring(0, 2) == ENGLISH) {
                    requestedLanguage = ENGLISH;
                }
                else if (requestedLanguage.Substring(0, 2) == ICELANDIC) {
                    requestedLanguage = ICELANDIC;
                }
                else
                {
                    requestedLanguage = ENGLISH;
                }
            }

            Debug.WriteLine("FinalValue: " + requestedLanguage);

            return Ok(_service.GetCourseInstancesBySemester(requestedLanguage, semester, page));
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
