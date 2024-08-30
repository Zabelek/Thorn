using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn.Source.Engine
{
    public class CursorSprite : BasicSprite
    {
        #region Variables
        public Texture2D TexClicked;
        #endregion

        #region Constructors
        public CursorSprite(String pathNormal, String pathClicked, Vector2 pos, Vector2 dims) : base(pathNormal, pos, dims)
        {
            FileStream fileStream = new FileStream(pathClicked, FileMode.Open);
            TexClicked = Texture2D.FromStream(Globals.GraphicsDeviceManager.GraphicsDevice, fileStream);
            fileStream.Dispose();
        }
        public CursorSprite(String pathNormal, Vector2 pos, Vector2 dims) : base(pathNormal, pos, dims)
        {
            FileStream fileStream = new FileStream(pathNormal, FileMode.Open);
            TexClicked = Texture2D.FromStream(Globals.GraphicsDeviceManager.GraphicsDevice, fileStream);
            fileStream.Dispose();
        }
        #endregion

        #region Draws
        public override void Draw(Vector2 offset)
        {
            if (Tex != null && TexClicked!=null)
            {
                if (Globals.Mouse.New.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    Globals.SpriteBatch.Draw(TexClicked, new Rectangle((int)(Pos.X + offset.X), (int)(Pos.Y + offset.Y), (int)(Dims.X), (int)(Dims.Y)), null, Color.White,
                        0.0f, new Vector2(0, 0), SpriteEffects, 0);
                }
                else
                {
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)(Pos.X + offset.X), (int)(Pos.Y + offset.Y), (int)(Dims.X), (int)(Dims.Y)), null, Color.White,
                        0.0f, new Vector2(0, 0), SpriteEffects, 0);
                }
            }
        }
        #endregion
    }
}
