using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Supreme_Commander_Thorn;
using Supreme_Commander_Thorn.Source.Engine;

namespace Supreme_Commander_Thorn
{
    public class Main : Game
    {
        public CursorSprite cursor;
        public Perspective perspective;

        public Main()
        {
            Globals.GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Globals.ScreenWidth = 1920;
            Globals.ScreenHeight = 1080;
            Globals.GraphicsDeviceManager.PreferredBackBufferWidth = 1920;
            Globals.GraphicsDeviceManager.PreferredBackBufferHeight = 1080;
            Globals.GraphicsDeviceManager.IsFullScreen = true;
            Globals.GraphicsDeviceManager.HardwareModeSwitch = false;
            Globals.GraphicsDeviceManager.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.ContentManager = this.Content;
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SetUpFonts();
            cursor = new CursorSprite("Content/graphics/Interface/cursor.png", "Content/graphics/Interface/cursor clicked.png", new Vector2(0,0), new Vector2(32,32));
            FileManager.LoadUniverse();
            GameSubsystems.NainPlanetClimateController = FileManager.LoadClimateControllerForAPlanet(Universe.MainCharacter.CurrentLocation.GetPlanet());
            if (GameSubsystems.NainPlanetClimateController == null)
                GameSubsystems.NainPlanetClimateController = new ClimateController();
            perspective = new Perspective();
        }

        protected override void Update(GameTime gameTime)
        {
            Globals.GameTime = gameTime;
            GameSubsystems.NainPlanetClimateController.Update();
            Globals.Keyboard.Update();
            Globals.Mouse.Update();
            DragAndDropManager.Update();
            perspective.Update();
            //Because of the timer reseting, Universe has to update last
            Universe.Update();
            Globals.Keyboard.UpdateOld();
            Globals.Mouse.UpdateOld();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Globals.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            perspective.Draw();
            cursor.Draw(new Vector2(Globals.Mouse.NewMousePos.X, Globals.Mouse.NewMousePos.Y));
            Globals.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
    public static class Program
    {
        static void Main(string[] args)
        {
            using var game = new Supreme_Commander_Thorn.Main();
            game.Run();
        }
    }
}