using FluentValidation;

namespace Evently.Modules.Events.Application.Events.CreateEvent;

internal sealed class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.Location).NotEmpty();
        RuleFor(c => c.StartAtUtc).NotEmpty();
        RuleFor(c => c.EndAtUtc).Must((cmd, endAtUtc) => endAtUtc > cmd.StartAtUtc).When(c => c.EndAtUtc.HasValue);
    }
}
