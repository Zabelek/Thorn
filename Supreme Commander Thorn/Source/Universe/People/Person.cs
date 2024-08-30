using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Supreme_Commander_Thorn
{
    public class Person
    {
        #region Variables
        public enum BodyType { Skinny, Normal, Athletic, Fat }
        public int PersonID;
        public String FirstName;
        public String LastName;
        public DateTime DateOfBirth;
        public String History;
        public Faction Faction;
        public int Height;
        public BodyType PersonBodyType;
        [XmlIgnore]
        public Location CurrentLocation, HomeLocation;
        public List<ScheduledTask> PlannedTasks = new();
        public ScheduledTask CurrentlyPerformedTask;
        public Inventory Inventory;
        #endregion

        #region Constructors
        public Person()
        {
            PersonID = Universe.GiveNewPersonId();
            Universe.AddPerson(this);
        }
        #endregion

        #region Methods
        public void ScheduleTask(DateTime startTime, TimeSpan timeSpan, Location location, string taskName)
        { 
            this.PlannedTasks.Add( new ScheduledTask(this, startTime, timeSpan, location, taskName));
        }
        public void ScheduleTask(DateTime startTime, Location location, string taskName)
        {
            this.PlannedTasks.Add(new ScheduledTask(this, startTime, new TimeSpan(1,0,0,0,0), location, taskName));
        }
        public void ScheduleTask(DateTime startTime, string taskName)
        {
            this.PlannedTasks.Add(new ScheduledTask(this, startTime, new TimeSpan(1, 0, 0, 0, 0), CurrentLocation, taskName));
        }
        public void Update()
        {
            foreach(ScheduledTask task in this.PlannedTasks) { task.Update(); }
        }

        public void PerformTask(ScheduledTask task)
        {
            this.CurrentlyPerformedTask = task;
            this.CurrentLocation = task.Location;
        }

        public void RemoveTask(ScheduledTask task)
        {
            this.PlannedTasks.Remove(task);
            if(this.CurrentlyPerformedTask == task) 
                this.CurrentlyPerformedTask = null;
        }
        public void GoHome()
        {
            if(this.HomeLocation != null)
                this.CurrentLocation = this.HomeLocation;
        }
        public void Kill()
        {
            this.CurrentLocation = null;
            this.PlannedTasks = null;
            this.CurrentlyPerformedTask = null;
            Universe.DeadPeople.Add(this);
        }
        #endregion
    }
}
