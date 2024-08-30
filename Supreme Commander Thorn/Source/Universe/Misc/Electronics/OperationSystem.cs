using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Supreme_Commander_Thorn
{
    public class OperationSystem
    {
        private List<ComputerApplication> _apps = new();
        private BasicSprite _testSprite;
        private ElectronicDevice _device;
        public List<ComputerFile> Files;
        public bool ShutDownSignal = false;
        public ComputerApplication CurrentApplication;
        public bool ApplicationChanged = false;
        public OperationSystem(ElectronicDevice device) {
            _device = device;
            List<ComputerApplication> Apps = new();
            _testSprite = new BasicSprite("Content/graphics/Interface/Notebook/Interface_Element_To_Stretch.png",new Vector2(0,0), device.ScreenSize);
            Files = new();
        }
        public void InstallApplication(string name)
        {
            if(String.Compare(name, "Core")==0)
            {
                var app = new ApplicationCore();
                _apps.Add(app);
                CurrentApplication = app;
            }
            else if(String.Compare(name, "Diary") == 0)
            {
                _apps.Add(new ApplicationDiary());
            }
            if(_apps.Count > 0)
                _apps.Last().OperationSystem = this;
        }
        public List<ComputerApplication> GetApplications()
        {
            return _apps;
        }
        public void loadApplication(String  name)
        {
            ApplicationChanged = true;
            if (String.Compare(name, "diary") == 0)
            {
                foreach (ComputerApplication app in _apps)
                    if (app is ApplicationDiary)
                    {
                        CurrentApplication?.Parent?.RemoveChild(CurrentApplication);
                        CurrentApplication = app;
                        break;
                    }
            }
            else if (String.Compare(name, "core") == 0)
                foreach (ComputerApplication appl in _apps)
                    if (appl is ApplicationCore)
                    {
                         CurrentApplication?.Parent?.RemoveChild(CurrentApplication);
                         CurrentApplication = appl;
                        break;
                    }
        }
        public ComputerFile OpenFile(String path)
        {
            foreach(ComputerFile file in Files) {
                if(String.Compare(file.Path, path)==0)
                {
                    return file;
                }
            }
            return null;
        }
        public void SaveFile(String path, String content)
        {
            foreach (ComputerFile file in Files)
            {
                if (String.Compare(file.Path, path) == 0)
                {
                    file.Content = content;
                    break;
                }
            }
        }
        public void CreateFile(String path, String content)
        {
            bool fileExists = false;
            foreach (ComputerFile file in Files)
            {
                if (String.Compare(file.Path, path) == 0)
                {
                    fileExists = true;
                    file.Content = content;
                    break;
                }
            }
            if(!fileExists)
            {
                Files.Add(new ComputerFile(path, content));
            }
        }
        public void DeleteFile(String path)
        {
            foreach (ComputerFile file in Files)
            {
                if (String.Compare(file.Path, path) == 0)
                {
                    Files.Remove(file);
                    break;
                }
            }
        }
        public void Update()
        {
            foreach (ComputerApplication app in _apps)
            {
                if(app.IsRunning)
                    app.Update();
            }
        }
    }
}