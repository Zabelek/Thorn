using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn.Source.Engine.Utilities
{
    internal class TextInputField : BasicTextInput
    {
        #region Variables
        private List<String> _paragraphs = new();
        private int _currentParagraph;
        #endregion

        #region Constructors
        public TextInputField(Vector2 pos, Vector2 dims) : base(pos, dims)
        {
            _currentParagraph = 0;
            ConvertDescriopotionToParagraphs();

        }
        #endregion

        #region Methods
        private void ConvertDescriopotionToParagraphs()
        {
            _paragraphs = CurrentText.Split("\\n").ToList();
            for (int i = 0; i < _paragraphs.Count; i++)
            {
                _paragraphs[i] = AddEndLinesToParagraph(_paragraphs[i]);
            }
        }
        private String AddEndLinesToParagraph(String paragraph)
        {
            String trimParagraph = paragraph.Replace("\n", "");
            _cursorTextPosition-=paragraph.Split('\n').Length-1;
            String[] words = trimParagraph.Split(" ");
            String currentLine = "";
            paragraph = "";
            for (int i=0; i < words.Length; i++)
            {
                if (_font.MeasureString(currentLine + words[i]).X <= Dims.X)
                {

                    currentLine += words[i];
                    if (i != words.Length - 1)
                        currentLine += " ";
                }
                else
                {
                    paragraph += currentLine + "\n";
                    _cursorTextPosition += 1;
                    currentLine = words[i];
                    if (i != words.Length - 1)
                        currentLine += " ";
                }
            }
            paragraph += currentLine;
            return paragraph;
        }
        protected override void InsertASingleCharacter(Keys key)
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
                BreakLine();
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
        public virtual void InsertChar(char ch)
        {
            bool lowerCase = true;
            if (Globals.Keyboard.NewKeyboardState.CapsLock || Globals.Keyboard.GetPress("LeftShift") || Globals.Keyboard.GetPress("RightShift"))
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
            if (ch != '\b')
            {
                if (lowerCase)
                    ch = char.ToLower(ch);
                _paragraphs[_currentParagraph] = _paragraphs[_currentParagraph].Insert(_cursorTextPosition, ch.ToString());
                _cursorTextPosition++;
            }
            else if (HasAnyText() && _cursorTextPosition > 0)
            {
                _paragraphs[_currentParagraph] = _paragraphs[_currentParagraph].Remove(_cursorTextPosition - 1, 1);
                _cursorTextPosition--;
            }
            _paragraphs[_currentParagraph] = AddEndLinesToParagraph(_paragraphs[_currentParagraph]);
            //ConvertDescriopotionToParagraphs();
            LocateCursor();
        }
        public bool HasAnyText()
        {
            foreach (var paragraph in _paragraphs)
            {
                if(paragraph.Length>0)
                    return true;
            }
            return false;
        }
        private void BreakLine()
        {
            String oldParagraph = _paragraphs[_currentParagraph].Substring(0, _cursorTextPosition);
            String newParagraph = _paragraphs[_currentParagraph].Substring(_cursorTextPosition);
            _paragraphs[_currentParagraph] = oldParagraph;
            _paragraphs.Insert(_currentParagraph+1, newParagraph);
            _currentParagraph++;
            _cursorTextPosition = 0;
        }
        protected override void LocateCursor()
        {
            //reset animation
            Cursor.Show();
            _animationTimer.Reset();
            //locate cursor
            String tempLines = _paragraphs[_currentParagraph].Substring(0, _cursorTextPosition);
            String[] lines = tempLines.Split("\n");
            Vector2 xPos = _font.MeasureString(lines[lines.Length - 1]);
            float yPos = 0;
            Vector2 yPosPom = _font.MeasureString("abcd");
            if (String.Compare(lines[lines.Length - 1], "") == 0)
            {
                yPos = Pos.Y + (lines.Length - 1) * yPosPom.Y;
            }
            else
                yPos = Pos.Y + (lines.Length - 1) * xPos.Y;
            int lineCount = 0;
            for(int i=0; i < _currentParagraph; i++)
            {
                String[]subLines = _paragraphs[i].Split("\n");
                foreach(String subLine in subLines)
                {
                    lineCount++;
                }
            }
            Cursor.Pos = new Vector2(Pos.X + xPos.X, yPos+lineCount*yPosPom.Y);
        }
        public override void MoveCursorLeft()
        {
            if (_cursorTextPosition > 0)
                _cursorTextPosition--;
            else if(_currentParagraph>0)
            {
                _currentParagraph--;
                _cursorTextPosition = _paragraphs[_currentParagraph].Length-1;
                if( _cursorTextPosition < 0 )
                    _cursorTextPosition = 0;
            }
            LocateCursor();
        }
        public override void MoveCursorRight()
        {
            if (_cursorTextPosition < _paragraphs[_currentParagraph].Length)
                _cursorTextPosition++;
            else if(_currentParagraph< _paragraphs.Count-1)
            {
                _currentParagraph++;
                _cursorTextPosition = 0;
            }
            LocateCursor();
        }
        public override void MoveCursorUp()
        {
            String currentLine = "";
            String previousLine = "";
            int cursorPositionInTheLine = 0;
            //dividing into lines and finding current one;
            String[] currentParagraphLines = _paragraphs[_currentParagraph].Split("\n");
            int lineNumber = 0;
            int textLength = 0;
            while (textLength < _cursorTextPosition)
            {
                textLength += currentParagraphLines[lineNumber++].Length+1;
                if (textLength > _cursorTextPosition)
                {
                    cursorPositionInTheLine = _cursorTextPosition - (textLength - currentParagraphLines[lineNumber - 1].Length);
                }
            }
            if (lineNumber > 0)
                lineNumber--;
            currentLine = currentParagraphLines[lineNumber];
            //jumping
            if(lineNumber == 0 && _currentParagraph>0)
            {
                //this should jump to previous paragraph
                previousLine = _paragraphs[_currentParagraph - 1].Split("\n").Last();
                _currentParagraph--;
                _cursorTextPosition = _paragraphs[_currentParagraph].Length - 1- previousLine.Length;
                if (previousLine.Length < cursorPositionInTheLine)
                    _cursorTextPosition += previousLine.Length;
                else
                    _cursorTextPosition += cursorPositionInTheLine;
                _cursorTextPosition += 1;
            }
            else if(lineNumber == 0)
            {
                //this should be on the top of the text, so does nothing
            }
            else
            {
                //this should jump inside the paragraph
                previousLine = currentParagraphLines[lineNumber-1];
                _cursorTextPosition -=cursorPositionInTheLine;
                if (previousLine.Length >= cursorPositionInTheLine)
                    _cursorTextPosition -= previousLine.Length - cursorPositionInTheLine;
                else
                    _cursorTextPosition -= 1;
                _cursorTextPosition -= 1;
            }
            LocateCursor();
        }
        public override void MoveCursorDown()
        {
            String currentLine = "";
            String nextLine = "";
            int cursorPositionInTheLine = 0;
            //dividing into lines and finding a current one;
            String[] currentParagraphLines = _paragraphs[_currentParagraph].Split("\n");
            int lineNumber = 0;
            int textLength = 0;
            while(textLength<_cursorTextPosition)
            {
                textLength += currentParagraphLines[lineNumber++].Length+1;
                if(textLength > _cursorTextPosition)
                {
                    cursorPositionInTheLine = _cursorTextPosition-(textLength - currentParagraphLines[lineNumber-1].Length);
                }
            }
            if(lineNumber>0)
                lineNumber--;
            currentLine = currentParagraphLines[lineNumber];
            //jumping
            if (lineNumber == currentParagraphLines.Length - 1 && _currentParagraph<_paragraphs.Count-1)
            {
                //this should jump into the next paragraph
                nextLine = _paragraphs[_currentParagraph + 1].Split("\n")[0];
                _currentParagraph++;
                _cursorTextPosition = cursorPositionInTheLine;
                if(_cursorTextPosition>= nextLine.Length)
                {
                    _cursorTextPosition = nextLine.Length-1;
                }
                _cursorTextPosition += 1;
            }
            else if(lineNumber == currentParagraphLines.Length - 1)
            {
                //this shoulkd be the end of text (probalbly)
            }
            else
            {
                //this is a jump inside the current paragraph
                nextLine = currentParagraphLines[lineNumber+1];
                _cursorTextPosition += currentLine.Length - cursorPositionInTheLine;
                if (nextLine.Length >= cursorPositionInTheLine)
                    _cursorTextPosition += cursorPositionInTheLine;
                else
                    _cursorTextPosition += nextLine.Length-1;
                _cursorTextPosition += 1;

            }
            LocateCursor();
        }
        protected override void MouseClickAction(Vector2 offset, float zoom)
        {
            int virtualCursorPosition;
            float lineHeight = _font.MeasureString("ABC").Y;
            Vector2 mousePos = Globals.Mouse.NewMousePos-Pos;
            mousePos -= offset;
            if(mousePos.X>0)
            {
                String[] lines;
                int pastLines = 0;
                bool found = false;
                for (int i = 0; i < _paragraphs.Count; i++)
                {
                    lines = _paragraphs[i].Split('\n');
                    int allLinesLength = 0;
                    for (int j = 0; j < lines.Length; j++)
                    {
                        pastLines++;
                        allLinesLength += lines[j].Length + 1;
                        if (pastLines * lineHeight > mousePos.Y)
                        {
                            _currentParagraph = i;
                            _cursorTextPosition = allLinesLength - 1 - lines[j].Length;
                            String tempLine = lines[j];
                            while (_font.MeasureString(tempLine).X > mousePos.X)
                            {
                                String tmp = tempLine.Remove(tempLine.Length - 1, 1);
                                tempLine = tmp;
                            }
                            _cursorTextPosition += tempLine.Length;
                            LocateCursor();
                            found = true;
                            break;
                        }
                    }
                    if (found)
                    {
                        break;
                    }
                }
            }

            //paragraphs only
            //for (int i=0; i<_paragraphs.Count; i++)
            //{
            //    if (i*lineHeight > mousePos.Y)
            //    {
            //        _currentParagraph = i-1;
            //        _cursorTextPosition = 0;
            //        LocateCursor();
            //        break;
            //    }
            //}
        }
        #endregion

        #region Draws
        public override void Draw(Vector2 offset, float zoom)
        {
            if (Selected)
                Cursor.Draw(offset, zoom);
            int lineCount = 0;
            float lineHeight = _font.MeasureString("abc").Y;
            foreach (String paragraph in _paragraphs)
            {
                String[] lines = paragraph.Split('\n');
                Globals.SpriteBatch.DrawString(_font, paragraph, new Vector2((Pos.X + offset.X) * zoom, (Pos.Y + offset.Y + (lineHeight * lineCount) * zoom)), Globals.NotebookInterfaceColor, Rot, new Vector2(0, 0), zoom, SpriteEffects, 0);
                lineCount += lines.Length;
            }
        }
        #endregion
    }
}