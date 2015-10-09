using System;
using System.Collections.Generic;

namespace HousekeepingScheduler.Model
{
    public class TaskToDoList
    {
        private Dictionary<Activity, Task> activityToTask;

        public IEnumerable<Activity> ScheduledActivities
        {
            get
            {
                return this.activityToTask.Keys;
            }
        }

        public IEnumerable<Task> CurrentTasks
        {
            get
            {
                return this.activityToTask.Values;
            }
        }

        public TaskToDoList()
        {
            this.activityToTask = new Dictionary<Activity, Task>();
        }

        public void AddTask(Task task)
        {
            if (this.activityToTask.ContainsKey(task.Activity))
            {
                throw new InvalidOperationException("There is already a task scheduled for this activity.");
            }

            this.activityToTask.Add(task.Activity, task);
        }

        public void RemoveTask(Task task)
        {
            if (!this.activityToTask.ContainsKey(task.Activity))
            {
                throw new InvalidOperationException("There is no such task scheduled.");
            }

            if (this.activityToTask[task.Activity] != task)
            {
                throw new InvalidOperationException("This task is not currently scheduled.");
            }

            this.activityToTask.Remove(task.Activity);
        }
    }
}
