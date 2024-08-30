using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class ItemSlot
    {
        public Item Item;
        public ItemSlot()
        {

        }
        public void RegisterSlot()
        {
            if (Item != null)
            {
                Item.ParentSlot = this;
            }
        }
    }
    
}
