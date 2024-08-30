using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class TextBox : BasicTextSprite
    {
        #region Variables
        private List<String> _paragraphs = new();
        #endregion
        #region Constructors
        public TextBox(string description, Vector2 pos, Vector2 dims, Color color, SpriteFont font) : base(description, pos, color, font)
        {
            Dims = dims;
            ConvertDescriopotionToParagraphs();
        }
        public TextBox(string description, Vector2 pos, Vector2 dims, SpriteFont font) : base(description, pos, font)
        {
            Dims = dims;
            ConvertDescriopotionToParagraphs();
        }
        public TextBox(string description, Vector2 pos, Vector2 dims, Color color) : base(description, pos, color)
        {
            Dims = dims;
            ConvertDescriopotionToParagraphs();
        }
        public TextBox(string description, Vector2 pos, Vector2 dims) : base(description, pos)
        {
            Dims = dims;
            ConvertDescriopotionToParagraphs();
        }
        #endregion

        #region Methods
        public override void SetDescription(String text)
        {
            this.Description = text;
            ConvertDescriopotionToParagraphs();
        }

        private void ConvertDescriopotionToParagraphs()
        {
            _paragraphs = Description.Split("\\n").ToList();
            for (int i = 0; i < _paragraphs.Count; i++)
            {
                _paragraphs[i] = AddEndLinesToParagraph(_paragraphs[i]);
            }
        }
        private String AddEndLinesToParagraph(String paragraph)
        {
            String[] words = paragraph.Split(" ");
            String currentLine = "";
            paragraph = "";
            foreach (String word in words)
            {
                if(_font.MeasureString(currentLine+word).X <= Dims.X)
                {
                    currentLine += word+" ";
                }
                else
                {
                    paragraph += currentLine + "\n";
                    currentLine = word + " ";
                }
            }
            paragraph += currentLine;
            return paragraph;
        }
        private void ConvertDescriopotionToTextLines()
        {
            String tempMeasureString = "";
            String currentLine = "";
            int pointer = 0;
            int lastSpacePosition = 0;
            while(pointer <= Description.Length-1)
            {
                pointer = lastSpacePosition;
                while ((_font.MeasureString(currentLine)).X <= Dims.X)
                {
                    if (Description[pointer] == ' ') {
                        lastSpacePosition = pointer;
                    }
                    if (Description[pointer] == '\\' && Description[pointer+1] == 'n') {
                        pointer+=2;
                        lastSpacePosition = pointer;
                        break;
                    }
                    currentLine += Description[pointer];
                    pointer++;
                    if(pointer > Description.Length-1)
                    {
                        break;
                    }
                }
                if (currentLine.Length>0 && currentLine[0] == ' ' && currentLine.Length > 1)
                {
                    currentLine = currentLine.Substring(1, currentLine.Length - 1);
                }
                if (pointer != lastSpacePosition && pointer < Description.Length)
                {
                    currentLine = currentLine.Substring(0, currentLine.Length - (pointer - lastSpacePosition));
                }
                tempMeasureString += currentLine+"\n";
                currentLine = "";
            }
            Description = tempMeasureString;
        }
        #endregion
        #region Draws
        public override void Draw(Vector2 offset, float zoom)
        {
            int lineCount = 0;
            float lineHeight = _font.MeasureString("abc").Y;
            foreach(String paragraph in _paragraphs) {
                String[] lines = paragraph.Split('\n');
                Globals.SpriteBatch.DrawString(_font, paragraph, new Vector2((Pos.X + offset.X) * zoom, (Pos.Y + offset.Y+(lineHeight*lineCount) * zoom)), Color, Rot, new Vector2(0, 0), zoom, SpriteEffects, 0);
                lineCount += lines.Length;
            }
        }
        #endregion
    }
}
