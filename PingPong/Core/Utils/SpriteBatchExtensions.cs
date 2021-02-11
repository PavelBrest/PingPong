using System;
using Microsoft.Xna.Framework.Graphics;

namespace PingPong.Core.Utils
{
	public static class SpriteBatchExtensions
	{
		public static IDisposable BeginScope(this SpriteBatch spriteBatch)
		{
			if (spriteBatch == null) throw new ArgumentNullException(nameof(spriteBatch));

			spriteBatch.Begin();

			return new DisposableAction(spriteBatch.End);
		}
	}
}