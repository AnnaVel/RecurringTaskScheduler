using System;
using System.Collections.Generic;
using System.Linq;

namespace HousekeepingScheduler.Model
{
    public class TaskCollection
    {
        private readonly Activity owner;
        private readonly List<Task> tasks;
        private Task lastTask;
        private TimeSpan? averageDuration;

        public Task LastTask
        {
            get
            {
                return this.lastTask;
            }
        }

        public TimeSpan? AverageDuration
        {
            get
            {
                if (!averageDuration.HasValue)
                {
                    this.averageDuration = this.CalculateAverageDuration();
                }

                return this.averageDuration;
            }
        }

        public TaskCollection(Activity owner)
        {
            Guard.ThrowExceptionIfNull(owner, "owner");

            this.owner = owner;
            this.tasks = new List<Task>();
        }

        public void AddTask(Task task)
        {
            if (task.Activity != owner)
            {
                throw new InvalidOperationException("This task does not belong to this activity.");
            }

            this.tasks.Add(task);
            this.lastTask = task;
            this.InvalidateAverageDuration();
        }

        private void InvalidateAverageDuration()
        {
            this.averageDuration = null;
        }

        private TimeSpan? CalculateAverageDuration()
        {
            if (this.tasks.Count == 0)
            {
                return null;
            }

            TimeSpan totalTime = new TimeSpan(0);
            this.tasks.ForEach(t => totalTime += t.TotalDuration.Value);
            TimeSpan averageTime = TimeSpan.FromTicks(totalTime.Ticks / this.tasks.Count);
            return averageTime;
        }
    }
}
