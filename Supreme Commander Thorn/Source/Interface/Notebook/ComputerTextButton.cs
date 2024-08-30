using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class ComputerTextButton : BasicButton
    {
        public String Text;

        public ComputerTextButton(string text, Vector2 pos, PassObject buttonClick, object info) : base("Content/graphics/Interface/Notebook/Interface_Element_To_Stretch.png", pos, buttonClick, info)
        {
            Text = text;
            float width = Globals.SmallerInterfaceFont.MeasureString(text).X + 20;
            this.Dims = new Vector2(width, 25);
        }

        public override void Draw(Vector2 offset, float zoom)
        {
            base.Draw(offset, zoom);
            Globals.SpriteBatch.DrawString(Globals.SmallerInterfaceFont, Text, (this.Pos + new Vector2(10, 5) + offset)*zoom, Color.Black);
        }
    }
}
