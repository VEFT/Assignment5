using System.Collections.Generic;
using System.Linq;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Exceptions;
using CoursesAPI.Services.Models.Entities;

namespace CoursesAPI.Services.Services
{
	public class CoursesServiceProvider
	{
		private readonly IUnitOfWork _uow;

		private readonly IRepository<CourseInstance> _courseInstances;
		private readonly IRepository<TeacherRegistration> _teacherRegistrations;
		private readonly IRepository<CourseTemplate> _courseTemplates; 
		private readonly IRepository<Person> _persons;

		public CoursesServiceProvider(IUnitOfWork uow)
		{
			_uow = uow;

			_courseInstances      = _uow.GetRepository<CourseInstance>();
			_courseTemplates      = _uow.GetRepository<CourseTemplate>();
			_teacherRegistrations = _uow.GetRepository<TeacherRegistration>();
			_persons              = _uow.GetRepository<Person>();
		}

		/// <summary>
		/// You should implement this function, such that all tests will pass.
		/// </summary>
		/// <param name="courseInstanceID">The ID of the course instance which the teacher will be registered to.</param>
		/// <param name="model">The data which indicates which person should be added as a teacher, and in what role.</param>
		/// <returns>Should return basic information about the person.</returns>
		public PersonDTO AddTeacherToCourse(int courseInstanceID, AddTeacherViewModel model)
		{
			var course = _courseInstances.All().SingleOrDefault(x => x.ID == courseInstanceID);
			if (course == null)
			{
				throw new AppObjectNotFoundException(ErrorCodes.INVALID_COURSEINSTANCEID);
			}

			// TODO: implement this logic!
			return null;
		}

		/// <summary>
		/// A method that accepts as input the language of the request, a semester 
        /// and a page number, and returns the course instances that appear
        /// on said page in the given language (given that the page number was valid).
		/// </summary>
        /// <param name="requestLanguage">The language of the request (i.e. "is" or "en")</param>
		/// <param name="semester">The query semester</param>
		/// <param name="page">1-based index of the requested page.</param>
		/// <returns>An envelope containing both our data (the courses)
        /// and the paging information</returns>
		public EnvelopeDTO<CourseInstanceDTO> GetCourseInstancesBySemester(string requestLanguage, string semester = null, int page = 1)
		{
            const string ICELANDIC = "is";
            const string DEFAULT_SEMESTER = "20153";
            const int PAGECOUNT = 10;

			if (string.IsNullOrEmpty(semester))
			{
				semester = DEFAULT_SEMESTER;
			}

            var pageCoursesList = new List<CourseInstanceDTO>();
            var allCoursesList = new List<CourseInstanceDTO>();

            var allCourses = (from c in _courseInstances.All()
                join ct in _courseTemplates.All() on c.CourseID equals ct.CourseID
                where c.SemesterID == semester
                select new CourseInstanceDTO
                {
                    Name = (requestLanguage == ICELANDIC ? ct.Name : ct.NameEN),
                    TemplateID = ct.CourseID,
                    CourseInstanceID = c.ID,
                    MainTeacher = ""
                });

            allCoursesList = allCourses.ToList();

            // If the page requested is a number less than 1,
            // we want pageCourseList to be empty and 
            // simply return an empty page.
            if (page > 0)
            { 
                pageCoursesList = allCourses.OrderBy(c => c.CourseInstanceID).Skip((page - 1) * PAGECOUNT).Take(PAGECOUNT).ToList();
            }

            EnvelopeDTO<CourseInstanceDTO> envelope = new EnvelopeDTO<CourseInstanceDTO>(pageCoursesList, page, PAGECOUNT, allCoursesList.Count);

			return envelope;
		}
	}
}
