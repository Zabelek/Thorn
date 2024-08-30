using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn.Source
{
    public class SwitchButton : BasicButton
    {
        private float _thickness;
        public String Text;
        public bool toggle;
        public SwitchButton(string text, Vector2 pos, PassObject buttonClick, object info) : base("Content/graphics/Interface/Notebook/Interface_Element_To_Stretch.png", pos, buttonClick, info)
        {
            Text = text;
            toggle = false;
            _thickness = 2;
            float width = Globals.SmallerInterfaceFont.MeasureString(text).X + 20;
            this.Dims = new Vector2(width, 25);
        }

        public override void Draw(Vector2 offset, float zoom)
        {
            if (!IsHidden)
                if (Tex != null)
                {
                    if(toggle)
                    {
                        Globals.SpriteBatch.Draw(Tex, new Rectangle((int)((Pos.X + offset.X) * zoom), (int)((Pos.Y + offset.Y) * zoom),
                        (int)(Dims.X * zoom), (int)(Dims.Y * zoom)), null, ClickedColor, Rot, new Vector2(0, 0), SpriteEffects, 0);
                    }
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)((Pos.X + offset.X) * zoom), (int)((Pos.Y + offset.Y) * zoom), (int)(Dims.X * zoom), (int)(_thickness * zoom)), null, DisplayColor,
                         Rot, new Vector2(0, 0), SpriteEffects, 0);
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)((Pos.X + offset.X) * zoom), (int)((Pos.Y + Dims.Y + offset.Y - _thickness) * zoom), (int)(Dims.X * zoom), (int)(_thickness * zoom)), null, DisplayColor,
                        Rot, new Vector2(0, 0), SpriteEffects, 0);
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)((Pos.X + offset.X) * zoom), (int)((Pos.Y + offset.Y) * zoom), (int)(_thickness * zoom), (int)(Dims.Y * zoom)), null, DisplayColor,
                        Rot, new Vector2(0, 0), SpriteEffects, 0);
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)((Pos.X + Dims.X + offset.X - _thickness) * zoom), (int)((Pos.Y + offset.Y) * zoom), (int)(_thickness * zoom), (int)(Dims.Y * zoom)), null, DisplayColor,
                        Rot, new Vector2(0, 0), SpriteEffects, 0);
                    Globals.SpriteBatch.DrawString(Globals.SmallerInterfaceFont, Text, (this.Pos + new Vector2(10, 5) + offset) * zoom, Globals.NotebookInterfaceColor);
                }
        }
    }
}
