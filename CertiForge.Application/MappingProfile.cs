using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CertiForge.Application.DTOs;
using CertiForge.Domain.Entities;

//here profile is coming from automapper and we will inherit that profile in our mapping profile and
//in the constructor of mapping profile we will create the mapping configuration between our entities and dtos

namespace CertiForge.Application
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDto>().ReverseMap();//converst from course to coursedto and reverse map is for converting from coursedto to course
            CreateMap<CreateCourseDto, Course>();
            CreateMap<UpdateCourseDto, Course>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
