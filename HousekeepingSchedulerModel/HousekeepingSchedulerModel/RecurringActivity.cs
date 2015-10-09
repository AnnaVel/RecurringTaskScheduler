using System;

namespace HousekeepingScheduler.Model
{
    public class RecurringActivity : Activity
    {
        private readonly TimeSpan period;

        public TimeSpan Period
        {
            get
            {
                return this.period;
            }
        }

        public RecurringActivity(string name, string description, TimeSpan period)
            :base(name, description)
        {
            this.period = period;
        }
    }
}
