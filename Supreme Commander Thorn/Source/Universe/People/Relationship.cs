using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class Relationship
    {
        #region Variables
        public enum Type { Friend, Enemy, Sibling, Parent, OtherFamilyMember, Spouse, Neutral }
        public Person Person1, Person2;
        public Type RelType;
        //typically from 0 to 200
        public int Person1Trust, Person2Trust, Person1Sympathy, Person2Sympathy;
        #endregion
        public Relationship(Person person1, Person person2)
        {
            this.Person2 = person2;
            this.Person1 = person1;
            this.RelType = Type.Neutral;
            Person1Trust = 30;
            Person2Trust = 30;
            Person1Sympathy = 80;
            Person1Sympathy = 80;
        }
        public void scheduleTask()
        {

        }
    }
}
