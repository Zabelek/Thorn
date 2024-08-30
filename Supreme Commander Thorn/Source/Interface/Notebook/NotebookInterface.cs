using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Supreme_Commander_Thorn.Source.Engine.Utilities;
using Supreme_Commander_Thorn.Source.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Supreme_Commander_Thorn
{
    public class NotebookInterface : ViewLayer
    {
        #region variables
        private BasicSprite _mainBackground;
        private BlockingShadowActor _shadow;
        private BasicTimer _timer;

        private ElectronicDevice _viewedDevice;
        #endregion
        public NotebookInterface(BasicCamera camera) : base(camera)
        {
            camera.AllowMovement = false;
            camera.AllowScroll = false;
            //_mainBackground = new BasicSprite("Content/graphics/Interface/Notebook/Notebook_Background.png", new Vector2(0, 0), new Vector2(1920, 1080));
            _shadow = new BlockingShadowActor();
            this.AddChild(_shadow);
            //this.AddChild(_mainBackground);
            this.Hide();
        }
        public void SetDevice(ElectronicDevice device)
        {
            _viewedDevice = device;
            _mainBackground?.Tex?.Dispose();
            this.RemoveChild(_mainBackground);
            _mainBackground = new BasicSprite(device.BackgroundImagePath, new Vector2(0, 0), new Vector2(1920, 1080));
            this.AddChild(_mainBackground);
        }
        public override void Update()
        {
            base.Update();
            if (_viewedDevice?.System?.ShutDownSignal == true)
            {
                _viewedDevice.System.ShutDownSignal = false;
                Hide();
            }
            else if(_viewedDevice?.System?.ApplicationChanged == true)
            {
                _viewedDevice.System.CurrentApplication.Pos = new Vector2(220, 90);
                _viewedDevice.System.CurrentApplication.Run(this);
                _viewedDevice.System.ApplicationChanged = false;
            }
        }

        public override void Show()
        {
            base.Show();
            if (_viewedDevice.System.GetApplications()[0]!=null)
            {
                _viewedDevice.System.CurrentApplication.Pos = new Vector2(220, 90);
                _viewedDevice.System.CurrentApplication.Run(this);
            }
        }
    }
}
