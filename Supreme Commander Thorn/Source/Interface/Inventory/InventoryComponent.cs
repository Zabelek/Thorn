using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class InventoryComponent : WidgetGroup
    {
        #region Variables
        private BasicSprite _inventoryBackground;
        private BasicButton _inventoryUp, _inventoryDown;
        private List<ItemSlotSprite> _itemSlots = new();
        private Inventory _displayedInventory;
        private int _nimItemIndex, _maxItemIndex;
        private BasicTextSprite _windowTitle;

        public InventoryInterface ParentInterface;
        #endregion

        #region Constructors
        public InventoryComponent(InventoryInterface parent) 
        {
            ParentInterface = parent;
            _nimItemIndex = 0;
            _maxItemIndex = 47;
            _inventoryBackground = new BasicSprite("Content\\graphics\\Interface\\Inventory\\Inventory_Background.png", new Vector2(0, 0));
            _inventoryUp = new BasicButton("Content\\graphics\\Interface\\Inventory\\Inventory_Arrow_Up.png", new Vector2(700, 65), InventoryUpButton_Click, null);
            _inventoryDown = new BasicButton("Content\\graphics\\Interface\\Inventory\\Inventory_Arrow_Down.png", new Vector2(700, 910), InventoryDownButton_Click, null);
            _windowTitle = new BasicTextSprite("My Inventory", new Vector2(60, 15), Globals.BiggerInterfaceFont);
            AddChild(_inventoryBackground);
            AddChild(_windowTitle);
            AddChild(_inventoryUp);
            AddChild(_inventoryDown);
        }
        #endregion

        #region Methods
        public void LoadInventory(Inventory inventory)
        {
            _displayedInventory = inventory;
            RecalculateSlotIndexes();
            foreach (var itemSlotSprite in _itemSlots)
            {
                itemSlotSprite.Dispose();
                this.RemoveChild(itemSlotSprite);
            }
            _itemSlots.Clear();
            int xPos = 0;
            int yPos = 0;
            int rowCounter = 0;
            for (int i = _nimItemIndex; i<=_maxItemIndex; i++)
            {
                ItemSlotSprite itemSlotSprite = new ItemSlotSprite(this);
                rowCounter++;
                itemSlotSprite.Pos = new Vector2(25 + xPos, 60 + yPos);
                xPos += 110;
                if (rowCounter == 6)
                {
                    xPos = 0;
                    yPos += 110;
                    rowCounter = 0;
                }
                itemSlotSprite.LogicalItemSlot = inventory.ItemSlots[i];
                if (inventory.ItemSlots[i].Item != null)
                {
                    itemSlotSprite.AssignItem(new ItemSprite(inventory.ItemSlots[i].Item));
                }
                _itemSlots.Add(itemSlotSprite);
            }
            foreach (var itemSlotSprite in _itemSlots)
            {
                this.AddChild(itemSlotSprite);
            }
        }
        private void RecalculateSlotIndexes()
        {
            _maxItemIndex = _nimItemIndex+47;
            if (_displayedInventory?.ItemSlots.Count <= _maxItemIndex)
                _maxItemIndex = _displayedInventory.ItemSlots.Count-1;
        }
        #endregion

        #region Clicks
        private void InventoryUpButton_Click(Object info)
        {
            _nimItemIndex -= 6;
            if(_nimItemIndex < 0)
                _nimItemIndex = 0;
            LoadInventory(_displayedInventory);
        }
        private void InventoryDownButton_Click(Object info)
        {
            _nimItemIndex += 6;
            LoadInventory(_displayedInventory);
        }
        #endregion
    }
}
