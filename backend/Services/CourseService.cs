using Microsoft.Extensions.Options;
using backend.Authorization;
using backend.Entities;
using backend.Helpers;
using backend.Models;
using BCrypt.Net;
using AutoMapper;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Runtime.Intrinsics.X86;
using System.Text.Json;

namespace backend.Services
{
    public interface ICourseService
    {
        Course GetCourseById(string id);

        Course GetCourseByName(string name);
        
        IEnumerable<Course> GetAll();

        IEnumerable<Course> CourseRecommendations(int id);

        void AddCourse(CompletedCourses course);

        void RemoveCourse(CompletedCourses course);

        IEnumerable<CoursesCompleted> CompletedCourses(int id);
    }
    public class CourseService : ICourseService
    {
        private readonly DataContext _context;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public CourseService(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper
        )
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public IEnumerable<Course> GetAll() => _context.Courses;

        public Course GetCourseById(string id)
        {
            var course = _context.Courses.FirstOrDefault(x => x.CourseId == id);
            if (course == null) throw new KeyNotFoundException("Course not found");
            return course;
        }

        public Course GetCourseByName(string name)
        {
            var course = _context.Courses.FirstOrDefault(x => x.CourseName == name);
            if (course == null) throw new KeyNotFoundException("Course not found");
            return course;
        }

        public IEnumerable<Course> CourseRecommendations(int id)
        {
            var user = _context.Users.Find(id);
            if(user == null) throw new KeyNotFoundException("User not found");

            var courses = _context.CompletedCourses.Where(x => x.UserId == id);
            List<string> courses_taken = new List<string>();
            foreach (var course in courses)
            {
                courses_taken.Add(course.CourseId);
            }

            var results = _context.Courses;
            List<Course> recommendations = new List<Course>();
            foreach(Course course in results)
            {
                FindRecommendations(courses_taken, course, recommendations);
            }

            return recommendations;
        }

        public void FindRecommendations(List<string> courses, Course course, List<Course> recommendations)
        {
            if (!courses.Contains(course.CourseId))
            {
                string[] prereqs = course.Prerequisites.Split(",");
                foreach (string prereq in prereqs)
                {
                    if (courses.Contains(prereq))
                    {
                        recommendations.Add(course);
                    }
                }
            }
        }

        public void AddCourse(CompletedCourses course)
        {
            var course_query = _context.Courses.FirstOrDefault(x => x.CourseId == course.CourseId);
            if (course_query == null) throw new AppException("Course not found");

            var course_query2 = _context.CompletedCourses.FirstOrDefault(x => (x.UserId == course.UserId) && (x.CourseId == course.CourseId));
            if (course_query2 != null) throw new AppException("Course has already been registered.");

            _context.CompletedCourses.Add(course);
            _context.SaveChanges();
        }

        public void RemoveCourse(CompletedCourses course)
        {
            var course_query = _context.Courses.FirstOrDefault(x => x.CourseId == course.CourseId);
            if (course_query == null) throw new AppException("Course not found");

            var course_query2 = _context.CompletedCourses.FirstOrDefault(x => (x.UserId == course.UserId) && (x.CourseId == course.CourseId));
            if (course_query2 == null) throw new AppException("User has not registered this course.");

            //Change tracker provides access to information and operations for entity instances this context is tracking
            _context.ChangeTracker.Clear();
            _context.CompletedCourses.Remove(course);
            _context.SaveChanges();
        }

        public IEnumerable<CoursesCompleted> CompletedCourses(int id)
        {
            var course_query = _context.CompletedCourses.Where(x => x.UserId == id);

            var course_query2 = _context.Courses
                                        .Join(
                                            course_query,
                                            x => x.CourseId,
                                            y => y.CourseId,
                                            (x, y) => new
                                            {
                                                CourseId = x.CourseId,
                                                CourseName = x.CourseName,
                                                Credits = x.Credits,
                                                Prerequisites = x.Prerequisites,
                                                CoRequisites = x.CoRequisites,
                                                Description = x.Description,
                                                Availability = x.Availability,
                                                Completed = true
                                            }).ToList();

            List<CoursesCompleted> completed_courses = new List<CoursesCompleted>();
            foreach(var course in course_query2)
            {
                var prerequisites = course.Prerequisites.Split(",");
                if (prerequisites[0] == "N/A") { prerequisites = null; }

                var corequisites = course.CoRequisites.Split(",");
                if (corequisites[0] == "N/A") { corequisites = null; }

                completed_courses.Add(new CoursesCompleted(
                        course.CourseId,
                        course.CourseName,
                        course.Credits,
                        prerequisites,
                        corequisites,
                        course.Description,
                        course.Availability,
                        course.Completed
                    ));
            }

            return completed_courses;
        }

    }
}