using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Supreme_Commander_Thorn
{
    public class BasicTimer
    {
        #region Variables
        protected TimeSpan Timer = new TimeSpan();
        public int MSec;
        public bool GoodToGo;
        #endregion

        #region Constructors
        public BasicTimer(int m)
        {
            GoodToGo = false;
            MSec = m;
        }
        public BasicTimer(int m, bool startLoaded)
        {
            GoodToGo = startLoaded;
            MSec = m;
        }
        #endregion

        #region Methods
        public void UpdateTimer()
        {
            Timer += Globals.GameTime.ElapsedGameTime;
        }

        public void UpdateTimer(float speed)
        {
            Timer += TimeSpan.FromTicks((long)(Globals.GameTime.ElapsedGameTime.Ticks * speed));
        }

        public virtual void AddToTimer(int inMSec)
        {
            Timer += TimeSpan.FromMilliseconds((long)(inMSec));
        }

        public bool Test()
        {
            if (Timer.TotalMilliseconds >= MSec || GoodToGo)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            Timer = Timer.Subtract(new TimeSpan(0, 0, MSec / 60000, MSec / 1000, MSec % 1000));
            if (Timer.TotalMilliseconds < 0)
            {
                Timer = TimeSpan.Zero;
            }
            GoodToGo = false;
        }

        public void Reset(int newTimer)
        {
            Timer = TimeSpan.Zero;
            MSec = newTimer;
            GoodToGo = false;
        }

        public void ResetToZero()
        {
            Timer = TimeSpan.Zero;
            GoodToGo = false;
        }

        public virtual XElement ReturnXML()
        {
            XElement xml = new XElement("Timer",
                                    new XElement("mSec", MSec),
                                    new XElement("timer", Timer));



            return xml;
        }

        public void SetTimer(TimeSpan TIME)
        {
            Timer = TIME;
        }

        public virtual void SetTimer(int MSEC)
        {
            Timer = TimeSpan.FromMilliseconds((long)(MSEC));
        }
        #endregion
    }
}
