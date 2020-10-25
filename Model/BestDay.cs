using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class BestDay
    {
        public DateTime Date { get; set; }
        public int TotalGoals { get; set; }

        public BestDay() { }

        public BestDay(DateTime date, int goals)
        {
            Date = date;
            TotalGoals = goals;
        }
    }
}
