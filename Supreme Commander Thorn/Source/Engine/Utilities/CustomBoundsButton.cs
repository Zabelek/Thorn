using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class CustomBoundsButton : BasicButton
    {
        #region Variables
        private Color[,] _testArray;
        #endregion

        #region Constructors
        public CustomBoundsButton(string path, Vector2 pos, Vector2 dims, PassObject buttonClick, object info) : base(path, pos, dims, buttonClick, info)
        {
            _testArray = TextureTo2DArray(this.Tex);
        }
        #endregion

        #region Methods
        public override bool Hover(Vector2 offset, float zoom)
        {
            if (base.Hover(offset, zoom))
            {
                try
                {
                    int pixelsX = (int)((Globals.Mouse.NewMousePos.X - (Pos.X + offset.X) * zoom) / zoom);
                    int pixelsY = (int)((Globals.Mouse.NewMousePos.Y - (Pos.Y + offset.Y) * zoom) / zoom);
                    if (_testArray[pixelsX, pixelsY].A > 0.9)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }
        private Color[,] TextureTo2DArray(Texture2D texture)
        {
            Color[] colors1D = new Color[texture.Width * texture.Height];
            texture.GetData(colors1D);
            Color[,] colors2D = new Color[texture.Width, texture.Height];
            for (int x = 0; x < texture.Width; x++)
            {
                for (int y = 0; y < texture.Height; y++)
                {
                    colors2D[x, y] = colors1D[x + y * texture.Width];
                }
            }
            return colors2D;
        }
        #endregion
    }
}
