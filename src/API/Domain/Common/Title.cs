using ValueOf;

namespace API.Domain;

public class Title : ValueOf<string, Title>
{
    protected override void Validate()
    {
        if (string.IsNullOrEmpty(Value)) 
        {
            throw new ArgumentException("Title cannot be empty", nameof(Title));
        }
    }
}
