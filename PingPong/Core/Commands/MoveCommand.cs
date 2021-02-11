using Microsoft.Xna.Framework;
using PingPong.Core.Commands.Abstractions;
using PingPong.Core.Entities.Abstractions;

namespace PingPong.Core.Commands
{
    public class MoveCommand<TEntity> : EntityCommand<TEntity>
        where TEntity : Entity
    {
        public MoveCommand(TEntity entity) : base(entity)
        {
        }

        public MoveCommand(TEntity entity, float speed) : base(entity)
        {
            Speed = speed;
        }

        public MoveCommand(TEntity entity, float speed, Vector2 direction) : base(entity)
        {
            Direction = direction;
            Speed = speed;
        }

        public float Speed { get; set; }

        public Vector2 Direction { get; set; }

        public override void Execute()
        {
            var (x, y) = _entity.Position;

            _entity.Position += new Vector2(Direction.X * Speed, Direction.Y * Speed);
        }
    }
}
