using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public static class DragAndDropManager
    {
        private static readonly List<IDraggableElement> _draggables = new();
        private static IDraggableElement _draggable;
        private static readonly List<ItemSlotSprite> _itemSlots = new();

        public static void AddDraggable(IDraggableElement draggable)
        {
            _draggables.Add(draggable);
        }
        internal static void RemoveDraggable(IDraggableElement draggable)
        {
            _draggables.Remove(draggable);
        }
        public static void AddItemSlot(ItemSlotSprite slot)
        {
            _itemSlots.Add(slot);
        }
        internal static void RemoveItemSlot(ItemSlotSprite slot)
        {
            _itemSlots.Remove(slot);
        }
        private static void CheckDragStart()
        {
            if(Globals.Mouse.LeftClick())
            {
                foreach(var item in _draggables)
                {
                    if(item.Rectangle.Contains(Globals.Mouse.NewMousePos))
                    {
                        _draggable = item;
                        break;
                    }
                }
            }
        }
        private static void CheckDragStop()
        {
            if(Globals.Mouse.LeftClickRelease())
            {
                if(_draggable is ItemSprite)
                {
                    bool assignedFlag = false;
                    ItemSlotSprite parentSlot = null;
                    foreach (var itemSlot in _itemSlots)
                    {
                        if (itemSlot.ColidesWithItem((ItemSprite)(_draggable)))
                        {
                            itemSlot.AssignItem((ItemSprite)(_draggable));
                            assignedFlag = true; break;
                        }
                    }
                    if (!assignedFlag)
                    {
                        ((ItemSprite)_draggable).ParentSlotSprite.AssignItem((ItemSprite)_draggable);
                    }
                }
                _draggable = null;
            }
        }
        public static void Update()
        {
            CheckDragStart();
            if(_draggable!= null)
            {
                if(_draggable is ItemSprite)
                {
                    Vector2 newPos = new Vector2(Globals.Mouse.NewMousePos.X - _draggable.Rectangle.Width / 2-((ItemSprite)_draggable).ParentOffset.X,
                        Globals.Mouse.NewMousePos.Y - _draggable.Rectangle.Height / 2-((ItemSprite)_draggable).ParentOffset.Y);
                    _draggable.Pos = newPos;
                    CheckDragStop();
                }
                else
                {
                    Vector2 newPos = new Vector2(Globals.Mouse.NewMousePos.X - _draggable.Rectangle.Width / 2, Globals.Mouse.NewMousePos.Y - _draggable.Rectangle.Height / 2);
                    _draggable.Pos = newPos;
                    CheckDragStop();
                }
            }    
        }
    }
}
