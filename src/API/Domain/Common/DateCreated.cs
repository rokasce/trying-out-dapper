using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace API.Domain.Common;

public class DateCreated : ValueOf<DateTime, DateCreated>
{
    protected override void Validate()
    {
        if (Value > DateTime.Now)
        {
            const string message = "Your date of birth cannot be in the future";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(DateCreated), message)
            });
        }
    }
}

