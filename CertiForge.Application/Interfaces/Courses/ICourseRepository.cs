using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertiForge.Domain.Entities;

namespace CertiForge.Application.Interfaces.Courses
{
    public interface ICourseRepository
    {
        //IEnumerable<Course> GetAllCourses();
        Task<IEnumerable<Course>> GetAllCoursesAsync();//writing above method into asynchronous way
        Task<Course?> GetCourseByIdAsync(int courseId);//nullable ruturn type of course
        Task<bool> IsTitleDuplicateAsync(string title);
        Task AddCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(Course course);
        Task UpdateDescriptionAsync(int courseId, string description);
    }
}
