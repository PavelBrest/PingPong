using Microsoft.Xna.Framework;
using PingPong.Core.Commands.Abstractions;
using PingPong.Core.Entities;
using PingPong.Core.Managers.Abstractions;
using PingPong.Core.Providers;

namespace PingPong.Core.Commands
{
	public class NewRoundCommand : ICommand
	{
		private readonly IEntityManager _entityManager = ServiceProvider.Instance.Resolve<IEntityManager>();
		private readonly GameWindow _gameWindow = ServiceProvider.Instance.Resolve<GameWindow>();

		public void Execute()
		{
			var player = (Player)_entityManager.GetEntity(Constants.EntityIds.PlayerId);
			player.Position = new Vector2(_gameWindow.ClientBounds.Width - player.Width - 2, _gameWindow.ClientBounds.Height / 2);

			var opponent = (Opponent)_entityManager.GetEntity(Constants.EntityIds.OpponentId);
			opponent.Position = new Vector2(2, _gameWindow.ClientBounds.Height / 2);

			var ball = (Ball)_entityManager.GetEntity(Constants.EntityIds.BallId);
			ball.Position = new Vector2(_gameWindow.ClientBounds.Width / 2, _gameWindow.ClientBounds.Height / 2);
		}
	}
}
