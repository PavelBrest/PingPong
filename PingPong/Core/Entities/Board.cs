using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PingPong.Core.Commands;
using PingPong.Core.Entities.Abstractions;
using PingPong.Core.Managers.Abstractions;
using PingPong.Core.Providers;
using Rectangle = PingPong.Core.Entities.Primitives.Rectangle;

namespace PingPong.Core.Entities
{
	public class Board : Rectangle
    {
	    private readonly UpdateScoreCommand _updateScoreCommand;
        private readonly ICommandManager _commandManager = ServiceProvider.Instance.Resolve<ICommandManager>();
        private readonly IEntityManager _entityManager = ServiceProvider.Instance.Resolve<IEntityManager>();

        public Board() : base(ServiceProvider.Instance.Resolve<SpriteBatch>())
        {
            Color = Color.Transparent;
            _updateScoreCommand = new UpdateScoreCommand((ScoreTextBox)_entityManager.GetEntity(Constants.EntityIds.ScoreId));
        }

        public override void Draw() { }

        public override void Load() { }

        public override void OnCollision(IEntity entity)
        {
	        if (!(entity is Ball)) 
		        return;

	        if (entity.Position.X + entity.Width >= Width)
	        {
		        _updateScoreCommand.IncrementPlayerScore = false;
		        _commandManager.AddCommand(_updateScoreCommand);
	        }
	        else if (entity.Position.Y <= 0)
	        {
		        _updateScoreCommand.IncrementPlayerScore = true;
		        _commandManager.AddCommand(_updateScoreCommand);
	        }
        }

        public override bool HasCollision(IEntity entity)
        {
            return entity.Position.X <= 0 || entity.Position.X + entity.Width >= Width ||
                entity.Position.Y <= 0 || entity.Position.Y + entity.Height >= Height;
        }
    }
}
