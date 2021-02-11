using System;
using PingPong.Core.Commands.Abstractions;
using PingPong.Core.Managers.Abstractions;
using PingPong.Core.Providers;

namespace PingPong.Core.Commands
{
	public class FreezeCommand : ICommand
	{
		private readonly ICommandManager _commandManager = ServiceProvider.Instance.Resolve<ICommandManager>();

		private readonly double _seconds;

		private DateTime _freezeTime;

		public FreezeCommand(double seconds)
		{
			_seconds = seconds;
		}

		public void Execute()
		{
			if (_freezeTime == default)
				_freezeTime = DateTime.Now.AddSeconds(_seconds);

			if (DateTime.Now < _freezeTime)
			{
				_commandManager.ClearQueue();
				_commandManager.AddCommand(this);
			}
		}
	}
}