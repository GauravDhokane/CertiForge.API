using CertiForge.Application.Interfaces.Courses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertiForge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCoursesAsync()//what is the IActionResult and name of api how it is created 
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);

        }
    }

}
