using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Supreme_Commander_Thorn
{
    public class Star
    {
        #region Variables
        [XmlIgnore]
        public List<Planet> Planets = new();
        public Vector2 Position { get; set; }
        public String Name;
        #endregion
        public Star() {}
        public Star(String name, Vector2 Position)
        {
            this.Position = Position;
            this.Name = name;
        }
    }
}