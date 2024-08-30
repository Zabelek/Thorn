using Microsoft.Xna.Framework;

namespace Supreme_Commander_Thorn
{
    public class ComputerApplication : WidgetGroup
    {
        protected ViewLayer _parentViewLayer;
        public bool IsRunning;
        public OperationSystem OperationSystem;

        public ComputerApplication() : base()
        {
            IsRunning = false;
        }
        public virtual void Shutdown()
        {
            IsRunning = false;
            Hide();
        }
        public virtual void Run(ViewLayer parentLayer)
        {
            IsRunning = true;
            Show();
            _parentViewLayer = parentLayer;
            parentLayer.AddChild(this);
        }
        public void Restart()
        {
            Shutdown();
            if(_parentViewLayer != null)
                Run(_parentViewLayer);
        }
    }
}