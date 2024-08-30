using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using static System.Net.Mime.MediaTypeNames;

namespace Supreme_Commander_Thorn
{
    public class Perspective
    {
        #region Variables
        private BasicSprite _locationBackground, _stormBackground;
        private BasicTimer _universeTimeTimer;
        public Location CurrentLocation;
        public ViewLayer WorldViewLayer;
        public Gui MainGui;
        public InventoryInterface InventoryInterface;
        public NotebookInterface NotebookInterface;
        public ViewLayer CustomViewLayer;
        #endregion

        #region Constructors
        public Perspective() {
            WorldViewLayer = new ViewLayer(new BasicCamera(Globals.GraphicsDeviceManager.GraphicsDevice.Viewport));
            _universeTimeTimer = new BasicTimer(1000);
            MainGui = new Gui(new BasicCamera(Globals.GraphicsDeviceManager.GraphicsDevice.Viewport), this);
            InventoryInterface = new InventoryInterface(new BasicCamera(Globals.GraphicsDeviceManager.GraphicsDevice.Viewport), this);
            NotebookInterface = new NotebookInterface(new BasicCamera(Globals.GraphicsDeviceManager.GraphicsDevice.Viewport));
            WorldViewLayer.OverlyingSpriteParents.Add(MainGui);
            WorldViewLayer.OverlyingSpriteParents.Add(InventoryInterface);
            WorldViewLayer.OverlyingSpriteParents.Add(NotebookInterface);
            MainGui.OverlyingSpriteParents.Add(InventoryInterface);
            MainGui.OverlyingSpriteParents.Add(NotebookInterface);
            _locationBackground = new BasicSprite("Content\\graphics\\standby.png", Vector2.Zero, new Vector2(1920, 1080));
            _stormBackground = new BasicSprite("Content\\graphics\\Effects\\Snow_Storm.png", Vector2.Zero, new Vector2(1920, 1080));
            SetCurrentLocation(Universe.MainCharacter.CurrentLocation);

        }
        #endregion

        #region Methods
        public void SetCurrentLocation(Location location)
        {
            if (this.CurrentLocation?.Usables.Count > 0)
                foreach (UsableObject usable in this.CurrentLocation.Usables)
                    usable.NestedGui = null;
            WorldViewLayer.Clear();
            WorldViewLayer.AddChild(_locationBackground);
            WorldViewLayer.AddChild(_stormBackground);
            this.CurrentLocation = location;
            this.MainGui.SetLcation(location);
            _locationBackground.Tex.Dispose();
            try
            {
                FileStream fileStream = new FileStream(location.GetImagePath(), FileMode.Open);
                _locationBackground.Tex = Texture2D.FromStream(Globals.GraphicsDeviceManager.GraphicsDevice, fileStream);
                fileStream.Dispose();
            }
            catch(IOException ex)
            {
                try
                {
                    FileStream fileStream = new FileStream(location.GetImagePathDayOnly(), FileMode.Open);
                    _locationBackground.Tex = Texture2D.FromStream(Globals.GraphicsDeviceManager.GraphicsDevice, fileStream);
                    fileStream.Dispose();
                }
                catch (IOException ex2)
                {
                    FileStream fileStream = new FileStream("Content\\graphics\\standby.png", FileMode.Open);
                    _locationBackground.Tex = Texture2D.FromStream(Globals.GraphicsDeviceManager.GraphicsDevice, fileStream);
                    fileStream.Dispose();
                }
            }
            Planet loctest = location.GetPlanet();
            GameSubsystems.NainPlanetClimateController.ControlledPlanet = loctest;
            //setting up usable objects
            if(location.Usables.Count > 0)
                foreach(UsableObject usable in location.Usables)
                {
                    if (Universe.IsItDay())
                        usable.ChangeToDayVersion();
                    else
                        usable.ChangeToNightVersion();
                    WorldViewLayer.AddChild(usable.ObjectButton);
                    usable.NestedGui = this.MainGui;
                }
            //setting correct storm effect depending on region
            if(location.IsOpenSpace)
            {
                WorldViewLayer.RemoveChild(_stormBackground);
                _stormBackground.Tex.Dispose();
                if(Universe.IsItDay())
                {
                    if (location.AverageTemperature > 0)
                        _stormBackground = new BasicSprite("Content\\graphics\\Effects\\Snow_Storm.png", Vector2.Zero, new Vector2(1920, 1080));
                    else if (location.AverageTemperature < 40)
                        _stormBackground = new BasicSprite("Content\\graphics\\Effects\\Snow_Storm.png", Vector2.Zero, new Vector2(1920, 1080));
                    else
                        _stormBackground = new BasicSprite("Content\\graphics\\Effects\\Snow_Storm.png", Vector2.Zero, new Vector2(1920, 1080));
                }
                else
                {
                    if (location.AverageTemperature > 0)
                        _stormBackground = new BasicSprite("Content\\graphics\\Effects\\Snow_Storm_n.png", Vector2.Zero, new Vector2(1920, 1080));
                    else if (location.AverageTemperature < 40)
                        _stormBackground = new BasicSprite("Content\\graphics\\Effects\\Snow_Storm_n.png", Vector2.Zero, new Vector2(1920, 1080));
                    else
                        _stormBackground = new BasicSprite("Content\\graphics\\Effects\\Snow_Storm_n.png", Vector2.Zero, new Vector2(1920, 1080));
                }
                WorldViewLayer.AddChild(_stormBackground);
                if (GameSubsystems.NainPlanetClimateController.IsStormRunning)
                    _stormBackground.Show();
                else
                    _stormBackground.Hide();
            }
        }

        public void Update()
        {
            if(Universe.Timer.Test())
            {
                if (Universe.DayNightShift)
                    SetCurrentLocation(CurrentLocation);
            }
            WorldViewLayer.Update();
            MainGui.Update();
            InventoryInterface.Update();
            NotebookInterface.Update();
            CustomViewLayer?.Draw();
            if (GameSubsystems.NainPlanetClimateController.IsStormRunning && CurrentLocation.IsOpenSpace==true)
                _stormBackground.Show();
            else _stormBackground.Hide();
        }
        #endregion

        #region Draws
        public void Draw()
        {
            WorldViewLayer.Draw();
            MainGui.Draw();
            InventoryInterface.Draw();
            NotebookInterface.Draw();
            CustomViewLayer?.Draw();
        }
        #endregion
    }
}
