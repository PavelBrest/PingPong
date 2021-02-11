using PingPong.Core.Commands;
using PingPong.Core.Entities;
using PingPong.Core.Entities.Abstractions;
using PingPong.Core.Entities.Primitives;
using PingPong.Core.Managers.Abstractions;
using PingPong.Core.Providers;

namespace PingPong.Core.TriggerEventHandlers
{
	public class OpponentTriggerHandler : ITriggerHandler
	{
		private readonly ICommandManager _commandManager = ServiceProvider.Instance.Resolve<ICommandManager>();
		private readonly IEntityManager _entityManager = ServiceProvider.Instance.Resolve<IEntityManager>();
		private readonly NewRoundCommand _newRoundCommand = new NewRoundCommand();

		private UpdateScoreCommand _updateScoreCommand;

		public void Handle(Trigger trigger, IEntity entity)
		{
			if ((entity is Ball) == false)
				return;

			if (_updateScoreCommand == null)
			{
				var score = (ScoreTextBox)_entityManager.GetEntity(Constants.EntityIds.ScoreId);
				_updateScoreCommand = new UpdateScoreCommand(score)
				{
					IncrementPlayerScore = false
				};
			}

			_commandManager.ClearQueue();
			_commandManager.AddCommand(_updateScoreCommand);
			_commandManager.AddCommand(_newRoundCommand);
			//_commandManager.AddCommand(new FreezeCommand(1));
		}
	}
}