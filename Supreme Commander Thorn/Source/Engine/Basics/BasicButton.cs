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
    public class BasicButton : Actor
    {
        #region Variables
        private object _info;

        public Color HoverColor, ClickedColor;
        public List<PassObject> ButtonClicks = new();
        #endregion

        #region Constructors
        public BasicButton(string path, Vector2 pos, Vector2 dims, PassObject buttonClick, object info) : base(path, pos, dims)
        {
            HoverColor = Color.LightGray;
            ClickedColor = Color.Gray;
            _info = info;
            ButtonClicks.Add(buttonClick);
            IsPressed = false;
            IsHovered = false;
        }
        public BasicButton(string path, Vector2 pos, PassObject buttonClick, object info) : base(path, pos)
        {
            HoverColor = Color.LightGray;
            ClickedColor = Color.Gray;
            _info = info;
            ButtonClicks.Add(buttonClick);
            IsPressed = false;
            IsHovered = false;
        }
        #endregion

        #region Methods
        public override void UpdateForCollision(Vector2 offset, float zoom)
        {
            if(Hover(offset, zoom) && !IsOverlaid)
            {
                IsHovered = true;
                if(Globals.Mouse.LeftClick())
                {
                    IsPressed = true;
                }
                if(Globals.Mouse.LeftClickRelease()) {
                    RunClick();
                    IsPressed = false;
                }
            }
            else
            {
                IsPressed = false;
                IsHovered = false;
            }
        }

        protected virtual void RunClick()
        {
            foreach(PassObject function in ButtonClicks)
            {
                if(function!= null)
                    function(_info);
            }
        }
        #endregion

        #region Draws
        public override void Draw(Vector2 offset)
        {
            if (!IsHidden && Tex != null)
            {
                if (IsPressed)
                {
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)(Pos.X + offset.X), (int)(Pos.Y + offset.Y), (int)(Dims.X), (int)(Dims.Y)), null, ClickedColor,
                        Rot, new Vector2(0, 0), SpriteEffects, 0);
                }
                else if (IsHovered)
                {
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)(Pos.X + offset.X), (int)(Pos.Y + offset.Y), (int)(Dims.X), (int)(Dims.Y)), null, HoverColor,
                        Rot, new Vector2(0, 0), SpriteEffects, 0);
                }
                else
                {
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)(Pos.X + offset.X), (int)(Pos.Y + offset.Y), (int)(Dims.X), (int)(Dims.Y)), null, DisplayColor,
                        Rot, new Vector2(0, 0), SpriteEffects, 0);
                }
            }
        }
        public override void Draw(Vector2 offset, float zoom)
        {
            if (!IsHidden && Tex != null)
            {
                if (IsPressed)
                {
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)((Pos.X + offset.X) * zoom), (int)((Pos.Y + offset.Y) * zoom),
                        (int)(Dims.X * zoom), (int)(Dims.Y * zoom)), null, ClickedColor, Rot, new Vector2(0, 0), SpriteEffects, 0);
                }
                else if (IsHovered)
                {
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)((Pos.X + offset.X) * zoom), (int)((Pos.Y + offset.Y) * zoom),
                        (int)(Dims.X * zoom), (int)(Dims.Y * zoom)), null, HoverColor, Rot, new Vector2(0, 0), SpriteEffects, 0);
                }
                else
                {
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)((Pos.X + offset.X) * zoom), (int)((Pos.Y + offset.Y) * zoom),
                        (int)(Dims.X * zoom), (int)(Dims.Y * zoom)), null, DisplayColor, Rot, new Vector2(0, 0), SpriteEffects, 0);
                }
            }
        }
        #endregion
    }
}
