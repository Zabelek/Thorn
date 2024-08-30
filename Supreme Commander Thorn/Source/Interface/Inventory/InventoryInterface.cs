using Microsoft.Xna.Framework;
using Supreme_Commander_Thorn.Source.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class InventoryInterface : ViewLayer
    {
        #region Variables
        private Perspective _perspective;

        private BasicSprite _itemDescriptionBackground;
        private BasicSprite _itemDescriptionImage;
        private BasicTextSprite _itemDescriptionTitle;
        private TextBox _itemDescriptionDesc;

        private InterfaceButton _closeInventoryButton;
        private BlockingShadowActor _shadow;
        private InventoryComponent _firstInventory, _secondInventory;
        private BasicTextSprite _descriptionTitle;
        #endregion

        #region Constructors
        public InventoryInterface(BasicCamera camera, Perspective perspective) : base(camera)
        {
            camera.AllowMovement = false;
            camera.AllowScroll = false;
            _perspective = perspective;
            _shadow = new BlockingShadowActor();
            _shadow.DisplayColor = new Color(255, 255, 255, 230);
            _shadow.CustonOpacityMask = true;
            _itemDescriptionBackground = new BasicSprite("Content\\graphics\\Interface\\Inventory\\Description_Section_Background.png", new Vector2(10, 20));
            _closeInventoryButton = new InterfaceButton("       Close Inventory       ", new Vector2(830, 1000), CloseInventoryView, null);
            _descriptionTitle = new BasicTextSprite("Item Description", new Vector2(80, 35), Globals.BiggerInterfaceFont);

            _firstInventory = new InventoryComponent(this);
            _firstInventory.Pos = new Vector2(400, 20);

            _itemDescriptionImage = new BasicSprite("Content\\graphics\\Interface\\Inventory\\Item_Placeholder.png", new Vector2(70, 90), new Vector2(250, 250));
            _itemDescriptionTitle = new BasicTextSprite("This Is A Test Item To Display", new Vector2(45, 380), Globals.DefaultInterfaceFont);
            _itemDescriptionDesc = new TextBox("This Is A Test Item Description To Display, however I'd like to write something more descriptive, so it's a bit longer.", new Vector2(40, 420), new Vector2(300, 420), Globals.SmallerInterfaceFont);

            AddChild(_shadow);
            AddChild(_itemDescriptionBackground);
            AddChild(_descriptionTitle);
            AddChild(_closeInventoryButton);
            AddChild(_firstInventory);
            AddChild(_itemDescriptionImage);
            AddChild(_itemDescriptionTitle);
            AddChild(_itemDescriptionDesc);
            Hide();
        }
        #endregion

        #region Methods
        public void LoadInventory(Inventory inventory)
        {
            _firstInventory.LoadInventory(inventory);
        }
        private void CloseInventoryView(Object info)
        {
            Hide();
        }
        public void SetItemToDescriptionWindow(Item item)
        {
            _itemDescriptionImage.Tex.Dispose();
            this.RemoveChild(_itemDescriptionImage);
            if(item != null )
            {
                _itemDescriptionImage = new BasicSprite(item.ImagePath, new Vector2(70, 90), new Vector2(250, 250));
                this.AddChild(_itemDescriptionImage);
                _itemDescriptionTitle.SetDescription(item.Name);
                if(item.Description != null)
                {
                    _itemDescriptionDesc.SetDescription(item.Description);
                }
                else
                    _itemDescriptionDesc.SetDescription("");
            }
            else
            {
                _itemDescriptionImage = new BasicSprite("Content\\graphics\\Interface\\Inventory\\Item_Placeholder.png", new Vector2(70, 90), new Vector2(250, 250));
                _itemDescriptionTitle.SetDescription("");
                _itemDescriptionDesc.SetDescription("");
                this.AddChild(_itemDescriptionImage);
            }
        }
        #endregion
    }
}
