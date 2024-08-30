using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Supreme_Commander_Thorn
{
    public class InterfaceButton : BasicButton
    {
        #region Variables
        private Texture2D _leftSide, _rightSide;
        private String _content;
        private Vector2 _leftSideDims, _rightSideDims, _centerDims;
        private BasicTextSprite _textSprite;
        public bool IsFixedSize;
        public float DimsX { get; protected set; }
        #endregion

        #region Constructors
        public InterfaceButton(String content, Vector2 pos, PassObject buttonClick, object info) : base("Content/graphics/Interface/Button_Center.png", pos, new Vector2(0,0), buttonClick, info)
        {
            FileStream fileStream1 = new FileStream("Content/graphics/Interface/Button_Left.png", FileMode.Open);
            _leftSide = Texture2D.FromStream(Globals.GraphicsDeviceManager.GraphicsDevice, fileStream1);
            fileStream1.Dispose();
            FileStream fileStream2 = new FileStream("Content/graphics/Interface/Button_Right.png", FileMode.Open);
            _rightSide = Texture2D.FromStream(Globals.GraphicsDeviceManager.GraphicsDevice, fileStream2);
            fileStream2.Dispose();
            this._content = content;
            _textSprite = new BasicTextSprite(content, new Vector2(0, 0), Globals.DefauldInterfaceColor, Globals.DefaultInterfaceFont);
            _textSprite.Pos = pos;
            IsFixedSize = false;
            RecalculateComponentDims();
            this.DisplayColor = new Color(220, 220, 220);
            this.ClickedColor = new Color(140, 140, 140);
            this.HoverColor = new Color(180, 180, 180);
        }
        public InterfaceButton(String content, Vector2 pos, float dimsX, PassObject buttonClick, object info) : this(content, pos, buttonClick, info)
        {
            SetDimsX(dimsX);
        }
        #endregion

        #region Methods
        public void SetDimsX(float gdims)
        {
            this.IsFixedSize = true;
            this.DimsX = gdims;
            RecalculateComponentDims();
        }
        private void RecalculateComponentDims()
        {
            if (IsFixedSize)
            {
                float scaleMod = 1.7f;
                float temp = _textSprite.Dims.Y * scaleMod * 160 / 290;
                _leftSideDims = new Vector2(temp, _textSprite.Dims.Y * scaleMod);
                _rightSideDims = new Vector2(temp, _textSprite.Dims.Y * scaleMod);
                _centerDims = new Vector2(DimsX * 1.1f, _textSprite.Dims.Y * scaleMod);
                this.Dims = new Vector2((_leftSideDims.X + _centerDims.X + _rightSideDims.X), _centerDims.Y);
            }
            else
            {
                float scaleMod = 1.7f;
                float temp = _textSprite.Dims.Y * scaleMod * 160 / 290;
                _leftSideDims = new Vector2(temp, _textSprite.Dims.Y * scaleMod);
                _rightSideDims = new Vector2(temp, _textSprite.Dims.Y * scaleMod);
                _centerDims = new Vector2(_textSprite.Dims.X * 1.1f, _textSprite.Dims.Y * scaleMod);
                this.Dims = new Vector2((_leftSideDims.X + _centerDims.X + _rightSideDims.X), _centerDims.Y);
            }
        }
        #endregion

        #region Draws
        public override void Draw(Vector2 offset)
        {
            if (!this.IsHidden && Tex != null)
            {
                if (IsPressed)
                {
                    Globals.SpriteBatch.Draw(_leftSide, new Rectangle((int)(Pos.X + offset.X), (int)(Pos.Y + offset.Y), (int)(_leftSideDims.X), (int)(_leftSideDims.Y)), null, ClickedColor,
                        Rot, new Vector2(0, 0), SpriteEffects, 0);
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)(Pos.X + offset.X + _leftSideDims.X), (int)(Pos.Y + offset.Y), (int)(_centerDims.X), (int)(_centerDims.Y)), null, ClickedColor,
                        Rot, new Vector2(0, 0), SpriteEffects, 0);
                    Globals.SpriteBatch.Draw(_rightSide, new Rectangle((int)(Pos.X + offset.X + _leftSideDims.X + _centerDims.X), (int)(Pos.Y + offset.Y), (int)(_rightSideDims.X), (int)(_rightSideDims.Y)), null, ClickedColor,
                        Rot, new Vector2(0, 0), SpriteEffects, 0);
                }
                else if (IsHovered)
                {
                    Globals.SpriteBatch.Draw(_leftSide, new Rectangle((int)(Pos.X + offset.X), (int)(Pos.Y + offset.Y), (int)(_leftSideDims.X), (int)(_leftSideDims.Y)), null, HoverColor,
                           Rot, new Vector2(0, 0), SpriteEffects, 0);
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)(Pos.X + offset.X + _leftSideDims.X), (int)(Pos.Y + offset.Y), (int)(_centerDims.X), (int)(_centerDims.Y)), null, HoverColor,
                        Rot, new Vector2(0, 0), SpriteEffects, 0);
                    Globals.SpriteBatch.Draw(_rightSide, new Rectangle((int)(Pos.X + offset.X + _leftSideDims.X + _centerDims.X), (int)(Pos.Y + offset.Y), (int)(_rightSideDims.X), (int)(_rightSideDims.Y)), null, HoverColor,
                        Rot, new Vector2(0, 0), SpriteEffects, 0);
                }
                else
                {
                    Globals.SpriteBatch.Draw(_leftSide, new Rectangle((int)(Pos.X + offset.X), (int)(Pos.Y + offset.Y), (int)(_leftSideDims.X), (int)(_leftSideDims.Y)), null, DisplayColor,
                        Rot, new Vector2(0, 0), SpriteEffects, 0);
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)(Pos.X + offset.X + _leftSideDims.X), (int)(Pos.Y + offset.Y), (int)(_centerDims.X), (int)(_centerDims.Y)), null, DisplayColor,
                        Rot, new Vector2(0, 0), SpriteEffects, 0);
                    Globals.SpriteBatch.Draw(_rightSide, new Rectangle((int)(Pos.X + offset.X + _leftSideDims.X + _centerDims.X), (int)(Pos.Y + offset.Y), (int)(_rightSideDims.X), (int)(_rightSideDims.Y)), null, DisplayColor,
                        Rot, new Vector2(0, 0), SpriteEffects, 0);
                }
                _textSprite.Draw(new Vector2(offset.X + _leftSideDims.X + _textSprite.Dims.X / 18, offset.Y + _textSprite.Dims.Y / 3));
            }
        }
        public override void Draw(Vector2 offset, float zoom)
        {
            if (!this.IsHidden && Tex != null)
            {
                if (IsPressed)
                {
                    Globals.SpriteBatch.Draw(_leftSide, new Rectangle((int)((Pos.X + offset.X) * zoom), (int)((Pos.Y + offset.Y) * zoom),
                        (int)(_leftSideDims.X * zoom), (int)(_leftSideDims.Y * zoom)), null, ClickedColor, Rot, new Vector2(0, 0), SpriteEffects, 0);
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)((Pos.X + offset.X + _leftSideDims.X) * zoom), (int)((Pos.Y + offset.Y) * zoom),
                        (int)(_centerDims.X * zoom), (int)(_centerDims.Y * zoom)), null, ClickedColor, Rot, new Vector2(0, 0), SpriteEffects, 0);
                    Globals.SpriteBatch.Draw(_rightSide, new Rectangle((int)((Pos.X + offset.X + _leftSideDims.X + _centerDims.X) * zoom), (int)((Pos.Y + offset.Y) * zoom),
                        (int)(_rightSideDims.X * zoom), (int)(_rightSideDims.Y * zoom)), null, ClickedColor, Rot, new Vector2(0, 0), SpriteEffects, 0);
                    _textSprite.Draw(new Vector2(offset.X + _leftSideDims.X + _textSprite.Dims.X / 18, offset.Y + _textSprite.Dims.Y / 3), zoom, ClickedColor);
                }
                else if (IsHovered)
                {
                    Globals.SpriteBatch.Draw(_leftSide, new Rectangle((int)((Pos.X + offset.X) * zoom), (int)((Pos.Y + offset.Y) * zoom),
                        (int)(_leftSideDims.X * zoom), (int)(_leftSideDims.Y * zoom)), null, HoverColor, Rot, new Vector2(0, 0), SpriteEffects, 0);
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)((Pos.X + offset.X + _leftSideDims.X) * zoom), (int)((Pos.Y + offset.Y) * zoom),
                        (int)(_centerDims.X * zoom), (int)(_centerDims.Y * zoom)), null, HoverColor, Rot, new Vector2(0, 0), SpriteEffects, 0);
                    Globals.SpriteBatch.Draw(_rightSide, new Rectangle((int)((Pos.X + offset.X + _leftSideDims.X + _centerDims.X) * zoom), (int)((Pos.Y + offset.Y) * zoom),
                        (int)(_rightSideDims.X * zoom), (int)(_rightSideDims.Y * zoom)), null, HoverColor, Rot, new Vector2(0, 0), SpriteEffects, 0);
                    _textSprite.Draw(new Vector2(offset.X + _leftSideDims.X + _textSprite.Dims.X / 18, offset.Y + _textSprite.Dims.Y / 3), zoom, HoverColor);
                }
                else
                {
                    Globals.SpriteBatch.Draw(_leftSide, new Rectangle((int)((Pos.X + offset.X) * zoom), (int)((Pos.Y + offset.Y) * zoom),
                        (int)(_leftSideDims.X * zoom), (int)(_leftSideDims.Y * zoom)), null, DisplayColor, Rot, new Vector2(0, 0), SpriteEffects, 0);
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)((Pos.X + offset.X + _leftSideDims.X) * zoom), (int)((Pos.Y + offset.Y) * zoom),
                        (int)(_centerDims.X * zoom), (int)(_centerDims.Y * zoom)), null, DisplayColor, Rot, new Vector2(0, 0), SpriteEffects, 0);
                    Globals.SpriteBatch.Draw(_rightSide, new Rectangle((int)((Pos.X + offset.X + _leftSideDims.X + _centerDims.X) * zoom), (int)((Pos.Y + offset.Y) * zoom),
                        (int)(_rightSideDims.X * zoom), (int)(_rightSideDims.Y * zoom)), null, DisplayColor, Rot, new Vector2(0, 0), SpriteEffects, 0);
                    _textSprite.Draw(new Vector2(offset.X + _leftSideDims.X + _textSprite.Dims.X / 18, offset.Y + _textSprite.Dims.Y / 3), zoom, DisplayColor);
                }

            }
            #endregion
        }
    }
}
