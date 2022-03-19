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
            if (weekEnds != null) return CalculateEndDate(startDate, dayCount, weekEnds);
            return startDate.AddDays(dayCount - 1);
        }


        // This method returns the date, which is end date counted as a start date + duration + weekends.
        private DateTime CalculateEndDate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            int weekEndsDaysCount = 0;
            int indexWeekend = WeekendIndexBinarySearch(ref startDate, ref weekEnds);

            /// move start day if it is weekend
            if ((weekEnds[indexWeekend].StartDate <= startDate) && (startDate <= weekEnds[indexWeekend].EndDate))
            {
                startDate = weekEnds[indexWeekend].EndDate.AddDays(1);
                indexWeekend++;
            }

            DateTime endDate = startDate.AddDays(dayCount-1);

            /// check if there are weekends, count end date
            while (indexWeekend < weekEnds.Length)
            {
                if (weekEnds[indexWeekend].StartDate <= endDate && startDate < weekEnds[indexWeekend].EndDate)
                {
                    weekEndsDaysCount += ((int)(weekEnds[indexWeekend].EndDate - weekEnds[indexWeekend].StartDate).TotalDays + 1);
                    endDate = startDate.AddDays(dayCount + weekEndsDaysCount - 1);
                }
                else return endDate;

                indexWeekend++;
            }
            return endDate;
        }


        // This method returns the index of the first weekEnd ends after startDate
        private static int WeekendIndexBinarySearch(ref DateTime startDate, ref WeekEnd[] weekEnds)
        {
            int left = 0;
            int right = weekEnds.Length-1;
            while(left < right)
            {
                int mid = (left+right)/2;
                if( (weekEnds[mid].StartDate <= startDate)&&(startDate <= weekEnds[mid].EndDate) ) return mid;
                else if (weekEnds[mid].StartDate > startDate) right = mid;
                else left = mid+1;
            }
            return left;
        }
    }
}
