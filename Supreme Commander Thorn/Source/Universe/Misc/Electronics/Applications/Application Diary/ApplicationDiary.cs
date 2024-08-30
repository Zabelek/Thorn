using Microsoft.Xna.Framework;
using Supreme_Commander_Thorn.Source;
using Supreme_Commander_Thorn.Source.Engine.Utilities;
using Supreme_Commander_Thorn.Source.Universe.Misc.Electronics.Applications.Application_Diary;
using System.Collections.Generic;

namespace Supreme_Commander_Thorn
{
    public class ApplicationDiary : ComputerApplication
    {
        private List<Note> _notes = new();
        private Note _currentNote;
        private TextInputField _leftPage, _rightPage;
        private BasicTextSprite _dateLabel;
        private BasicButton _daysArrowBack, _daysArrowFoward;
        private SwitchButton _dateSwitch, _noteSwitch;

        public ApplicationDiary() : base()
        {
            this.AddChild(new ComputerTextButton("Exit", new Vector2(30, 860), ExitDiary, null));
            this.AddChild(new ComputerTextButton("Save All", new Vector2(100, 860), SaveAllNotes, null));
            this.AddChild(new ComputerTextButton("Save Current", new Vector2(207, 860), SaveCurrentNote, null));
            this.AddChild(new ComputerTextButton("Reload", new Vector2(360, 860), LoadAllNotes, null));
            this.AddChild(new ComputerEmptyBox(new Vector2(30, 70), new Vector2(690, 770)));
            this.AddChild(new ComputerEmptyBox(new Vector2(750, 70), new Vector2(690, 770)));
            this.AddChild(new ComputerEmptyBox(new Vector2(590, 15), new Vector2(290, 40)));
            _leftPage = new TextInputField(new Vector2(35, 75), new Vector2(680, 760));
            _rightPage = new TextInputField(new Vector2(755, 75), new Vector2(680, 760));
            _dateLabel = new BasicTextSprite("01.01.3801", new Vector2(673, 22), Globals.NotebookInterfaceColor, Globals.BiggerInterfaceFont);
            this.AddChild(_leftPage);
            this.AddChild(_rightPage);
            this.AddChild(_dateLabel);
            _daysArrowBack = new BasicButton("Content/graphics/Interface/Notebook/arrow_left.png", new Vector2(550, 25), new Vector2(20, 20), daysArrowBack_Click, null);
            _daysArrowFoward = new BasicButton("Content/graphics/Interface/Notebook/arrow_right.png", new Vector2(900, 25), new Vector2(20, 20), daysArrowFoward_Click, null);
            _daysArrowBack.DisplayColor = Globals.NotebookInterfaceColor;
            _daysArrowBack.HoverColor = Globals.NotebookInterfaceColorHover;
            _daysArrowBack.ClickedColor = Globals.NotebookInterfaceColorClicked;
            _daysArrowFoward.DisplayColor = Globals.NotebookInterfaceColor;
            _daysArrowFoward.HoverColor = Globals.NotebookInterfaceColorHover;
            _daysArrowFoward.ClickedColor = Globals.NotebookInterfaceColorClicked;
            this.AddChild(_daysArrowBack);
            this.AddChild(_daysArrowFoward);
            _noteSwitch = new SwitchButton("Notes only", new Vector2(35, 25), NoteSwitchPress, null);
            _dateSwitch = new SwitchButton("All dates", new Vector2(170, 25), DateSwitchPress, null);
            this.AddChild(_noteSwitch);
            this.AddChild(_dateSwitch);
            _noteSwitch.toggle = true;
        }
        private void NoteSwitchPress(object info)
        {
            _noteSwitch.toggle = true;
            _dateSwitch.toggle = false;
        }
        private void DateSwitchPress(object info)
        {
            _noteSwitch.toggle = false;
            _dateSwitch.toggle = true;
        }
        public override void Show()
        {
            base.Show();
            LoadAllNotes(null);
        }
        private void ExitDiary(object info)
        {
            OperationSystem.loadApplication("core");
        }
        private void LoadAllNotes(object info)
        {

        }
        private void SaveAllNotes(object info)
        {

        }
        private void SaveCurrentNote(object info)
        {

        }
        private void daysArrowBack_Click(object info)
        {

        }
        private void daysArrowFoward_Click(object info)
        {

        }
    }
}