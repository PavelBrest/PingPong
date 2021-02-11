using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PingPong.Core.Entities.Abstractions
{
	public interface IEntity
    {
	    public string Id { get; }
        public int Width { get; }
        public int Height { get; }
        public Color Color { get; }
        public Vector2 Position { get; }
        IList<Vector2> CollisionPoints { get; }

        void Load();
        void Draw();
        void Update();
        void OnCollision(IEntity entity);
        bool HasCollision(IEntity entity);
    }
}
