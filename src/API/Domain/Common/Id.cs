﻿using ValueOf;

namespace API.Domain.Common;

public class Id : ValueOf<Guid, Id>
{
    protected override void Validate()
    {
        if (Value == Guid.Empty) 
        {
            throw new ArgumentException("Id cannot be empty", nameof(Id));
        }
    }
}
