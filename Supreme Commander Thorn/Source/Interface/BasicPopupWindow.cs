using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn.Source
{
    public class BasicPopupWindow : WidgetGroup
    {
        #region Variables
        private BasicSprite _background;
        private Actor _shadow;
        public BasicTextSprite MessageSprite;
        public InterfaceButton ButtonYes, ButtonNo;
        #endregion

        #region Constructors
        public BasicPopupWindow(string message, PassObject function, object info)
        {
            Pos = new Vector2(670, 300);
            _background = new BasicSprite("Content/graphics/Interface/Popup_Window.png", new Vector2(0, 0), new Vector2(556, 277));
            _shadow = new Actor("Content/graphics/Interface/Popup_Window_Shadow.png", new Vector2(-670, -300), new Vector2(1920, 1080));
            _shadow.DisplayColor = new Color(255, 255, 255, 100);
            ButtonNo = new InterfaceButton("No", new Vector2(30, 220), NoButton_Click, null);
            ButtonYes = new InterfaceButton("Yes", new Vector2(130, 220), function, info);
            ButtonYes.ButtonClicks.Add(NoButton_Click);
            MessageSprite = new BasicTextSprite(message, new Vector2(50, 60));
            this.AddChild(_shadow);
            this.AddChild(_background);
            this.AddChild(MessageSprite);
            this.AddChild(ButtonNo);
            this.AddChild(ButtonYes);
        }
        #endregion

        #region Methods
        public void NoButton_Click(object info)
        {
            this.Parent.RemoveChild(this);
        }
        #endregion
    }
}
