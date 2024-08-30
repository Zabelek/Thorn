using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Supreme_Commander_Thorn.Source.Engine.Basics
{
    public interface ISpriteParent
    {
        public void AddChild(BasicSprite s);
        public void RemoveChild(BasicSprite s);
        public void Clear();
        public void Show();
        public void Hide();
        public List<Actor> GetChildActors();
    }
}
