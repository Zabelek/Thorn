using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class BasicCamera
    {
        #region Variables
        public float Zoom { get; set; }
        public Vector2 Position;
        public Rectangle Bounds { get; protected set; }
        public Rectangle VisibleArea { get; protected set; }
        public Matrix Transform { get; protected set; }
        public bool AllowScroll, AllowMovement;
        //Closer it gets to 0.86, slower it becomes.
        public float Speed;
        #endregion

        #region Constructors
        public BasicCamera(Viewport viewport)
        {
            Bounds = viewport.Bounds;
            Zoom = 1f;
            AllowScroll = true;
            AllowMovement = true;
            Speed = 0.87f;
        }
        #endregion
        #region Methods
        private void UpdateVisibleArea()
        {
            var inverseViewMatrix = Matrix.Invert(Transform);

            var tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
            var tr = Vector2.Transform(new Vector2(Bounds.X, 0), inverseViewMatrix);
            var bl = Vector2.Transform(new Vector2(0, Bounds.Y), inverseViewMatrix);
            var br = Vector2.Transform(new Vector2(Bounds.Width, Bounds.Height), inverseViewMatrix);

            var min = new Vector2(
                MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
                MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
            var max = new Vector2(
                MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
                MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));
            VisibleArea = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        private void UpdateMatrix()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                    Matrix.CreateScale(Zoom) *
                    Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
            UpdateVisibleArea();
        }

        public void MoveCamera(Vector2 movePosition)
        {
            if(AllowMovement)
            {
                Position.X += movePosition.X;
                Position.Y += movePosition.Y;
            }
        }

        public void ChangeZoon()
        {
            if(AllowScroll && AllowMovement)
            {
                if (Globals.Mouse.GetMouseWheelChange() != 0)
                {
                    if (Globals.Mouse.GetMouseWheelChange() < 0)
                    {
                        var pomWidth = (float)(Globals.ScreenWidth / Zoom);
                        var pomHeight = (float)(Globals.ScreenHeight / Zoom);
                        Zoom /= (float)((float)Globals.Mouse.GetMouseWheelChange() * 0.01f*Speed) * -1;
                        var pomWidth2 = (float)(Globals.ScreenWidth / Zoom);
                        var pomHeight2 = (float)(Globals.ScreenHeight / Zoom);

                        Position.X -= (pomWidth - pomWidth2) * (Globals.Mouse.NewMousePos.X / Globals.ScreenWidth);
                        Position.Y -= (pomHeight - pomHeight2) * (Globals.Mouse.NewMousePos.Y / Globals.ScreenHeight);
                    }
                    else
                    {
                        var pomWidth = (float)(Globals.ScreenWidth / Zoom);
                        var pomHeight = (float)(Globals.ScreenHeight / Zoom);
                        Zoom *= (float)((float)Globals.Mouse.GetMouseWheelChange() * 0.01f * Speed);
                        var pomWidth2 = (float)(Globals.ScreenWidth / Zoom);
                        var pomHeight2 = (float)(Globals.ScreenHeight / Zoom);

                        Position.X -= (pomWidth - pomWidth2) * (Globals.Mouse.NewMousePos.X / Globals.ScreenWidth);
                        Position.Y -= (pomHeight - pomHeight2) * (Globals.Mouse.NewMousePos.Y / Globals.ScreenHeight);
                    }
                    //Place Camera in image bounds
                    if (Zoom < 1)
                        Zoom = 1;
                    if(1920/ Zoom - 1920> Position.X)
                    {
                        Position.X +=((1920 / Zoom) - 1920) - Position.X;
                    }
                    if (Position.X>0)
                    {
                        Position.X = 0;
                    }
                    if (1080 / Zoom - 1080 > Position.Y)
                    {
                        Position.Y += ((1080/ Zoom)-1080) - Position.Y;
                    }
                    if(Position.Y>0)
                    {
                        Position.Y = 0;
                    }
                }
            }
        }
        public void UpdateCamera(Viewport bounds)
        {
            Bounds = bounds.Bounds;
            UpdateMatrix();
            ChangeZoon();
        }
        public void UpdateCamera()
        {
            UpdateCamera(Globals.GraphicsDeviceManager.GraphicsDevice.Viewport);
        }
        #endregion
    }
}
