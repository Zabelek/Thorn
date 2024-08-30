using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class ItemSprite : BasicSprite, IDraggableElement
    {
        #region Variables
        private Rectangle _rectangle;
        public Rectangle Rectangle => _rectangle;
        public Vector2 ParentOffset;
        public ItemSlotSprite ParentSlotSprite;
        public Item LogicalItem;
        Vector2 IDraggableElement.Pos { get => this.Pos; set => this.Pos = value; }
        #endregion

        #region Constructors
        public ItemSprite(String path, Vector2 pos) : base(path, pos)
        {
            _rectangle = new Rectangle((int)(Pos.X), (int)(pos.Y), (int)(Dims.X), (int)(Dims.Y));
            (this as IDraggableElement).RegisterDraggable();
        }
        public ItemSprite(String path, Vector2 pos, Vector2 dims) : base(path, pos, dims)
        {
            _rectangle = new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)(Dims.X), (int)(Dims.Y));
            (this as IDraggableElement).RegisterDraggable();
        }
        public ItemSprite(Item item) : base(item.ImagePath, new Vector2(0,0), new Vector2(100, 100))
        {
            _rectangle = new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)(Dims.X), (int)(Dims.Y));
            LogicalItem = item;
            (this as IDraggableElement).RegisterDraggable();
        }
        #endregion

        #region Methods
        public override void Update()
        {
            base.Update();
        }
        public override void Update(Vector2 offset, float zoom)
        {
            base.Update(offset, zoom);
            _rectangle = new Rectangle((int)(Pos.X+offset.X),(int)(Pos.Y+offset.Y), (int)(Dims.X), (int)(Dims.Y));
        }
        #endregion
    }
}
