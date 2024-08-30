using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class ElectronicDevice : Item
    {
        #region Variables
        public OperationSystem System;
        public String BackgroundImagePath;
        public Vector2 ScreenPosition, ScreenSize;
        #endregion
        #region Constructors
        public ElectronicDevice(int itemId, String name, String imagePath, String backgroundImagePath, Vector2 screenPosition, Vector2 screenSize) : base(itemId, name, imagePath)
        {
            BackgroundImagePath = backgroundImagePath;
            ScreenPosition = screenPosition;
            ScreenSize = screenSize;
            System = new OperationSystem(this);
        }
        public ElectronicDevice(int itemId, String name, String imagePath, String backgroundImagePath) : base(itemId, name, imagePath)
        {
            BackgroundImagePath = backgroundImagePath;
            ScreenPosition = new Vector2(0,0);
            ScreenSize = new Vector2(1920, 1080);
            System = new OperationSystem(this);
        }
        public ElectronicDevice(int itemId, String name, String imagePath) : base(itemId, name, imagePath)
        {
        }
        #endregion

        #region Methods
        public override void Update()
        {
            System.Update();
        }
        #endregion
    }

}
