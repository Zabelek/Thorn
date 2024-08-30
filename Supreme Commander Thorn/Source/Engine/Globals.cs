using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public delegate void PassObject(object i);
    public delegate object PassObjectAndReturn(object i);

    public class Globals
    {
        #region Variables
        public static int ScreenHeight, ScreenWidth;

        public static ContentManager ContentManager;
        public static SpriteBatch SpriteBatch;
        public static GraphicsDeviceManager GraphicsDeviceManager;
        public static BasicKeyboard Keyboard = new();
        public static BasicMouseControl Mouse = new();
        public static GameTime GameTime;

        //colors and fonts
        public static Color DefauldInterfaceColor = new Color(210, 210, 210);
        public static Color GrayedOutInterfaceColor = new Color(160, 160, 160);
        public static Color NotebookInterfaceColor = new Color(252, 255, 63);
        public static Color NotebookInterfaceColorHover = new Color(212, 215, 23);
        public static Color NotebookInterfaceColorClicked = new Color(172, 175, 0);
        public static Color BlockingShadowColor = new Color(255, 255, 255, 100);
        public static SpriteFont DefaultInterfaceFont, SmallerInterfaceFont, BiggerInterfaceFont;
        #endregion

        #region Init Methods
        public static void SetUpFonts()
        {
            DefaultInterfaceFont = ContentManager.Load<SpriteFont>("fonts\\Default_Interface_Font");
            SmallerInterfaceFont = ContentManager.Load<SpriteFont>("fonts\\Smaller_Interface_Font");
            BiggerInterfaceFont = ContentManager.Load<SpriteFont>("fonts\\Bigger_Interface_Font");
        }
        #endregion

        #region Methods
        public static float GetDistance(Vector2 pos, Vector2 target)
        {
            return (float)(Math.Sqrt(Math.Pow(pos.X - target.X, 2)) + Math.Sqrt(Math.Pow(pos.Y - target.Y, 2)));
        }
        #endregion
    }
}
