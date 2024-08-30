using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class BasicKeyboard
    {
        #region Variables
        public KeyboardState OldKeyboardState, NewKeyboardState;
        public List<BasicKey> PressedKeys, PreviousPressedKeys;
        #endregion

        #region Constructors
        public BasicKeyboard() 
        {
            PressedKeys = new List<BasicKey>();
            PreviousPressedKeys = new List<BasicKey>();
        }
        #endregion

        #region Methods
        public void Update()
        {
            NewKeyboardState = Keyboard.GetState();
            GetPressedKeys();
        }
        public virtual void GetPressedKeys()
        {
            bool found = false;
            PressedKeys.Clear();
            for (int i = 0; i < NewKeyboardState.GetPressedKeys().Length; i++)
            {
                PressedKeys.Add(new BasicKey(1, NewKeyboardState.GetPressedKeys()[i].ToString()));
            }
        }
        public List<Keys> GetClickedKeys()
        {
            List<Keys> keys = new();
            foreach (Keys key in NewKeyboardState.GetPressedKeys()) 
            {
                if(OldKeyboardState.IsKeyDown(key)==false)
                {
                    keys.Add(key);
                }
            }
            return keys;
        }
        public void UpdateOld()
        {
            OldKeyboardState = NewKeyboardState;

            PreviousPressedKeys = new List<BasicKey>();
            for (int i = 0; i < PressedKeys.Count; i++)
            {
                PreviousPressedKeys.Add(PressedKeys[i]);
            }
        }
        public bool GetPress(String key)
        {
            for (int i = 0; i < PressedKeys.Count; i++)
            {
                if (PressedKeys[i].Key == key)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
