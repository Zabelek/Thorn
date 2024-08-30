using Microsoft.Xna.Framework;
using Supreme_Commander_Thorn.Source;
using Supreme_Commander_Thorn.Source.Engine.Utilities;
using System;
using System.Diagnostics;

namespace Supreme_Commander_Thorn
{
    public class ApplicationCore : ComputerApplication
    {
        private BasicTextSprite _firstPrompt;
        private BasicTimer _timer;
        private bool _startedBooting, _finishedBooting;
        private ComputerCommandPrompt _inputBox;

        public ApplicationCore() : base()
        {
            _startedBooting = false;
            _finishedBooting = false;
            _inputBox = new ComputerCommandPrompt(new Vector2(0, 40));
            _inputBox.Selected = false;
            _inputBox.IsHidden = true;
            _firstPrompt = new BasicTextSprite("", new Vector2(0, 0), Globals.NotebookInterfaceColor, Globals.SmallerInterfaceFont);
            _timer = new BasicTimer(1000);
            this.AddChild(_firstPrompt);
            this.AddChild(_inputBox);
        }
        public override void Update()
        {
            base.Update();
            _timer.UpdateTimer();
            if (_timer.Test())
            {
                if (!_startedBooting)
                {
                    _firstPrompt.SetDescription(">>Initiating biometric scanning, please wait...");
                    _startedBooting = true;
                }
                else if (!_finishedBooting)
                {
                    _firstPrompt.SetDescription(_firstPrompt.Description + "\n>>Biometric scan finished. Welcome back, Zack!");
                    _finishedBooting = true;
                    _inputBox.Selected = true;
                    _inputBox.IsHidden = false;
                }
                _timer.Reset();
            }
            if(_inputBox.EnterClicked)
            {
                _inputBox.EnterClicked=false;
                if (String.Compare(_inputBox.CurrentText.ToLower(), "exit") == 0)
                {
                    Shutdown();
                    OperationSystem.ShutDownSignal = true;
                }
                else if (String.Compare(_inputBox.CurrentText.ToLower(), "diary") == 0)
                {
                    OperationSystem.loadApplication("diary");
                }
                _inputBox.SetCurrentText("");
            }
        }
        public override void Update(BasicCamera camera)
        {
            base.Update(camera);
        }
        public override void Run(ViewLayer parentLayer)
        {
            base.Run(parentLayer);
            _firstPrompt.SetDescription("");
            _startedBooting = false;
            _finishedBooting = false;
            _timer.Reset();
        }
    }
}