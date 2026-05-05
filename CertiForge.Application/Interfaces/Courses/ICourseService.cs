using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertiForge.Application.DTOs;

namespace CertiForge.Application.Interfaces.Courses
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto?> GetCourseByIdAsync(int courseId);
        Task<bool> IsTitleDuplicateAsync(string title);
        Task AddCourseAsync(CreateCourseDto createCourseDto);
        Task UpdateCourseAsync(int courseId, UpdateCourseDto updateCourseDto);
        Task DeleteCourseAsync(int courseId);
        Task UpdateDescriptionAsync(int courseId, string description);
    }
}
