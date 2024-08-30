using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class ScheduledTask
    {
        #region Variables
        public Person Person;
        public DateTime StartTime;
        public TimeSpan TimeSpan;
        public Location Location;
        public String TaskName;
        public bool IsFinished, IsStarted;
        #endregion

        #region Constructors
        public ScheduledTask() { }
        public ScheduledTask(Person person, DateTime startTime, TimeSpan timeSpan, Location location, string taskName)
        {
            this.IsFinished = false;
            this.IsStarted = false;
            this.Person = person;
            this.StartTime = startTime;
            this.TimeSpan =  timeSpan;
            this.Location = location;
            this.TaskName = taskName;
        }
        #endregion

        #region Methods
        public void Update()
        {
            if (!IsStarted && Universe.GameDate>StartTime)
            {
                IsStarted = true;
                Person.PerformTask(this);
            }
            else if(Universe.GameDate>StartTime+TimeSpan)
            {
                IsFinished = true;
                Person.RemoveTask(this);
            }
        }
        #endregion
    }
}
