using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spine;
using EquestriEngine.Systems;

namespace EquestriEngine.Objects
{
    public class DrawableSkeleton
    {
        private Skeleton _skeleton;
        private AnimationState _animation;
        private Atlas _atlas;
        private bool _ready;
        private string 
            _name,
            _file,
            _skin;

        public string Name
        {
            get { return _name; }
        }

        public float X
        {
            get { return _skeleton.RootBone.X; }
            set
            {
                _skeleton.RootBone.X = value;
            }
        }

        public float Y
        {
            get { return _skeleton.RootBone.X; }
            set
            {
                _skeleton.RootBone.X = value;
            }
        }

        public float Rotation
        {
            get { return _skeleton.RootBone.Rotation; }
            set
            {
                _skeleton.RootBone.Rotation = value;
            }
        }

        public float ScaleX
        {
            get { return _skeleton.RootBone.ScaleX; }
            set
            {
                _skeleton.RootBone.ScaleX = value;
            }
        }

        public float ScaleY
        {
            get { return _skeleton.RootBone.ScaleY; }
            set
            {
                _skeleton.RootBone.ScaleY = value;
            }
        }

        public bool Ready
        {
            get { return _ready; }
        }

        public DrawableSkeleton(string name, string file,string skin = "")
        {
            _ready = false;
            _name = name;
            _file = file;
            _skin = skin;
            //_name = AssetManager.AddSkeleton(_name, this);
        }

        public bool Load(Microsoft.Xna.Framework.Graphics.GraphicsDevice device)
        {
            _ready = false;
            const string 
                SKELE_PATH = @"Content\Data\",
                SKELE_EXT1 =".atlas",
                SKELE_EXT2 = ".json";
            try
            {
                _atlas = new Atlas(SKELE_PATH + _file + SKELE_EXT1, new XnaTextureLoader(device));
                SkeletonJson jsonReader = new SkeletonJson(_atlas);
                _skeleton = new Skeleton(jsonReader.ReadSkeletonData(SKELE_PATH + _file + SKELE_EXT2));
                _skeleton.SetSkin("Default");
                _ready = true;
            }
            catch
            {
                throw new Exception(string.Format("Error loading Skeleton - {0}",_name));
            }
            return true;
        }

        public void Draw(SkeletonRenderer renderer,float dt)
        {
            _animation.Update(dt);
            _animation.Apply(_skeleton);
            _skeleton.UpdateWorldTransform();

            renderer.Begin();
            renderer.Draw(_skeleton);
            renderer.End();
        }
    }
}
