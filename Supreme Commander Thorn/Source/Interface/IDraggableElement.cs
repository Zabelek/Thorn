using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public interface IDraggableElement
    {
        Rectangle Rectangle { get; }
        Vector2 Pos { get; set; }

        public void RegisterDraggable()
        {
            DragAndDropManager.AddDraggable(this);
        }

    }
}
