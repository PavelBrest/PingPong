using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace PingPong.Core.Entities.Abstractions
{
	public abstract class Entity : IEntity
    {
        protected readonly SpriteBatch _spriteBatch;

        protected Entity(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch ?? throw new ArgumentNullException(nameof(spriteBatch));
            Id = Guid.NewGuid().ToString();
        }

        protected Entity(SpriteBatch spriteBatch, string id)
        {
	        _spriteBatch = spriteBatch ?? throw new ArgumentNullException(nameof(spriteBatch));
	        Id = id;
        }

        public string Id { get; protected set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }
        public virtual Vector2 Position { get; set; }
        public IList<Vector2> CollisionPoints { get; } = new List<Vector2>();


        public virtual void Load() { }

        public virtual void Draw() { }

        public virtual void Update() { }

        public virtual bool HasCollision(IEntity entity) => false;

        public virtual void OnCollision(IEntity entity) { }
    }
}