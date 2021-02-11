using Microsoft.Xna.Framework.Graphics;
using PingPong.Core.Entities.Abstractions;
using PingPong.Core.Providers;
using PingPong.Core.TriggerEventHandlers;
using System;

namespace PingPong.Core.Entities.Primitives
{
	public class Trigger : Rectangle
	{
		public Trigger(string id) : base(ServiceProvider.Instance.Resolve<SpriteBatch>(), id)
		{
		}

		public ITriggerHandler TriggerHandler { get; set; }

		public event EventHandler<IEntity> InTrigger;

		public override bool HasCollision(IEntity entity)
		{
			if (entity.CollisionPoints.Count == 0)
				return false;

			foreach ((float x, float y) in entity.CollisionPoints)
			{
				var collisionPositionX = x + entity.Position.X;
				var collisionPositionY = y + entity.Position.Y;

				if (Position.X <= collisionPositionX && collisionPositionX < Position.X + Width &&
				    Position.Y <= collisionPositionY && collisionPositionY < Position.Y + Height)
				{
					TriggerHandler?.Handle(this, entity);
					InTrigger?.Invoke(this, entity);

					return true;
				}
			}

			return false;
		}
	}
}
