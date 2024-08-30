using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class ComputerEmptyBox : BasicSprite
    {
        public float Thickness;
        public ComputerEmptyBox(Vector2 pos, Vector2 dims) : base("Content/graphics/Interface/Notebook/Interface_Element_To_Stretch.png", pos, dims)
        {
            Thickness = 2;
        }
        public override void Draw(Microsoft.Xna.Framework.Vector2 offset, float zoom)
        {
            if (!IsHidden)
                if (Tex != null)
                {
                   Globals.SpriteBatch.Draw(Tex, new Rectangle((int)((Pos.X + offset.X) * zoom), (int)((Pos.Y + offset.Y) * zoom), (int)(Dims.X * zoom), (int)(Thickness * zoom)), null, DisplayColor,
                        Rot, new Vector2(0, 0), SpriteEffects, 0);
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)((Pos.X + offset.X) * zoom), (int)((Pos.Y + Dims.Y + offset.Y - Thickness) * zoom), (int)(Dims.X * zoom), (int)(Thickness * zoom)), null, DisplayColor,
                        Rot, new Vector2(0, 0), SpriteEffects, 0);
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)((Pos.X + offset.X) * zoom), (int)((Pos.Y + offset.Y) * zoom), (int)(Thickness * zoom), (int)(Dims.Y * zoom)), null, DisplayColor,
                        Rot, new Vector2(0, 0), SpriteEffects, 0);
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)((Pos.X + Dims.X + offset.X - Thickness) * zoom), (int)((Pos.Y + offset.Y) * zoom), (int)(Thickness * zoom), (int)(Dims.Y * zoom)), null, DisplayColor,
                        Rot, new Vector2(0, 0), SpriteEffects, 0);
                }
        }
    }
}
