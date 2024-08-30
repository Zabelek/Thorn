using Supreme_Commander_Thorn.Source.Engine.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class ViewLayer : ISpriteParent
    {
        #region Variables
        private BasicCamera _camera;
        public List<BasicSprite> Sprites;
        public List<Actor> Actors;
        public bool InputAllowed;
        public List<ISpriteParent> OverlyingSpriteParents = new();
        public bool IsHidden;

        #endregion

        #region Constructors
        public ViewLayer(BasicCamera camera)
        {
            Sprites = new List<BasicSprite>();
            Actors = new List<Actor>();
            this._camera = camera;
            InputAllowed = true;
            IsHidden = false;
        }
        #endregion

        #region Methods
        public void AddChild(BasicSprite sprite)
        {
            Sprites.Add(sprite);
            var actor = sprite as Actor;
            if (actor != null)
            {
                if (actor.Parent != null)
                    actor.Parent.RemoveChild(actor);
                Actors.Add((Actor)actor);
                actor.Parent = this;
            }
        }
        public void RemoveChild(BasicSprite sprite)
        {
            Sprites.Remove(sprite);
            var actor = sprite as Actor;
            if (actor != null)
            {
                Actors.Remove((Actor)actor);
                actor.Parent = null;
            }
        }
        public void Clear()
        {
            foreach (Actor actor in Actors)
            {
                actor.Parent = null;
            }
            Sprites.Clear();
            Actors.Clear();
        }
        public virtual void Show() 
        { 
            IsHidden = false; 
            foreach(BasicSprite sprite in Sprites)
            {
                sprite.Show();
            }
        }
        public virtual void Hide() {
            IsHidden = true;
            foreach (BasicSprite sprite in Sprites)
            {
                sprite.Hide();
            }
        }
        public virtual void Update()
        {
            _camera.UpdateCamera();
            if(!IsHidden)
            {
                foreach (BasicSprite sprite in Sprites)
                {
                    sprite.Update();
                }
                if (InputAllowed)
                {
                    List<Actor> virtualActors = new List<Actor>();
                    foreach (Actor actor in Actors)
                    {
                        actor.IsOverlaid = false;
                        virtualActors.Add(actor);
                    }
                    //check overlay with other layers
                    bool flagCheckOverlayInternally = true;
                    if (OverlyingSpriteParents.Count > 0)
                    {
                        foreach (ISpriteParent actorPar in OverlyingSpriteParents)
                            foreach (Actor actor in actorPar.GetChildActors())
                                if (actor.Hover(_camera))
                                {
                                    flagCheckOverlayInternally = false;
                                    break;
                                }
                    }
                    //if no layer overlays, check it internally
                    if (flagCheckOverlayInternally)
                    {
                        Actor virtualActor = null;
                        foreach (Actor actor in virtualActors)
                            if (actor.Hover(_camera))
                            {
                                if (virtualActor != null)
                                    virtualActor.IsOverlaid = true;
                                virtualActor = actor;
                            }
                    }
                    else
                        foreach (Actor actor in virtualActors)
                            actor.IsOverlaid = true;
                    foreach (Actor actor in virtualActors)
                        actor.Update(_camera);
                }
            }         
        }
        public virtual void Draw()
        {
            if(!IsHidden)
            {
                foreach (BasicSprite sprite in Sprites)
                {
                    sprite.Draw(_camera);
                }
            }
        }

        public List<Actor> GetChildActors()
        {
            return Actors;
        }
        #endregion
    }
}
