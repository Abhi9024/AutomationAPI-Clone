using System;
using System.Collections.Generic;
using System.Text;

namespace Automation.Core.DataAccessAbstractions
{
    public interface IDashboardRepo
    {
        int RecordsModified();
        string[] ModifiedFeeds();
    }
}
