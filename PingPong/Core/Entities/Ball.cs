using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PingPong.Core.Commands;
using PingPong.Core.Entities.Abstractions;
using PingPong.Core.Entities.Primitives;
using PingPong.Core.Managers.Abstractions;
using PingPong.Core.Providers;

namespace PingPong.Core.Entities
{
	public class Ball : Circle
    {
        private readonly MoveCommand<Ball> _moveCommand;
        private readonly ICommandManager _commandManager;

        public Ball() : base(ServiceProvider.Instance.Resolve<SpriteBatch>(), Constants.EntityIds.BallId)
        {
            _moveCommand = new MoveCommand<Ball>(this, 4, new Vector2(1f, 0.15f));
            _commandManager = ServiceProvider.Instance.Resolve<ICommandManager>();
        }

        public override void Load()
        {
	        base.Load();

	        CollisionPoints.Add(new Vector2(0, 0));
            CollisionPoints.Add(new Vector2(Width, 0));
            CollisionPoints.Add(new Vector2(0, Height));
            CollisionPoints.Add(new Vector2(Width, Height));
        }

        public override void OnCollision(IEntity entity)
        {
            var direction = _moveCommand.Direction;
            var normal = Vector2.Zero;

            if (entity is Board)
            {
                if (Position.Y <= entity.Position.Y)
                    normal = new Vector2(0, 1);
                else if (Position.Y + Height >= entity.Height)
                    normal = new Vector2(0, -1);
                else if (Position.X <= entity.Position.X)
                    normal = new Vector2(1, 0);
                else if (Position.X + Width >= entity.Width)
                    normal = new Vector2(-1, 0);

                _moveCommand.Direction = Vector2.Reflect(direction, normal);
            }
            else if (entity is Player || entity is Opponent)
            {
                _moveCommand.Direction = Vector2.Reflect(direction, Vector2.UnitX);
                _moveCommand.Speed += 0.2f;
            }
        }

        public override void Update()
        {
            _commandManager.AddCommand(_moveCommand);
        }
    }
}
