using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PingPong.Core.Commands;
using PingPong.Core.Entities.Abstractions;
using PingPong.Core.Managers.Abstractions;
using PingPong.Core.Providers;
using Rectangle = PingPong.Core.Entities.Primitives.Rectangle;

namespace PingPong.Core.Entities
{
	public class Player : Rectangle
    {
        private readonly MoveCommand<Player> _moveCommand;
        private readonly ICommandManager _commandManager;

        public Player() : base(ServiceProvider.Instance.Resolve<SpriteBatch>(), Constants.EntityIds.PlayerId)
        {
            _moveCommand = new MoveCommand<Player>(this, 3);
            _commandManager = ServiceProvider.Instance.Resolve<ICommandManager>();

            Color = Color.Black;
            Height = 100;
            Width = 20;

            CollisionPoints.Add(new Vector2(0, 0));
            CollisionPoints.Add(new Vector2(Width, 0));
            CollisionPoints.Add(new Vector2(0, Height));
            CollisionPoints.Add(new Vector2(Width, Height));
        }

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
			        return true;
		        }
	        }

	        return false;
        }

        public override void OnCollision(IEntity entity)
        {
            if (entity is Board)
            {
                if (Position.Y <= entity.Position.Y && _moveCommand.Direction.Y == -1)
                {
                    _moveCommand.Speed = 0;
                }
                else if (Position.Y + Height >= entity.Height && _moveCommand.Direction.Y == 1)
                {
                    _moveCommand.Speed = 0;
                }
            }
        }

        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {

                _moveCommand.Direction = new Vector2(0, -1);
                _moveCommand.Speed = 3;

                _commandManager.AddCommand(_moveCommand);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _moveCommand.Direction = new Vector2(0, 1f);
                _moveCommand.Speed = 3;

                _commandManager.AddCommand(_moveCommand);
            }
        }
    }
}