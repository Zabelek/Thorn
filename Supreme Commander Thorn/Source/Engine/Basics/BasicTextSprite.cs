using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supreme_Commander_Thorn
{
    public class BasicTextSprite : BasicSprite
    {
        #region Variables
        protected SpriteFont _font;
        public String Description { get; protected set; }
        public Color Color;
        #endregion

        #region Constructors
        public BasicTextSprite(string description, Vector2 pos, Color color, SpriteFont font) : base(null, pos)
        {
            this._font = font;
            this.Description = description;
            this.Color = color;
            this.Dims = font.MeasureString(description);
        }
        public BasicTextSprite(string description, Vector2 pos, Color color) : base(null, pos)
        {
            _font = Globals.DefaultInterfaceFont;
            this.Description = description;
            this.Color = color;
            this.Dims = _font.MeasureString(description);
        }
        public BasicTextSprite(string description, Vector2 pos, SpriteFont font) : base(null, pos)
        {
            this._font = font;
            this.Description = description;
            Color = Globals.DefauldInterfaceColor;
            this.Dims = font.MeasureString(description);
        }
        public BasicTextSprite(string description, Vector2 pos) : base(null, pos)
        {
            _font = Globals.DefaultInterfaceFont;
            this.Description = description;
            Color = Globals.DefauldInterfaceColor;
            this.Dims = _font.MeasureString(description);
        }
        #endregion

        #region Methods
        public virtual void SetDescription(String text)
        {
            this.Description = text;
            this.Dims = _font.MeasureString(Description);
        }
        #endregion

        #region Draws
        public override void Draw(Vector2 offset, float zoom)
        {
            if(!this.IsHidden && this.Description!=null)
            {
                Vector2 strDims = _font.MeasureString(Description);
                Globals.SpriteBatch.DrawString(_font, Description, new Vector2((Pos.X + offset.X) * zoom, (Pos.Y + offset.Y) * zoom), Color, Rot, new Vector2(0, 0), zoom, SpriteEffects, 0);
            }
        }
        public void Draw(Vector2 offset, float zoom, Color customColor)
        {
            if(!this.IsHidden && this.Description != null) {
                Vector2 strDims = _font.MeasureString(Description);
                Globals.SpriteBatch.DrawString(_font, Description, new Vector2((Pos.X + offset.X) * zoom, (Pos.Y + offset.Y) * zoom), customColor, Rot, new Vector2(0, 0), zoom, SpriteEffects, 0);            }
        }
        #endregion
    }
}
