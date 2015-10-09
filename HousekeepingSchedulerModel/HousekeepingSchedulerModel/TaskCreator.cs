using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace HousekeepingScheduler.Model
{
    internal class TaskCreator
    {
        private const int TimerMillisecondsInterval = 60000;
        private readonly Timer timer;

        private readonly HashSet<RecurringActivity> registeredActivities;
        private readonly TaskToDoList toDoList;

        public TaskCreator(TaskToDoList toDoList)
        {
            Guard.ThrowExceptionIfNull(toDoList, "toDoList");

            this.timer = new Timer(TimerMillisecondsInterval);
            this.timer.AutoReset = true;
            this.timer.Elapsed += TimerElapsed;
            this.timer.Start();
            this.registeredActivities = new HashSet<RecurringActivity>();
            this.toDoList = toDoList;
        }

        public void RegisterActivity(RecurringActivity activity)
        {
            if (this.registeredActivities.Contains(activity))
            {
                throw new InvalidOperationException("This activity is already registered.");
            }

            this.registeredActivities.Add(activity);
        }

        public bool UnregisterActivity(RecurringActivity activity)
        {
            return this.registeredActivities.Remove(activity);
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            this.UpdateToDoList();
        }

        private void UpdateToDoList()
        {
            List<Activity> activitiesToSchedule = this.CheckForUnscheduledDueActivities();

            foreach(Activity activity in activitiesToSchedule)
            {
                Task newTask = new Task(toDoList, activity);
                toDoList.AddTask(newTask);
            }
        }

        private List<Activity> CheckForUnscheduledDueActivities()
        {
            List<Activity> unscheduledActivities = new List<Activity>();

            foreach (RecurringActivity activity in this.registeredActivities)
            {
                bool activityIsDue = activity.CompletedTasks.LastTask.DateFinished + activity.Period >= DateTime.Now;
                bool activityIsScheduled = this.toDoList.ScheduledActivities.Contains(activity);

                if (activityIsDue && !activityIsScheduled)
                {
                    unscheduledActivities.Add(activity);
                }
            }

            return unscheduledActivities;
        }
    }
}
