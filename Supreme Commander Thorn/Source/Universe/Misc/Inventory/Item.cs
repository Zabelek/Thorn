using System;
using System.Xml.Serialization;

namespace Supreme_Commander_Thorn
{
    public class Item
    {
        #region Variables
        [XmlIgnore]
        public ItemSlot ParentSlot;
        public int ItemID;
        public String Name;
        public String Description;
        public String ImagePath;
        #endregion

        #region Constructors
        public Item()
        {
        }
        public Item(int itemId)
        {
            ItemID = itemId;
            Universe.RegisterItem(this);
        }
        public Item(int itemId, String name)
        {
            ItemID = itemId;
            Universe.RegisterItem(this);
            Name = name;
        }
        public Item(int itemId, String name, String imagePath)
        {
            ItemID = itemId;
            Universe.RegisterItem(this);
            Name = name;
            ImagePath = imagePath;
        }
        #endregion

        #region Methods()
        public virtual void Update()
        {
        }
        #endregion
    }
}