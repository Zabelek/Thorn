using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;

namespace Supreme_Commander_Thorn
{
    public class BasicTextInput : Actor
    {
        #region Variables
        private BasicTimer _pressTimer, _pressTimerNewChar;
        protected BasicTimer _animationTimer;
        protected Keys _lastPressedKey;
        protected SpriteFont _font;
        protected int _cursorTextPosition;
        public String CurrentText { get; private set; }
        public Vector2 CursorPosition;
        public BasicSprite Cursor;
        public bool Selected { get; set; }
        #endregion

        #region Constructors
        public BasicTextInput(Vector2 pos, Vector2 dims) : base(null, pos, dims)
        {
            CurrentText = "";
            _cursorTextPosition = 0;
            _font = Globals.SmallerInterfaceFont;
            Cursor = new BasicSprite("Content/graphics/Interface/Notebook/Text_Input_Cursor.png", pos);
            CursorPosition = pos;
            Selected = false;
            _animationTimer = new BasicTimer(1000);
            _pressTimer = new BasicTimer(600);
            _pressTimerNewChar = new BasicTimer(50);
        }
        #endregion

        #region Methods
        public void SetCurrentText(string text)
        {
            this.CurrentText = text;
            this._cursorTextPosition = 0;
            LocateCursor();
        }
        public virtual void InsertChar(char ch)
        {
            bool lowerCase = true;
            if(Globals.Keyboard.NewKeyboardState.CapsLock || Globals.Keyboard.GetPress("LeftShift") || Globals.Keyboard.GetPress("RightShift"))
            {
                lowerCase = false;
                if (ch == ',')
                    ch = '<';
                else if (ch == '.')
                    ch = '>';
                else if (ch == ';')
                    ch = ':';
                else if (ch == '=')
                    ch = '+';
                else if (ch == '-')
                    ch = '_';
                else if (ch == '/')
                    ch = '?';
                else if (ch == '[')
                    ch = '{';
                else if (ch == '\\')
                    ch = '|';
                else if (ch == ']')
                    ch = '}';
                else if (ch == '\'')
                    ch = '"';
                else if (ch == '1')
                    ch = '!';
                else if (ch == '2')
                    ch = '@';
                else if (ch == '3')
                    ch = '#';
                else if (ch == '4')
                    ch = '$';
                else if (ch == '5')
                    ch = '%';
                else if (ch == '6')
                    ch = '^';
                else if (ch == '7')
                    ch = '&';
                else if (ch == '8')
                    ch = '*';
                else if (ch == '9')
                    ch = '(';
                else if (ch == '0')
                    ch = ')';
            }
            if(ch!='\b')
            {
                if(lowerCase)
                    ch = char.ToLower(ch);
                CurrentText = CurrentText.Insert(_cursorTextPosition, ch.ToString());
                _cursorTextPosition++;
            }
            else if(CurrentText.Length > 0 && _cursorTextPosition>0)
            {
                CurrentText = CurrentText.Remove(_cursorTextPosition-1, 1);
                _cursorTextPosition--;
            }
            LocateCursor();
        }
        protected virtual void LocateCursor()
        {
            //reset animation
            Cursor.Show();
            _animationTimer.Reset();
            //locate cursor
            String tempLines = CurrentText.Substring(0, _cursorTextPosition);
            String[] lines = tempLines.Split("\n");
            Vector2 xPos = _font.MeasureString(lines[lines.Length - 1]);
            float yPos = 0;
            if (String.Compare(lines[lines.Length - 1], "") == 0)
            {
                Vector2 yPosPom = _font.MeasureString("abcd");
                yPos = Pos.Y + (lines.Length - 1) * yPosPom.Y;
            }
            else
                yPos = Pos.Y + (lines.Length - 1) * xPos.Y;
            Cursor.Pos = new Vector2(Pos.X + xPos.X, yPos);
        }
        public virtual void MoveCursorUp()
        {
            int tempCursorTextPosition = _cursorTextPosition;
            String tempLines = CurrentText.Substring(0, _cursorTextPosition);
            String[] lines = tempLines.Split("\n");
            if(lines.Length > 1)
            {
                tempCursorTextPosition -= lines[lines.Length - 2].Length;
                if (tempCursorTextPosition > 0 && lines[lines.Length - 1].Length <= lines[lines.Length-2].Length)
                {
                    _cursorTextPosition = tempCursorTextPosition-1;
                }
                else
                {
                    tempCursorTextPosition = _cursorTextPosition;
                    tempCursorTextPosition -= lines[lines.Length - 1].Length +1;
                    _cursorTextPosition = tempCursorTextPosition;
                }
            }
            LocateCursor();
        }
        public virtual void MoveCursorDown()
        {
            int tempCursorTextPosition = _cursorTextPosition;
            String tempLines = CurrentText.Substring(0, _cursorTextPosition);
            String[] lines = tempLines.Split("\n");
            String[] allLines = CurrentText.Split("\n");
            if(lines.Length<allLines.Length)
            {
                tempCursorTextPosition += allLines[lines.Length - 1].Length;
                if(tempCursorTextPosition < CurrentText.Length && allLines[lines.Length].Length >= lines[lines.Length-1].Length)
                {
                    _cursorTextPosition = tempCursorTextPosition + 1;
                }
                else
                {
                    tempCursorTextPosition = _cursorTextPosition;
                    tempCursorTextPosition += allLines[lines.Length - 1].Length - lines[lines.Length - 1].Length + allLines[lines.Length].Length +1;
                    _cursorTextPosition = tempCursorTextPosition;
                }
            }
            LocateCursor();
        }
        public void CheckPressedKey()
        {
            if (_lastPressedKey!=null && Globals.Keyboard.NewKeyboardState.IsKeyDown(_lastPressedKey))
            {
                _pressTimer.UpdateTimer();
                if(_pressTimer.Test())
                {
                    _pressTimerNewChar.UpdateTimer();
                    if( _pressTimerNewChar.Test())
                    {
                        _pressTimerNewChar.Reset();
                        InsertASingleCharacter(_lastPressedKey);
                    }
                }
            }
            else
            {
                _pressTimer.Reset();
                _pressTimerNewChar.Reset();
            }
        }
        public virtual void MoveCursorLeft()
        {
            if (_cursorTextPosition > 0)
                _cursorTextPosition--;
            LocateCursor();
        }
        public virtual void MoveCursorRight()
        {
            if (_cursorTextPosition < CurrentText.Length)
                _cursorTextPosition++;
            LocateCursor();
        }
        public void animateCursor()
        {
            _animationTimer.UpdateTimer();
            if(_animationTimer.Test())
            {
                Cursor.IsHidden = !Cursor.IsHidden;
                _animationTimer.Reset();
            }
        }
        public void HandleInput()
        {
            List<Keys> keys = Globals.Keyboard.GetClickedKeys();
            if (Selected)
            {
                if (keys.Count > 0)
                {
                    if (keys.Count > 1)
                        keys[0] = ExtractASingleCharacter(keys);
                    InsertASingleCharacter(keys[0]);
                }
            }
        }
        private Keys ExtractASingleCharacter(List<Keys> keys)
        {
            foreach (Keys key in keys)
            {
                if ((int)key >= 48 && (int)key <= 105)
                {
                    return key;
                }
            }
            return Keys.None;
        }
        protected virtual void InsertASingleCharacter(Keys key)
        {
            String value = "";
            if (Globals.Keyboard.NewKeyboardState.IsKeyDown(Keys.Back) || Globals.Keyboard.NewKeyboardState.IsKeyDown(Keys.Delete))
            {
                InsertChar('\b');
            }
            if ((int)key >= 48 && (int)key <= 105)
            {
                value = key.ToString().Substring(key.ToString().Length - 1);
                InsertChar(value.ToCharArray()[0]);
            }
            else if ((int)key == 0x20)
                InsertChar(' ');
            else if ((int)key == 188)
                InsertChar(',');
            else if ((int)key == 190)
                InsertChar('.');
            else if ((int)key == 186)
                InsertChar(';');
            else if ((int)key == 187)
                InsertChar('=');
            else if ((int)key == 189)
                InsertChar('-');
            else if ((int)key == 191)
                InsertChar('/');
            else if ((int)key == 219)
                InsertChar('[');
            else if ((int)key == 220)
                InsertChar('\\');
            else if ((int)key == 221)
                InsertChar(']');
            else if ((int)key == 222)
                InsertChar('\'');
            else if ((int)key == 13)
                InsertChar('\n');
            else if ((int)key == 38)
                MoveCursorUp();
            else if ((int)key == 40)
                MoveCursorDown();
            else if ((int)key == 37)
                MoveCursorLeft();
            else if ((int)key == 39)
                MoveCursorRight();
            _lastPressedKey = key;
        }
        public override void Update(Vector2 offset, float zoom)
        {
            base.Update();
            HandleInput();
            animateCursor();
            CheckPressedKey();
            if(Globals.Mouse.LeftClick())
            {
                if(this.Hover(offset, zoom))
                {
                    this.Selected = true;
                    MouseClickAction(offset, zoom);
                }
                else
                {
                    this.Selected = false;
                }
            }
        }
        protected virtual void MouseClickAction(Vector2 offset, float zoom)
        {
        }
        #endregion

        #region Draws
        public override void Draw (Vector2 offset, float zoom)
        {
            if(!IsHidden)
            {
                if(Selected)
                    Cursor.Draw(offset, zoom);
                Globals.SpriteBatch.DrawString(_font, CurrentText, (Pos+offset)*zoom, Globals.NotebookInterfaceColor, 0, new Vector2(0,0), zoom, SpriteEffects.None, 0);
            }
        }
        #endregion
    }
}