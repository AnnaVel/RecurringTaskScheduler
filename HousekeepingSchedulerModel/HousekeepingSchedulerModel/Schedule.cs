using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HousekeepingScheduler.Model
{
    public class Schedule
    {
        private readonly TaskToDoList taskToDolist;
        private readonly TaskCreator taskCreator;

        public IEnumerable<Task> ToDoList
        {
            get
            {
                return this.taskToDolist.CurrentTasks;
            }
        }

        public Schedule()
        {
            this.taskToDolist = new TaskToDoList();
            this.taskCreator = new TaskCreator(this.taskToDolist);
        }
    }
}
