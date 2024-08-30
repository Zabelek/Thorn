using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Supreme_Commander_Thorn.Source.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Supreme_Commander_Thorn
{
    public class UsableObjectInspectionWindow : WidgetGroup
    {
        #region Variables
        private List<BasicSprite> _backgroundSprites = new();
        private List<BasicTextSprite> _textSprites = new();
        private BlockingShadowActor _shadow;
        public UsableObject UsableObj;
        public BasicTextSprite Title;
        public InterfaceButton IntButton;
        #endregion

        #region Constructors
        public UsableObjectInspectionWindow(UsableObject usableObj)
        {
            this.Pos = new Vector2(670, 150);
            this.UsableObj = usableObj;
            _shadow = new BlockingShadowActor();
            Title = new BasicTextSprite("Object Description", new Vector2(90, 10), Globals.DefauldInterfaceColor, Globals.BiggerInterfaceFont);
            CheckTypesForTextBoxes();
            int howMuchBackground = _textSprites.Count;
            _backgroundSprites.Add(new BasicSprite("Content/graphics/Interface/UsableWindow/Usable_Description_Box_Top.png", new Vector2(0, 0), new Vector2(557, 44)));
            for (int i = 0; i < howMuchBackground; i++)
            {
                _backgroundSprites.Add(new BasicSprite("Content/graphics/Interface/UsableWindow/Usable_Description_Box_Middle.png",
                    new Vector2(0, 44+i*32), new Vector2(557, 32)));
            }
            _backgroundSprites.Add(new BasicSprite("Content/graphics/Interface/UsableWindow/Usable_Description_Box_Boton.png",
                new Vector2(0, 44 + howMuchBackground * 32), new Vector2(557, 42)));
            IntButton = new InterfaceButton("OK", new Vector2(420, 45+32*howMuchBackground), IntButton_Click, null);
            this.AddChild(_shadow);
            foreach (BasicSprite sprite in _backgroundSprites)
            {
                this.AddChild(sprite);
            }
            foreach(BasicTextSprite sprite in _textSprites)
            {
                this.AddChild(sprite);
            }
            this.AddChild(IntButton);
            this.AddChild(Title);
        }
        #endregion

        #region Methods
        private void CheckTypesForTextBoxes()
        {
            int height = 0;
            _textSprites.Add(new BasicTextSprite("Name: "+UsableObj.Name, new Vector2(30, 50 + height)));
            height += 25;
            if (UsableObj is IElectricDevice)
            {
                String data = "Power: "+Math.Round(((IElectricDevice) UsableObj).CurrentEnergy, 2)+"/"+ ((IElectricDevice)UsableObj).EnergyStorageCapacity;
                _textSprites.Add(new BasicTextSprite(data, new Vector2(30, 50 + height)));
                height += 25;

                if(((IElectricDevice)UsableObj).IsRunning)
                    _textSprites.Add(new BasicTextSprite("Running: Yes", new Vector2(30, 50 + height)));
                else
                    _textSprites.Add(new BasicTextSprite("Running: No", new Vector2(30, 50 + height)));
                height += 25;
                if(((IElectricDevice)UsableObj).IsCharging)
                    _textSprites.Add(new BasicTextSprite("Charging: Yes", new Vector2(30, 50 + height)));
                else
                    _textSprites.Add(new BasicTextSprite("Charging: No", new Vector2(30, 50 + height)));
                height += 25;
            }
            if(UsableObj is IClimateModifier)
            {
                if(((IClimateModifier)UsableObj).TemperatureModValue!=0)
                {
                    _textSprites.Add(new BasicTextSprite("Warming Power: "+ ((IClimateModifier)UsableObj).TemperatureModValue, new Vector2(30, 50 + height)));
                    height += 25;
                }
                if (((IClimateModifier)UsableObj).RadiationModValue!= 0)
                {
                    _textSprites.Add(new BasicTextSprite("Radiation Power: " + ((IClimateModifier)UsableObj).RadiationModValue, new Vector2(30, 50 + height)));
                    height += 25;
                }
                if (((IClimateModifier)UsableObj).AirConditionModValue != 0)
                {
                    _textSprites.Add(new BasicTextSprite("Air Condition Power: " + ((IClimateModifier)UsableObj).AirConditionModValue, new Vector2(30, 50 + height)));
                    height += 25;
                }
            }
            if(UsableObj is IBed) 
            {
                if (((IBed)UsableObj).HealingCapabilities != 0)
                {
                    _textSprites.Add(new BasicTextSprite("Healing Capabilities: " + ((IBed)UsableObj).HealingCapabilities, new Vector2(30, 50 + height)));
                    height += 25;
                }
            }
        }

        public void IntButton_Click(object info)
        {
            this.Parent.RemoveChild(this);
        }
        #endregion
    }
}