﻿using Sphere = Microsoft.Xna.Framework.BoundingSphere;

namespace EquestriEngine.Data.Scenes
{
    public struct BoundingSphere
    {
        private Sphere _sphere;

        public float Radius
        {
            get { return _sphere.Radius; }
            set { _sphere.Radius = value; }
        }

        public Vector3 Center
        {
            get { return _sphere.Center; }
            set { _sphere.Center = value; }
        }

        public BoundingSphere(Vector3 center, float radius)
        {
            _sphere = new Sphere(center, radius);
            
        }

        public bool Intersects(BoundingSphere sphere)
        {
            return _sphere.Intersects(sphere);
        }

        public bool Intersects(BoundingBox box)
        {
            return _sphere.Intersects(box);
        }

        public static implicit operator Sphere(BoundingSphere sphere)
        {
            return sphere._sphere;
        }

        public static implicit operator BoundingSphere(Sphere sphere)
        {
            return sphere;
        }
    }
}
