using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ProjectC.Business.Interface
{
    public interface ISQLiteInterface
    {
        SQLiteConnection GetConnection();
    }
}
