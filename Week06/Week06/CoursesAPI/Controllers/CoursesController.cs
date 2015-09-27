﻿using System.Web.Http;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Services;
using System.Diagnostics;

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
			// TODO: figure out the requested language (if any!)
			// and pass it to the service provider!

            var languages = Request.Headers.AcceptLanguage;
            var requestedLanguage = "";

            
            int i = 0;
            foreach(var language in languages)
            {
                Debug.WriteLine("i: " + i + " " + language);
                i++;
            }
            

            if(languages.Count == 0)
            {
                requestedLanguage = "en";
            }
            else
            {
                //requestedLanguage = languages.
                
            }

			return Ok(_service.GetCourseInstancesBySemester(semester, page));
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
