using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using Supreme_Commander_Thorn.Source.Engine.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class WidgetGroup : Actor, ISpriteParent
    {
        #region Variables
        public List<BasicSprite> Sprites;
        public List<Actor> Actors;
        public bool InputAllowed;
        public bool isHidden { get;private set; }
        #endregion

        #region Constructors
        public WidgetGroup()
        { 
            Sprites = new List<BasicSprite>();
            Actors = new List<Actor>();
            isHidden = false;
            InputAllowed = true;
            Pos = new Vector2(0, 0);
        }
        #endregion

        #region Methods
        public override void Show()
        {
            isHidden = false;
            InputAllowed = true;
        }
        public override void Hide()
        {
            isHidden = true;
            InputAllowed = false;
        }
        public void AddChild(BasicSprite sprite)
        {
            Sprites.Add(sprite);
            var actor = sprite as Actor;
            if (actor != null)
            {
                actor.Parent?.RemoveChild(actor);
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
            Sprites.Clear();
            Actors.Clear();
        }
        public override void Update(Vector2 offset, float zoom)
        {
            foreach (BasicSprite sprite in Sprites)
            {
                sprite.Update();
            }
            if (InputAllowed && !this.IsOverlaid)
            {
                List<Actor> virtualActors = new List<Actor>();
                foreach (Actor actor in Actors)
                {
                    actor.IsOverlaid = false;
                    virtualActors.Add(actor);
                }
                //checkOverlay
                Actor virtualActor = null;
                foreach (Actor actor in virtualActors)
                {
                    if (actor.Hover(new Vector2(offset.X+this.Pos.X, offset.Y+this.Pos.Y), zoom))
                    {
                        if (virtualActor != null)
                        {
                            virtualActor.IsOverlaid = true;
                        }
                        virtualActor = actor;
                    }
                }
                foreach (Actor actor in virtualActors)
                {
                    actor.Update(new Vector2(offset.X + this.Pos.X, offset.Y + this.Pos.Y), zoom);
                }
            }
        }
        public override void Update(BasicCamera camera)
        {
            Update(camera.Position, camera.Zoom);
        }
        public override bool Hover(Vector2 offset, float zoom)
        {
            foreach (Actor actor in Actors)
            {
                if(actor.Hover(offset, zoom))
                {
                    return true;
                }
            }
            return base.Hover(offset, zoom);
        }
        public override void Draw(BasicCamera camera)
        {
            if(!isHidden)
            {
                foreach(BasicSprite sprite in Sprites)
                {
                    sprite.Draw(new Vector2(camera.Position.X + this.Pos.X, camera.Position.Y + this.Pos.Y), camera.Zoom);
                }
            }
        }
        public override void Draw(Vector2 offset, float zoom)
        {
            if (!isHidden)
            {
                foreach (BasicSprite sprite in Sprites)
                {
                    sprite.Draw(new Vector2(offset.X + this.Pos.X, offset.Y + this.Pos.Y), zoom);
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
