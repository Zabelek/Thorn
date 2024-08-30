using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class MainCharacter : Person
    {
        #region Variables
        //base is 1000. It decreases in hard enviroment conditions.
        public int CurrentEndurance, MaxEndurance;
        #endregion

        #region Constructors
        public MainCharacter() : base() 
        {
            CurrentEndurance = 1000;
            MaxEndurance = 1000;
            Inventory = new Inventory();
        }
        #endregion
        #region Methods
        #endregion
    }
}
