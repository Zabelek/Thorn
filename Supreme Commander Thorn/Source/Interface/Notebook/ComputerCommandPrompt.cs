using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn.Source
{
    public class ComputerCommandPrompt : BasicTextInput
    {
        public bool EnterClicked = false;
        public ComputerCommandPrompt(Vector2 pos) : base(pos, new Vector2(1000, 30))
        {

        }
        public override void InsertChar(char ch)
        {
            if (ch == '\n')
                EnterClicked = true;
            else
                base.InsertChar(ch);
        }
    }
}
