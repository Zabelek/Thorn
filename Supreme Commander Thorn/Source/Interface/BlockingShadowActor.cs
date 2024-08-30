using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn.Source.Interface
{
    internal class BlockingShadowActor : Actor
    {
        public bool CustonOpacityMask;
        public BlockingShadowActor() 
        {
            FileStream fileStream = new FileStream("Content/graphics/Interface/Popup_Window_Shadow.png", FileMode.Open);
            Tex = Texture2D.FromStream(Globals.GraphicsDeviceManager.GraphicsDevice, fileStream);
            fileStream.Dispose();
            Pos = new Vector2(0, 0);
            Dims = new Vector2(1920, 1080);
            IsHidden = false;
            DisplayColor = Globals.BlockingShadowColor;
            CustonOpacityMask = false;
        }
        public override void UpdateForCollision(Vector2 offset, float zoom)
        {
            if (!this.IsHidden && !this.IsOverlaid)
            {
                IsHovered = true;
                if (Globals.Mouse.LeftClick())
                {
                    IsPressed = true;
                }
                if (Globals.Mouse.LeftClickRelease())
                {
                    IsPressed = false;
                }
            }
            else
            {
                IsPressed = false;
                IsHovered = false;
            }
        }
        public override void Draw(Vector2 offset, float zoom)
        {
            if(CustonOpacityMask)
                Globals.SpriteBatch.Draw(Tex, new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)(Dims.X), (int)(Dims.Y)), null, DisplayColor, Rot, new Vector2(0, 0), SpriteEffects, 0);
            else
                Globals.SpriteBatch.Draw(Tex, new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)(Dims.X), (int)(Dims.Y)), null, Globals.BlockingShadowColor, Rot, new Vector2(0, 0), SpriteEffects, 0);
        }
    }
}
