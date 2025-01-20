using FluentValidation;
using TaskFlow.API.DTO.TaskCreateDTO;

namespace TaskFlow.API.Validators;

public class TaskCreateAndUpdateValidator : AbstractValidator<TaskCreateAndUpdateDTO>
{
    public TaskCreateAndUpdateValidator()
    {
        RuleFor(t => t.title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");
        
        RuleFor(t => t.description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(t => t.priority)
            .IsInEnum().WithMessage("Invalid priority value.");

        RuleFor(t => t.enddate)
            .GreaterThan(DateTime.UtcNow).WithMessage("End date must be in the future.");

        RuleFor(t => t.userid)
            .NotEmpty().WithMessage("UserId is required.");
    }
}
