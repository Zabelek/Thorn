﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class BasicKey
    {
        #region Variables
        public int State;
        public String Key, Print, Display;
        #endregion

        #region Constructors
        public BasicKey(int state, String key)
        {
            this.State = state;
            this.Key = key;
            MakePrint(key);
        }
        #endregion

        #region Methods
        public virtual void Update()
        {
            State = 2;
        }
        private void MakePrint(string key)
        {
            Display = key;

            string tempStr = "";

            if (key == "A" || key == "B" || key == "C" || key == "D" || key == "E" || key == "F" || key == "G" || key == "H" || key == "I" || key == "J" || key == "K" || key == "L" || key == "M" || key == "N" || key == "O" || key == "P" || key == "Q" || key == "R" || key == "S" || key == "T" || key == "U" || key == "V" || key == "W" || key == "X" || key == "Y" || key == "Z")
            {
                tempStr = key;
            }
            if (key == "Space")
            {
                tempStr = " ";
            }
            if (key == "OemCloseBrackets")
            {
                tempStr = "]";
                Display = tempStr;
            }
            if (key == "OemOpenBrackets")
            {
                tempStr = "[";
                Display = tempStr;
            }
            if (key == "OemMinus")
            {
                tempStr = "-";
                Display = tempStr;
            }
            if (key == "OemPeriod" || key == "Decimal")
            {
                tempStr = ".";
            }
            if (key == "D1" || key == "D2" || key == "D3" || key == "D4" || key == "D5" || key == "D6" || key == "D7" || key == "D8" || key == "D9" || key == "D0")
            {
                tempStr = key.Substring(1);
            }
            else if (key == "NumPad1" || key == "NumPad2" || key == "NumPad3" || key == "NumPad4" || key == "NumPad5" || key == "NumPad6" || key == "NumPad7" || key == "NumPad8" || key == "NumPad9" || key == "NumPad0")
            {
                tempStr = key.Substring(6);
            }
            Print = tempStr;
        }
        #endregion
    }
}
