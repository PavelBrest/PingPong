using PingPong.Core.Commands.Abstractions;
using PingPong.Core.Entities.Primitives;

namespace PingPong.Core.Commands
{
	public class UpdateScoreCommand : EntityCommand<TextBox>
	{
		public UpdateScoreCommand(TextBox entity) : base(entity)
		{
		}

		public bool IncrementPlayerScore { get; set; }

		public override void Execute()
		{
			if (IncrementPlayerScore)
				_entity.Text = "";
			else
				_entity.OpponentScore++;
		}
	}
}
