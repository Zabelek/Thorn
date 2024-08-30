using Microsoft.VisualBasic.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Supreme_Commander_Thorn
{
    public class UsableObject
    {
        #region Variables
        [XmlIgnore]
        public Gui NestedGui;
        [XmlIgnore]
        public CustomBoundsButton ObjectButton;
        [XmlIgnore]
        public string MainDirectory;
        public Vector2 Pos, Dims;
        public String Name;
        #endregion

        #region Constructors
        public UsableObject() { Universe.RegisteredUsableObjects.Add(this); }

        public UsableObject(string name, string mainDirectory, Vector2 pos, Vector2 dims)
        {
            this.Pos = pos;
            this.Dims = dims;
            this.Name = name;
            this.MainDirectory = mainDirectory;
            this.ObjectButton = new CustomBoundsButton(mainDirectory+ "Image_Day.png", pos, dims, ShowOptions, null);
        }
        #endregion

        #region Methods
        public virtual void ShowOptions(Object info)
        {
            List<InterfaceButton> buttons = new List<InterfaceButton>();
            buttons.Add(new InterfaceButton("Inspect", new Vector2(1615, 120), 230, ShowInspectWindow, null));
            NestedGui.ShowInterface();
            NestedGui.ShowAdditionalOptions(buttons);
        }
        public virtual void Update() { }
        public void ShowInspectWindow(Object info)
        {
            this.NestedGui.showInspectUsableObjectWindow(this);
        }
        public void ChangeToDayVersion()
        {
            if (ObjectButton != null)
            {
                ObjectButton.Tex.Dispose();
                ObjectButton = new CustomBoundsButton(MainDirectory + "\\Image_Day.png", Pos, Dims, ShowOptions, null);
            }
        }
        public void ChangeToNightVersion()
        {
            try
            {
                if (ObjectButton != null)
                {
                    ObjectButton.Tex.Dispose();
                    ObjectButton = new CustomBoundsButton(MainDirectory + "\\Image_Night.png", Pos, Dims, ShowOptions, null);
                }
            }
            catch(IOException ex)
            {
                ChangeToDayVersion();
            }
        }
        #endregion
    }
}
