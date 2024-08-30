using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class Universe
    {
        #region Variables
        //The timer indicating true ingame time, scaled down ten times
        public static BasicTimer Timer = new BasicTimer(1000);
        public static List<Galaxy> Galaxies = new();
        public static List<Person> RegisteredPeople = new();
        public static List<Person> DeadPeople = new();
        public static List<UsableObject> RegisteredUsableObjects = new();
        public static List<Item> RegisteredItems = new();
        public static DateTime GameDate;
        public static MainCharacter MainCharacter;
        public static bool DayNightShift;
        #endregion

        #region Methods
        public static void Update()
        {
            if (Timer.Test())
            {
                var hour = GameDate.Hour;
                GameDate = GameDate.AddSeconds(10);
                if ((hour == 6 && GameDate.Hour == 7) || ( hour == 21 && GameDate.Hour == 22))
                    DayNightShift = true;
                else
                    DayNightShift = false;
                foreach (UsableObject usable in RegisteredUsableObjects)
                    usable.Update();
                Timer.Reset();
            }
            foreach (Item item in RegisteredItems)
                item.Update();
            Timer.UpdateTimer();
        }
        public static void AddPerson(Person person)
        {
            RegisteredPeople.Add(person);
        }
        public static int GiveNewPersonId()
        {
            return RegisteredPeople.Count;
        }
        public static bool IsItDay() {
            return (GameDate.Hour > 6 && GameDate.Hour < 22);
        }
        public static void RegisterItem(Item item)
        {
            bool contains = false;
            foreach (Item regItem in RegisteredItems)
            {
                if (regItem.ItemID == item.ItemID)
                {
                    contains = true;
                    item.Name = regItem.Name;
                    item.Description = regItem.Description;
                    item.ImagePath = regItem.ImagePath;
                    break;
                }
            }
            if (!contains)
            {
                RegisteredItems.Add(item);
            }
        }
        #endregion
    }
}
