
using AutoMapper;
using CertiForge.Application.DTOs;
using CertiForge.Application.Interfaces.Courses;
using CertiForge.Domain.Entities;

namespace CertiForge.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        public CourseService(ICourseRepository courseRepository, IMapper mapper)
        {
            this._courseRepository = courseRepository;
            this._mapper = mapper;
        }
        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllCoursesAsync();//getting all courses from repository and storing it in courses variable
            var courseData = _mapper.Map<IEnumerable<CourseDto>>(courses);//mapping courses to courseDto using automapper
            courseData.ToList().ForEach(c =>
            {
                c.QuestionsAvailable = courses.Any(w => w.CourseId == c.CourseId && w.Questions.Count > 0);
                c.QuestionCount = courses.Where(w => w.CourseId == c.CourseId)
                    .SelectMany(s => s.Questions)
                    .Count();
            });

            return courseData;
        }

        public async Task<CourseDto?> GetCourseByIdAsync(int courseId)
        {
            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            return course == null ? null : _mapper.Map<CourseDto>(course);//if course is null return null otherwise map course to courseDto and return it
        }

        public async Task<bool> IsTitleDuplicateAsync(string title)
        {
            return await _courseRepository.IsTitleDuplicateAsync(title);
        }

        public async Task AddCourseAsync(CreateCourseDto createCourseDto)
        {
            var course = _mapper.Map<Course>(createCourseDto);
            course.CreatedBy = 1; // Replace with actual user context
            course.CreatedOn = DateTime.UtcNow;

            await _courseRepository.AddCourseAsync(course);
        }

        public async Task UpdateCourseAsync(int courseId, UpdateCourseDto updateCourseDto)
        {
            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            if (course == null) throw new KeyNotFoundException("Course not found");

            _mapper.Map(updateCourseDto, course);
            await _courseRepository.UpdateCourseAsync(course);
        }

        public async Task DeleteCourseAsync(int courseId)
        {
            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            if (course == null) throw new KeyNotFoundException($"Course with id {courseId} not found");

            await _courseRepository.DeleteCourseAsync(course);
        }

        public async Task UpdateDescriptionAsync(int courseId, string description)
        {
            await _courseRepository.UpdateDescriptionAsync(courseId, description);//calling update description method of repository and passing courseId and description to it
        }
    }
}
