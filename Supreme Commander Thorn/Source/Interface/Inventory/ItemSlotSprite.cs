using Microsoft.Xna.Framework;
using Supreme_Commander_Thorn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class ItemSlotSprite : Actor
    {
        #region Variables
        private InventoryInterface _parentInterface;

        public ItemSprite PlacedItemSprite;
        public Vector2 ParentOffset;
        public ItemSlot LogicalItemSlot;
        #endregion

        #region Constructors
        public ItemSlotSprite(InventoryComponent component) : base("Content\\graphics\\Interface\\Inventory\\Item_Slot.png", new Vector2(0,0), new Vector2(100, 100))
        {
            _parentInterface = component.ParentInterface;
            DragAndDropManager.AddItemSlot(this);
        }
        #endregion

        #region Methods
        public bool ColidesWithItem(ItemSprite item)
        {
            if(PlacedItemSprite == null){
                if (item.Rectangle.Contains(new Vector2(Pos.X + Dims.X / 2 + ParentOffset.X, Pos.Y + Dims.Y / 2 + ParentOffset.Y)))
                    return true;
                return false;
            }
            else
            {
                //here will be a good place to eventually connect items or something like that
                return false;
            }
        }
        public void AssignItem(ItemSprite draggable)
        {
            if(draggable.ParentSlotSprite!=null)
            {
                draggable.LogicalItem.ParentSlot.Item = null;
                draggable.LogicalItem.ParentSlot = null;
                draggable.ParentSlotSprite.PlacedItemSprite = null;
                draggable.ParentSlotSprite = null;               
            }
            draggable.Pos = this.Pos;
            draggable.LogicalItem.ParentSlot = this.LogicalItemSlot;
            LogicalItemSlot.Item = draggable.LogicalItem;
            draggable.ParentSlotSprite = this;
            PlacedItemSprite = draggable;
        }
        public override void Update()
        {
            base.Update();
            PlacedItemSprite?.Update();
        }
        public override void Update(Vector2 offset, float zoom)
        {
            base.Update(offset, zoom);
            ParentOffset = offset;
            if (PlacedItemSprite != null)
            {
                PlacedItemSprite.Update(offset, zoom);
                PlacedItemSprite.ParentOffset = offset;
            }
        }
        public override void UpdateForCollision(Vector2 offset, float zoom)
        {
            if (Hover(offset, zoom) && !IsOverlaid)
            {
                IsHovered = true;
                if (Globals.Mouse.LeftClick())
                {
                    IsPressed = true;
                }
                if (Globals.Mouse.LeftClickRelease())
                {
                    _parentInterface.SetItemToDescriptionWindow(LogicalItemSlot.Item);
                    IsPressed = false;
                }
            }
            else
            {
                IsPressed = false;
                IsHovered = false;
            }
        }
        public void Dispose()
        {
            DragAndDropManager.RemoveItemSlot(this);
            if (PlacedItemSprite != null)
            {
                DragAndDropManager.RemoveDraggable(PlacedItemSprite);
                PlacedItemSprite.Tex.Dispose();
            }
            Tex.Dispose();
        }
        #endregion

        #region Draws
        public override void Draw(Vector2 offset, float zoom)
        {
            base.Draw(offset, zoom);
            PlacedItemSprite?.Draw(offset, zoom);
        }
        #endregion
    }
}
