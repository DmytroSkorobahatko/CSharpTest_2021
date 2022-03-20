using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        // implement method Calculate
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            DateTime lastWorkDate = startDate.AddDays(dayCount - 1);
           
            if (weekEnds == null)
                return lastWorkDate;

            int weekend_index = FindFirstWeekendIndex(ref startDate, ref weekEnds);

            if (startDate > weekEnds[weekend_index].EndDate)
                return lastWorkDate;
            
            if (startDate > weekEnds[weekend_index].StartDate)
            {
                int weekendsCount = weekEnds[weekend_index].StartDate.Subtract(startDate).Days;
                lastWorkDate = lastWorkDate.AddDays(weekendsCount);
            }

            while (weekend_index < weekEnds.Length && weekEnds[weekend_index].StartDate <= lastWorkDate)
            {
                int weekendsCount = weekEnds[weekend_index].EndDate.Subtract(weekEnds[weekend_index].StartDate).Days + 1;
                lastWorkDate = lastWorkDate.AddDays(weekendsCount);
                weekend_index++;
            }
            return lastWorkDate;
        }

        // returns the index of the first weekend ends after startDate
        private static int FindFirstWeekendIndex(ref DateTime firstDate, ref WeekEnd[] weekEnds)
        {
            int left_index = 0;
            int right_index = weekEnds.Length-1;
            while(left_index < right_index)
            {
                int mid_index = (left_index+right_index)/2;
                if( (weekEnds[mid_index].StartDate <= firstDate) && (firstDate <= weekEnds[mid_index].EndDate) ) 
                    return mid_index;
                else 
                    if (weekEnds[mid_index].StartDate > firstDate) 
                        right_index = mid_index;
                    else 
                        left_index = mid_index + 1;
            }
            return left_index;
        }
    }
}
