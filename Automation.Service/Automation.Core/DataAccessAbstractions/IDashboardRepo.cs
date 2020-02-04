using System;
using System.Collections.Generic;
using System.Text;

namespace Automation.Core.DataAccessAbstractions
{
    public interface IDashboardRepo
    {
        int RecordsModified();
        string[] ModifiedFeeds();
        List<UserRole> GetAllRoles();
        string[] GetModifiedFeedsByMonth(int fromLastMonthCount);
        string[] GetModifiedFeedsByDay(int fromLastDayCount);
        string[] GetModifiedFeedsByMinute(int fromLastMinuteCount);
        string[] GetModifiedFeedsByHours(int fromLastHourCount);
        string[] GetModifiedFeedsBySecond(int fromLastSecondCount);
    }
}
