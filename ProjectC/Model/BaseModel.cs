using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectC.Model
{
    public interface BaseModel
    {
        public Guid Id { get; set; }
    }
}
