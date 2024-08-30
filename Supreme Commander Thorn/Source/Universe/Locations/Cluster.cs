using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Supreme_Commander_Thorn
{
    public class Cluster
    {
        [XmlIgnore]
        public List<Star> Stars = new();
        public Vector2 Position { get; set; }
        public String Name;
        public Cluster() {}
        public Cluster(String name, Vector2 position)
        {
            Position = position;
            this.Name = name;
        }
    }
}