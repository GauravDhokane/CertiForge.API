using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertiForge.Application.Interfaces.Courses;
using CertiForge.Domain.Entities;
using CertiForge.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CertiForge.Infrastructure
{
    public class CourseRepository : ICourseRepository
    {
        public CertiForgeContext certiForgeContext;
        public CourseRepository(CertiForgeContext certiForgeContext)//always inject dbcontext into constructor
        {
            this.certiForgeContext = certiForgeContext;
        }
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await certiForgeContext.Courses.ToListAsync();       
        }
        public async Task<Course?> GetCourseByIdAsync(int courseId)
        {
            return await certiForgeContext.Courses.FindAsync(courseId);
        }

        public async Task<bool> IsTitleDuplicateAsync(string title)
        {
            return await certiForgeContext.Courses.AnyAsync(c => c.Title == title);
        }

        public async Task AddCourseAsync(Course course)
        {
            certiForgeContext.Courses.Add(course);
            await certiForgeContext.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(Course course)
        {
            certiForgeContext.Courses.Update(course);
            await certiForgeContext.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(Course course)
        {
            certiForgeContext.Courses.Remove(course);
            await certiForgeContext.SaveChangesAsync();
        }

        public async Task UpdateDescriptionAsync(int courseId, string description)
        {
            var course = await certiForgeContext.Courses.FindAsync(courseId);
            if (course == null) throw new KeyNotFoundException("Course not found.");

            course.Description = description;
            await certiForgeContext.SaveChangesAsync();
        }
    }
}

/*
Async 


 */
