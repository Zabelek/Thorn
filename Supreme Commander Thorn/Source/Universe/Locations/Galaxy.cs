using System.Collections.Generic;
using System.Xml.Serialization;

namespace Supreme_Commander_Thorn
{
    public class Galaxy
    {
        [XmlIgnore]
        public List<Cluster> Clusters = new();
        public string Name;
        public Galaxy() {}
        public Galaxy(string name)
        {
            this.Name = name;
        }
    }
}