using ValueOf;

namespace API.Domain.Common
{
    public class Content : ValueOf<string, Content>
    {
        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Value)) 
            {
                throw new ArgumentException("Content cannot be empty", nameof(Content));
            }
        }
    }
}
