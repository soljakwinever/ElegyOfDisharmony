﻿using EquestriEngine.Data.Scenes;

namespace EquestriEngine.GameData.Map.Character
{
    public abstract class Character
    {
        public virtual Vector2 Postion
        {
            get { return Vector2.Zero; }
            set { }
        }

        public abstract void Update(float dt);
    }
}