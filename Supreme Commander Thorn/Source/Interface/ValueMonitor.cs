using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class ValueMonitor : WidgetGroup
    {
        #region Variables
        private int _yellowTresholdBelow, _redTresholdBelow, _yellowTresholdAbove, _redTresholdAbove, _currentValue;
        private BasicTextSprite _textSprite;
        public enum MonitorType { Below, Above, InBetween}
        public MonitorType Type;
        public BasicSprite Icon;
        #endregion

        #region Constructors
        public ValueMonitor(String path, int yellowTreshold, int redTreshold, Vector2 pos) 
        {
            this.Pos = pos;
            Icon = new BasicSprite(path, new Vector2(0,0), new Vector2(30, 40));
            _textSprite = new BasicTextSprite("0", new Vector2(40, 10), Globals.DefauldInterfaceColor, Globals.BiggerInterfaceFont);
            this._yellowTresholdBelow = yellowTreshold;
            this._redTresholdBelow = redTreshold;
            this.AddChild(Icon);
            this.AddChild(_textSprite);
            this.Type = MonitorType.Below;
            this._currentValue = 0;
        }
        public ValueMonitor(String path, int yellowTreshold, int redTreshold, Vector2 pos, MonitorType type) : this(path, yellowTreshold, redTreshold, pos)
        {
            this.Type = type;
            if(type == MonitorType.Above)
            {
                this._yellowTresholdAbove = yellowTreshold;
                this._redTresholdAbove = redTreshold;
            }
            this._currentValue = 100000;
        }
        public ValueMonitor(String path, int yellowTresholdBelow, int redTresholdBelow, int yellowTresholdAbove, int redTresholdAbove, Vector2 pos) : this(path, yellowTresholdBelow, redTresholdBelow, pos)
        {
            this._yellowTresholdAbove = yellowTresholdAbove;
            this._redTresholdAbove = redTresholdAbove;
            this.Type = MonitorType.InBetween;
            this._currentValue = yellowTresholdBelow + 1;
        }
        #endregion

        #region Methods
        public void Update(float newValue)
        {
            if(_currentValue!=(int)newValue)
            {
                if (this.Type == MonitorType.Below)
                {
                    if (_currentValue < _yellowTresholdBelow && newValue >= _yellowTresholdBelow)
                        Icon.DisplayColor = Color.Gray;
                    else if (_currentValue < _redTresholdBelow && newValue >= _redTresholdBelow)
                        Icon.DisplayColor = Color.Yellow;
                    else if (_currentValue > _redTresholdBelow && newValue <= _redTresholdBelow)
                        Icon.DisplayColor = Color.Red;
                    else if (_currentValue > _yellowTresholdBelow && newValue <= _yellowTresholdBelow)
                        Icon.DisplayColor = Color.Yellow;
                }
                else if (this.Type == MonitorType.Above)
                {
                    if (_currentValue > _yellowTresholdAbove && newValue <= _yellowTresholdAbove)
                        Icon.DisplayColor = Color.Gray;
                    else if (_currentValue > _redTresholdAbove && newValue <= _redTresholdAbove)
                        Icon.DisplayColor = Color.Yellow;
                    else if (_currentValue < _redTresholdAbove && newValue >= _redTresholdAbove)
                        Icon.DisplayColor = Color.Red;
                    else if (_currentValue < _redTresholdAbove && newValue >= _redTresholdAbove)
                        Icon.DisplayColor = Color.Yellow;
                }
                else
                {
                    if ((_currentValue < _yellowTresholdBelow && newValue >= _yellowTresholdBelow) || (_currentValue > _yellowTresholdAbove && newValue <= _yellowTresholdAbove))
                        Icon.DisplayColor = Color.Gray;
                    else if ((_currentValue < _redTresholdBelow && newValue >= _redTresholdBelow) || (_currentValue > _redTresholdAbove && newValue <= _redTresholdAbove))
                        Icon.DisplayColor = Color.Yellow;
                    else if ((_currentValue > _redTresholdBelow && newValue <= _redTresholdBelow) || (_currentValue < _redTresholdAbove && newValue >= _redTresholdAbove))
                        Icon.DisplayColor = Color.Red;
                    else if ((_currentValue > _yellowTresholdBelow && newValue <= _yellowTresholdBelow) || (_currentValue < _redTresholdAbove && newValue >= _redTresholdAbove))
                        Icon.DisplayColor = Color.Yellow;
                }
                this._currentValue = (int)newValue;
                _textSprite.SetDescription(_currentValue.ToString());
                //textSprite.setDescription(newValue.ToString());
            }
        }
        #endregion
    }
}
