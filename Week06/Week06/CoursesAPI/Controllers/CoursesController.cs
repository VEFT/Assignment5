using System.Web.Http;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Services;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

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

            // Get accept-languages from the header
            var languages = Request.Headers.AcceptLanguage;
            var dictionary = new Dictionary<string, double?>();
            string requestedLanguage = ENGLISH;

            if (languages.Count != 0)
            {
                foreach(var language in languages)
                {
                    // If the quality is equal to null than we set it as 1.0.
                    if (language.Quality == null)
                    {
                        dictionary.Add(language.Value, 1.0);
                    }
                    else
                    {
                        dictionary.Add(language.Value, language.Quality);
                    }
                }

                // Get only the valid languages. I.e. languages that starts with "en" or "is".
                var validLanguages = dictionary.Where(d => d.Key.Substring(0, 2) == ENGLISH || d.Key.Substring(0, 2) == ICELANDIC);

                // If there are not valid languages than we use the default one which is "en".
                if(validLanguages != null)
                {
                    requestedLanguage = validLanguages.OrderByDescending(d => d.Value).FirstOrDefault().Key;
                }
            }

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
