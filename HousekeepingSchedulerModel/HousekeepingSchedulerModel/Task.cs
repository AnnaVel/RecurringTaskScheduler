using System;

namespace HousekeepingScheduler.Model
{
    public class Task
    {
        private readonly TaskToDoList owner;
        private readonly Activity activity;
        private TimeSpan? totalDuration;
        private DateTime dateFinished;

        public Activity Activity
        {
            get
            {
                return activity;
            }
        }

        public bool IsFinished
        {
            get
            {
                return this.totalDuration.HasValue;
            }
        }

        public TimeSpan? TotalDuration
        {
            get
            {
                return this.totalDuration;
            }
        }

        public DateTime DateFinished
        {
            get
            {
                return this.dateFinished;
            }
        }

        public Task(TaskToDoList owner, Activity activity)
        {
            Guard.ThrowExceptionIfNull(activity, "activity");
            Guard.ThrowExceptionIfNull(owner, "owner");

            this.activity = activity;
            this.owner = owner;
        }

        public void FinishTask(TimeSpan duration)
        {
            if (this.IsFinished)
            {
                throw new InvalidOperationException("This task is already finished.");
            }

            this.totalDuration = duration;
            this.dateFinished = DateTime.Now;

            this.owner.RemoveTask(this);
            this.activity.AddFinishedTask(this);
        }
    }
}
