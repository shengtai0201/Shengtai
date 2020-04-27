using System;
using System.Collections.Generic;
using System.Text;

namespace Shengtai.Data
{
    public enum DataProviders : int
    {
        None = 0,
        MySQL = 2,
        PostgreSQL = 4,
        MicrosoftSQLServer = 8,
        Oracle = 16
    }
}
