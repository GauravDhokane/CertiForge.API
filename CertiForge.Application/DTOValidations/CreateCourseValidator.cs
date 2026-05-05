
using CertiForge.Application.DTOs;
using CertiForge.Application.Interfaces.Courses;
using FluentValidation;

namespace CertiForge.Application.DTOValidations
{
    public class CreateCourseValidator: AbstractValidator<CreateCourseDto>
    {
        public CreateCourseValidator(ICourseRepository repository)
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100)
                .MustAsync(async (title, cancellation) =>
                    !await repository.IsTitleDuplicateAsync(title))
                .WithMessage("The course title must be unique."); 

            //this is custom validation rule to check if the title is unique or not by calling
            //the IsTitleDuplicateAsync method from repository and passing the title as parameter and
            //if it returns true then it means the title is duplicate and we will return false to fail the validation
            //and if it returns false then it means the title is unique and we will return true to pass the validation

            RuleFor(x => x.Description)
               .NotNull()
               .NotEmpty()
              .MaximumLength(500);
        }
    }
}
