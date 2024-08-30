using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class BasicMouseControl
    {
        #region Variables
        protected MouseState NewMouse, OldMouse, FirstMouse;
        public bool Dragging, RightDrag;
        public Vector2 NewMousePos, OldMousePos, FirstMousePos, NewMouseAdjustedPos, SystemCursorPos, ScreenLoc;
        #endregion

        #region Constructors
        public BasicMouseControl()
        {
            Dragging = false;

            NewMouse = Mouse.GetState();
            OldMouse = NewMouse;
            FirstMouse = NewMouse;

            NewMousePos = new Vector2(NewMouse.Position.X, NewMouse.Position.Y);
            OldMousePos = new Vector2(NewMouse.Position.X, NewMouse.Position.Y);
            FirstMousePos = new Vector2(NewMouse.Position.X, NewMouse.Position.Y);

            GetMouseAndAdjust();
        }
        #endregion

        #region Properties
        public MouseState First
        {
            get { return FirstMouse; }
        }

        public MouseState New
        {
            get { return NewMouse; }
        }

        public MouseState Old
        {
            get { return OldMouse; }
        }
        #endregion

        #region Methods
        public void Update()
        {
            GetMouseAndAdjust();
            if (NewMouse.LeftButton == ButtonState.Pressed && OldMouse.LeftButton == ButtonState.Released)
            {
                FirstMouse = NewMouse;
                FirstMousePos = NewMousePos = GetScreenPos(FirstMouse);
            }
        }
        public void UpdateOld()
        {
            OldMouse = NewMouse;
            OldMousePos = GetScreenPos(OldMouse);
        }
        public virtual float GetDistanceFromClick()
        {
            return Globals.GetDistance(NewMousePos, FirstMousePos);
        }
        public virtual void GetMouseAndAdjust()
        {
            NewMouse = Mouse.GetState();
            NewMousePos = GetScreenPos(NewMouse);
        }
        public int GetMouseWheelChange()
        {
            return NewMouse.ScrollWheelValue - OldMouse.ScrollWheelValue;
        }
        public Vector2 GetScreenPos(MouseState mouseState)
        {
            Vector2 tempVec = new Vector2(mouseState.Position.X, mouseState.Position.Y);
            return tempVec;
        }
        public virtual bool LeftClick()
        {
            if (NewMouse.LeftButton == ButtonState.Pressed && OldMouse.LeftButton != ButtonState.Pressed && NewMouse.Position.X >= 0 && NewMouse.Position.X <= Globals.ScreenWidth && NewMouse.Position.Y >= 0 && NewMouse.Position.Y <= Globals.ScreenHeight)
            {
                return true;
            }
            return false;
        }
        public virtual bool LeftClickHold()
        {
            bool holding = false;
            if (NewMouse.LeftButton == ButtonState.Pressed && OldMouse.LeftButton == ButtonState.Pressed && NewMouse.Position.X >= 0 && NewMouse.Position.X <= Globals.ScreenWidth && NewMouse.Position.Y >= 0 && NewMouse.Position.Y <= Globals.ScreenHeight)
            {
                holding = true;

                if (Math.Abs(NewMouse.Position.X - FirstMouse.Position.X) > 8 || Math.Abs(NewMouse.Position.Y - FirstMouse.Position.Y) > 8)
                {
                    Dragging = true;
                }
            }
            return holding;
        }
        public virtual bool LeftClickRelease()
        {
            if (NewMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released && OldMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                Dragging = false;
                return true;
            }
            return false;
        }
        public virtual bool RightClick()
        {
            if (NewMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && OldMouse.RightButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed && NewMouse.Position.X >= 0 && NewMouse.Position.X <= Globals.ScreenWidth && NewMouse.Position.Y >= 0 && NewMouse.Position.Y <= Globals.ScreenHeight)
            {
                return true;
            }
            return false;
        }
        public virtual bool RightClickHold()
        {
            bool holding = false;
            if (NewMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && OldMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && NewMouse.Position.X >= 0 && NewMouse.Position.X <= Globals.ScreenWidth && NewMouse.Position.Y >= 0 && NewMouse.Position.Y <= Globals.ScreenHeight)
            {
                holding = true;

                if (Math.Abs(NewMouse.Position.X - FirstMouse.Position.X) > 8 || Math.Abs(NewMouse.Position.Y - FirstMouse.Position.Y) > 8)
                {
                    RightDrag = true;
                }
            }
            return holding;
        }
        public virtual bool RightClickRelease()
        {
            if (NewMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Released && OldMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                Dragging = false;
                return true;
            }
            return false;
        }
        public void SetFirst(){}
        #endregion
    }
}
