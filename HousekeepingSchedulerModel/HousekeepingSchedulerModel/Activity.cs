using System;

namespace HousekeepingScheduler.Model
{
    public class Activity
    {
        private readonly string name;
        private readonly string description;
        private readonly TaskCollection completedTasks;

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
        }

        public TaskCollection CompletedTasks
        {
            get
            {
                return this.completedTasks;
            }
        }

        public TimeSpan? AverageDuration
        {
            get
            {
                return completedTasks.AverageDuration;
            }
        }

        public Activity(string name, string description)
        {
            Guard.ThrowExceptionIfNullOrEmpty(name, "name");
            Guard.ThrowExceptionIfNullOrEmpty(description, "description");

            this.name = name;
            this.description = description;
            this.completedTasks = new TaskCollection(this);
        }

        public void AddFinishedTask(Task task)
        {
            if (!task.IsFinished)
            {
                throw new InvalidOperationException("The task should be finished before it can be added to the activity list.");
            }

            this.completedTasks.AddTask(task);
        }
    }
}
