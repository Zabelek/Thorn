using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class Inventory
    {
        public List<ItemSlot> ItemSlots = new();

        public Inventory() { }
        public Inventory(int numberOfSlots)
        {
            for(int i=0; i<numberOfSlots; i++)
            {
                ItemSlots.Add(new ItemSlot());
            }
        }
        public void RegisterAllItems()
        {
            foreach(ItemSlot slot in ItemSlots)
            {
                if (slot.Item != null)
                {
                    Universe.RegisterItem(slot.Item);
                }
            }
        }
        public bool HasItem(Item item)
        {
            foreach(ItemSlot slot in ItemSlots)
            {
                if(slot.Item?.ItemID == item.ItemID)
                    return true;
            }
            return false;
        }
        public bool HasItem(int itemID)
        {
            foreach (ItemSlot slot in ItemSlots)
            {
                if (slot.Item?.ItemID == itemID)
                    return true;
            }
            return false;
        }
        public Item GetItem(int itemID)
        {
            foreach (ItemSlot slot in ItemSlots)
            {
                if (slot.Item?.ItemID == itemID)
                    return slot.Item;
            }
            return null;
        }
    }
}
