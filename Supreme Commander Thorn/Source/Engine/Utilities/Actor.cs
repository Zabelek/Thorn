using Microsoft.Xna.Framework;
using SharpDX.MediaFoundation;
using Supreme_Commander_Thorn.Source.Engine.Basics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class Actor : BasicSprite
    {
        #region Variables
        protected bool IsPressed, IsHovered;
        public ISpriteParent Parent;
        public bool IsOverlaid;
        #endregion

        #region Constructors
        public Actor(string path, Vector2 pos, Vector2 dims) : base(path, pos, dims)
        {
            IsOverlaid = false;
            IsPressed = false;
            IsHovered = false;
        }
        public Actor(string path, Vector2 pos) : base(path, pos)
        {
            IsOverlaid = false;
            IsPressed = false;
            IsHovered = false;
        }
        public Actor() : this(null, Vector2.Zero, Vector2.Zero){ }
        #endregion

        #region Methods
        public virtual void UpdateForCollision(Vector2 offset, float zoom)
        {
            if (this.Hover(offset, zoom) && !this.IsOverlaid)
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
        public void UpdateForCollision(BasicCamera camera)
        {
            UpdateForCollision(camera.Position, camera.Zoom);
        }
        public override void Update(BasicCamera camera)
        {
            UpdateForCollision(camera);
        }
        public override void Update(Vector2 offset, float zoom)
        {
            UpdateForCollision(offset, zoom);
        }
        public virtual void Reset()
        {
            IsPressed = false;
            IsHovered = false;
        }
        #endregion
    }
}
