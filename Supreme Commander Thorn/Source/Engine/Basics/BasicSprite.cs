using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using Supreme_Commander_Thorn.Source.Engine.Basics;

namespace Supreme_Commander_Thorn
{
    public class BasicSprite
    {
        #region Variables 
        protected SpriteEffects SpriteEffects;
        public Vector2 Pos, Dims;
        public float Rot;
        public Texture2D Tex;
        public bool IsHidden;
        public Color DisplayColor;

        #endregion

        #region Constructors 
        public BasicSprite(String path)
        {
            if(path!=null)
            {
                FileStream fileStream = new FileStream(path, FileMode.Open);
                Tex = Texture2D.FromStream(Globals.GraphicsDeviceManager.GraphicsDevice, fileStream);
                fileStream.Dispose();
                this.Dims = new Vector2(Tex.Bounds.Width, Tex.Bounds.Height);
            }
            this.Pos = new Vector2(0, 0);
            this.Rot = 0;
            this.IsHidden = false;
            DisplayColor = Color.White;
        }
        public BasicSprite(String path, Vector2 pos) : this(path)
        {
            this.Pos = pos;
            this.IsHidden = false;
            DisplayColor = Color.White;
        }
        public BasicSprite(String path, Vector2 pos, Vector2 dims) {
            this.Pos = pos;
            this.Dims = dims;
            if(path!=null)
            {
                FileStream fileStream = new FileStream(path, FileMode.Open);
                Tex = Texture2D.FromStream(Globals.GraphicsDeviceManager.GraphicsDevice, fileStream);
                fileStream.Dispose();
            }
            SpriteEffects = new SpriteEffects();
            Rot = 0;
            this.IsHidden = false;
            DisplayColor = Color.White;
        }
        public BasicSprite(String path, Vector2 pos, Vector2 dims, float rot) : this(path, pos, dims)
        {
            this.Rot = rot;
            this.IsHidden = false;
            DisplayColor = Color.White;
        }
        #endregion

        #region Methods
        public virtual bool Hover(Vector2 offset, float zoom)
        {
            if(!IsHidden)
            {
                Vector2 mousePos = new Vector2(Globals.Mouse.NewMousePos.X, Globals.Mouse.NewMousePos.Y);
                if (mousePos.X >= (Pos.X + offset.X) * zoom && mousePos.X <= (Pos.X + Dims.X + offset.X) * zoom && mousePos.Y >= (Pos.Y + offset.Y) * zoom && mousePos.Y <= (Pos.Y + Dims.Y + offset.Y) * zoom)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public bool Hover(BasicCamera camera)
        {
            return Hover(camera.Position, camera.Zoom);
        }
        public virtual void Update() { }
        public virtual void Update(Vector2 offset) 
        {
            Update();
        }
        public virtual void Update(Vector2 offset, float zoom) 
        {
            Update();
        }
        public virtual void Update(BasicCamera camera)
        {
            Update(camera.Position, camera.Zoom);
        }
        public virtual void Hide()
        {
            IsHidden=true;
        }
        public virtual void Show()
        {
            IsHidden = false;
        }
        public void NewTexture(String path)
        {
            Tex.Dispose();
            if(path == null)
            {
                FileStream fileStream = new FileStream(path, FileMode.Open);
                Tex = Texture2D.FromStream(Globals.GraphicsDeviceManager.GraphicsDevice, fileStream);
                fileStream.Dispose();
            }
        }
        #endregion

        #region Draws
        public virtual void Draw(BasicCamera camera)
        {
            if(!IsHidden)
                Draw(camera.Position, camera.Zoom);
        }
        public virtual void Draw(Vector2 offset)
        {
            if (!IsHidden)
                if (Tex != null)
                {
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)(Pos.X + offset.X), (int)(Pos.Y + offset.Y), (int)(Dims.X), (int)(Dims.Y)), null, DisplayColor,
                        Rot, new Vector2(0, 0), SpriteEffects, 0);
                }
        }
        public virtual void Draw(Vector2 offset, Vector2 origin)
        {
            if (!IsHidden)
                if (Tex != null)
                {
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)(Pos.X + offset.X), (int)(Pos.Y + offset.Y), (int)(Dims.X), (int)(Dims.Y)), null, DisplayColor,
                        Rot, new Vector2(origin.X, origin.Y), SpriteEffects, 0);
                }
        }
        public virtual void Draw(Vector2 offset, float zoom)
        {
            if (!IsHidden)
                if (Tex != null)
                {
                    Globals.SpriteBatch.Draw(Tex, new Rectangle((int)((Pos.X + offset.X) * zoom), (int)((Pos.Y + offset.Y) * zoom), (int)(Dims.X * zoom), (int)(Dims.Y * zoom)), null, DisplayColor,
                        Rot, new Vector2(0, 0), SpriteEffects, 0);
                }
        }
        #endregion
    }
}
