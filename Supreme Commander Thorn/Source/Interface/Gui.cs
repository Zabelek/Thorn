using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using Supreme_Commander_Thorn.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Supreme_Commander_Thorn
{
    public class Gui : ViewLayer
    {
        #region Variables
        private WidgetGroup _interfaceComponents, _additionalMenuComponents;
        private BasicSprite _mainInterfaceBackground, _additionalFunctionsBackground, _DialogueNameBackground;
        private List<InterfaceButton> _additionalButtons = new();
        private String _currentlySpeakingNpc;
        private BasicButton _exitButton, _saveButton, _goButton, _hideInterfaceButton, _hideAdditionalMenuButton, _updateWorldButton, _inventoryButton,
            _notebookButton;
        private BasicTextSprite _nameTag, _textBox, _clockHour, _clockDate;
        private ValueMonitor _airMonitor, _temperatureMonitor, _radiationMonitor;
        private Location _currentLocation;
        private Perspective _perspective;
        public bool IsInterfaceHidden, IsAdditionalActionsHidden, IsDialogueHidden;
        #endregion

        #region Constructors
        public Gui(BasicCamera camera, Perspective perspective) : base(camera)
        {
            camera.AllowMovement = false;
            camera.AllowScroll = false;
            this._perspective = perspective;
            _interfaceComponents = new WidgetGroup();
            _additionalMenuComponents = new WidgetGroup();
            _mainInterfaceBackground = new BasicSprite("Content/graphics/Interface/Main_Interface_Background.png", new Vector2(0, 0), new Vector2(1920, 1080));
            _additionalFunctionsBackground = new BasicSprite("Content/graphics/Interface/Additional_Functions_Backgrwound.png", new Vector2(0, 0),
                new Vector2(1920, 1080));
            _hideInterfaceButton = new BasicButton("Content/graphics/Interface/Show_Hide_Interface_Button.png", new Vector2(1864, 1052),
                new Vector2(56, 28), ShowHideInterfaceButton_Click, null);
            _hideAdditionalMenuButton = new BasicButton("Content/graphics/Interface/Hide_Additional_Menu_Button.png", new Vector2(1555, 754),
                new Vector2(44, 79), HideAdditionalMenuButton_Click, null);
            _hideAdditionalMenuButton.DisplayColor = Color.Black;
            _hideAdditionalMenuButton.HoverColor = Color.Gray;
            _hideAdditionalMenuButton.ClickedColor = Color.DarkGray;
            //dialogue section
            _DialogueNameBackground = new BasicSprite("Content/graphics/Interface/Dialogue_Name_Background.png", new Vector2(0, 0), new Vector2(1920, 1080));
            _currentlySpeakingNpc = "";
            _nameTag = new BasicTextSprite(_currentlySpeakingNpc, new Vector2(420, 760), Globals.DefauldInterfaceColor, Globals.BiggerInterfaceFont);
            _textBox = new BasicTextSprite("It's empty here, for now.\nBut it will change, soon enough :)", new Vector2(400, 810),
                Globals.DefauldInterfaceColor, Globals.DefaultInterfaceFont);

            //save, exit, walk annd stuff
            _exitButton = new BasicButton("Content/graphics/Interface/Exit_Button.png", new Vector2(10, 1010), new Vector2(40, 40), ExitGameBuitton_Click, null);
            _saveButton = new BasicButton("Content/graphics/Interface/Save_Button.png", new Vector2(70, 1010), new Vector2(40, 40), SaveGameButton_Click, null);  
            _updateWorldButton = new BasicButton("Content/graphics/Interface/Update_World_Button.png", new Vector2(130, 1010),
                new Vector2(40, 40), UpdateWorldButton_Click, null);
            _goButton = new BasicButton("Content/graphics/Interface/Go_Button.png", new Vector2(1600, 1010), new Vector2(40, 40), OpenGoingMenu, null);
            _inventoryButton = new BasicButton("Content/graphics/Interface/Inventory_Button.png", new Vector2(1720, 1010),
                new Vector2(40, 40), OpenInventoryButton_Click, null);
            _notebookButton = new BasicButton("Content/graphics/Interface/Notebook_Button.png", new Vector2(1660, 1010),
                new Vector2(40, 40), OpenNotebookButton_Click, null);
            //clock and monitors
            _clockHour = new BasicTextSprite("00:00", new Vector2(1310, 6), Globals.DefauldInterfaceColor, Globals.BiggerInterfaceFont);
            _clockDate = new BasicTextSprite("1000:10:10", new Vector2(1310, 27), Globals.DefauldInterfaceColor, Globals.SmallerInterfaceFont);
            _airMonitor = new ValueMonitor("Content/graphics/Interface/Air_Icon.png", 85, 70, new Vector2(1820, 5));
            _temperatureMonitor = new ValueMonitor("Content/graphics/Interface/Temparature_Icon.png", 10, -5, 40, 55, new Vector2(1720, 5));
            _radiationMonitor = new ValueMonitor("Content/graphics/Interface/Radiation_Icon.png", 5, 10, new Vector2(1630, 5), ValueMonitor.MonitorType.Above);
            this.AddChild(_interfaceComponents);
            this.AddChild(_hideInterfaceButton);
            this._additionalMenuComponents.AddChild(_additionalFunctionsBackground);
            this._additionalMenuComponents.AddChild(_hideAdditionalMenuButton);
            this._interfaceComponents.AddChild(_mainInterfaceBackground);
            this._interfaceComponents.AddChild(_DialogueNameBackground);
            this._interfaceComponents.AddChild(_nameTag);
            this._interfaceComponents.AddChild(_textBox);
            this._interfaceComponents.AddChild(_airMonitor);
            this._interfaceComponents.AddChild(_temperatureMonitor);
            this._interfaceComponents.AddChild(_radiationMonitor);
            this._interfaceComponents.AddChild(_exitButton);
            this._interfaceComponents.AddChild(_saveButton);
            this._interfaceComponents.AddChild(_updateWorldButton);
            this._interfaceComponents.AddChild(_goButton);
            this._interfaceComponents.AddChild(_inventoryButton);
            this._interfaceComponents.AddChild(_notebookButton);
            this._interfaceComponents.AddChild(_additionalMenuComponents);
            this._interfaceComponents.AddChild(_clockDate);
            this._interfaceComponents.AddChild(_clockHour);
            this.IsAdditionalActionsHidden = true;
            this._additionalMenuComponents.Hide();
            this.HideNameTag();
        }
        #endregion

        #region Methods
        //This is the place for importing new features to the current world without the need for recreating it.
        private void UpdateWorld(Object info)
        {
            ElectronicDevice notebook = new ElectronicDevice(1, "My Notebook", "Content\\Zacks_Notebook.png",
                "Content/graphics/Interface/Notebook/Notebook_Background.png", new Vector2(230, 95), new Vector2(1450, 890));
            notebook.System.InstallApplication("Core");
            notebook.System.InstallApplication("Diary");
            Universe.MainCharacter.Inventory.ItemSlots[3].Item = notebook;
            //Universe.MainCharacter.Inventory.ItemSlots[1].Item = new Item(0);
        }
        public override void Update()
        {
            base.Update();
            if(this._currentLocation != null )
            {
                _airMonitor.Update(_currentLocation.AirQuality);
                _temperatureMonitor.Update(_currentLocation.CurrentTemperature);
                _radiationMonitor.Update(_currentLocation.RadioationLevel);
            }
            if(Universe.GameDate.Minute<10)
                _clockHour.SetDescription(Universe.GameDate.Hour + ":0" + Universe.GameDate.Minute);
            else
                _clockHour.SetDescription(Universe.GameDate.Hour+":"+Universe.GameDate.Minute);
            _clockDate.SetDescription(Universe.GameDate.Year+"."+Universe.GameDate.Month + "." + Universe.GameDate.Day);
        }
        public void SetLcation(Location location)
        {
            this._currentLocation = location;
        }
        public virtual void HideInterface()
        {
            _interfaceComponents.Hide();
            IsInterfaceHidden = true;
        }
        public virtual void ShowInterface()
        {
            _interfaceComponents.Show();
            IsInterfaceHidden = false;
        }
        public virtual void ShowAdditionalOptions(List<InterfaceButton> additionalButtons)
        {
            ClearAdditionalOptions();
            if (additionalButtons?.Count>0) 
            {
                _additionalMenuComponents.Show();
                this.IsAdditionalActionsHidden = false;
                foreach (InterfaceButton button in additionalButtons)
                {
                    _additionalMenuComponents.AddChild(button);
                    this._additionalButtons.Add(button);
                }
            }
        }
        public void ShowAdditionalOptions()
        {
            _additionalMenuComponents.Show();
            this.IsAdditionalActionsHidden = false;
        }
        public void HideAdditionalOptions()
        {
            _additionalMenuComponents.Hide();
            this.IsAdditionalActionsHidden = true;
        }
        public void ClearAdditionalOptions()
        {
            if(_additionalButtons.Count > 0)
            {
                foreach (InterfaceButton button in _additionalButtons)
                {
                    _additionalMenuComponents.RemoveChild(button);
                }
                this._additionalButtons.Clear();
            }
            _additionalMenuComponents.Hide();
            this.IsAdditionalActionsHidden = true;
        }
        public void ShowNameTag(string name)
        {
            _currentlySpeakingNpc = name;
            this.IsDialogueHidden = false;
            this._DialogueNameBackground.Show();
            this._nameTag.SetDescription(_currentlySpeakingNpc);
            this._nameTag.Show();
        }
        public void HideNameTag()
        {
            _currentlySpeakingNpc = null;
            this.IsDialogueHidden = true;
            this._DialogueNameBackground.Hide();
            this._nameTag.Hide();
        }
        private void ExitGame(object info)
        {
            System.Environment.Exit(0);
        }

        private void SaveGame(object info)
        {
            FileManager.SaveUniverse();
            FileManager.SaveClimateController(GameSubsystems.NainPlanetClimateController);
            FileManager.SaveDate();
        }

        public void OpenGoingMenu(object info)
        {
            List<InterfaceButton> buttons = new List<InterfaceButton>();
            
            Location loc = null;
            if (!_currentLocation.ParentLocation.IsPlanet())
                loc = (Location)_currentLocation.ParentLocation;
            int tempHeight = -40;
            if (loc != null)
            {
                tempHeight += 40;
                buttons.Add(new InterfaceButton("Move out", new Vector2(1615, 120+ tempHeight), 230, MoveToLocationButton_Click, loc));
                foreach(Location subloc in loc.Sublocations)
                {
                    if (subloc != _currentLocation)
                    {
                        tempHeight += 40;
                    buttons.Add(new InterfaceButton(subloc.GetShortName(), new Vector2(1615, 120+ tempHeight), 230, MoveToLocationButton_Click, subloc));
                    }
                }
            }
            if(_currentLocation.Sublocations.Count>0)
            {
                foreach(Location subloc in _currentLocation.Sublocations)
                {
                        tempHeight += 40;
                        buttons.Add(new InterfaceButton(subloc.GetShortName(), new Vector2(1615, 120 + tempHeight), 230, MoveToLocationButton_Click, subloc));
                }
            }
            ShowAdditionalOptions(buttons);
        }
        public void showInspectUsableObjectWindow(UsableObject usable)
        {
            this.AddChild(new UsableObjectInspectionWindow(usable));
        }
        #endregion

        #region Click Functions
        private void ShowHideInterfaceButton_Click(object info)
        {
            if (!IsInterfaceHidden)
                HideInterface();
            else
                ShowInterface();
        }
        private void HideAdditionalMenuButton_Click(object info)
        {
            ClearAdditionalOptions();
        }
        private void UpdateWorldButton_Click(Object info)
        {
            this.AddChild(new BasicPopupWindow("Do you want to update the world?", UpdateWorld, null));
        }
        private void SaveGameButton_Click(object info)
        {
            this.AddChild(new BasicPopupWindow("Do you want to save?", SaveGame, null));
        }
        private void ExitGameBuitton_Click(object info)
        {
            this.AddChild(new BasicPopupWindow("Do you want to exit?", ExitGame, null));
        }
        private void MoveToLocationButton_Click(Object info)
        {
            _perspective.SetCurrentLocation((Location)info);
            this.ClearAdditionalOptions();
        }
        private void OpenInventoryButton_Click(Object info)
        {
            _perspective.InventoryInterface.Show();
            _perspective.InventoryInterface.LoadInventory(Universe.MainCharacter.Inventory);
        }
        private void OpenNotebookButton_Click(Object info)
        {
            if (Universe.MainCharacter.Inventory.HasItem(1))
            {
                _perspective.NotebookInterface.SetDevice((ElectronicDevice)(Universe.MainCharacter.Inventory.GetItem(1)));
                _perspective.NotebookInterface.Show();
            }
        }
        #endregion
    }
}
