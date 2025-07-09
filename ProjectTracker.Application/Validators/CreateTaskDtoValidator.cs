using FluentValidation;
using ProjectTracker.Application.Dtos;
using System;

namespace ProjectTracker.Application.Validators
{
    public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDto>
    {
        public CreateTaskDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Task title is required.")
                .MaximumLength(100).WithMessage("Title must be at most 100 characters.");

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.UtcNow.Date)
                .WithMessage("Due date must be in the future.");
        }
    }
}
