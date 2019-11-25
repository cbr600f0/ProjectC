using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectC.Model
{
    public interface BaseModel
    {
        Guid Id { get; set; }

        DateTimeOffset CreatedAt { get; set; }

        DateTimeOffset? ModifiedAt { get; set; }

        DateTimeOffset? LastSynchronized { get; set; }

        Boolean Active { get; set; }
    }
}
