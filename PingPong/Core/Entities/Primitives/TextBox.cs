using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PingPong.Core.Entities.Abstractions;
using PingPong.Core.Providers;
using PingPong.Core.Utils;

namespace PingPong.Core.Entities.Primitives
{
	public class TextBox : Entity
	{
		private readonly ContentManager _contentManager = ServiceProvider.Instance.Resolve<ContentManager>();

		private SpriteFont _spriteFont;

		public TextBox() : base(ServiceProvider.Instance.Resolve<SpriteBatch>())
		{
		}

		public TextBox(string id) : base(ServiceProvider.Instance.Resolve<SpriteBatch>(), id)
		{
		}

		public string Text { get; set; }

		public override void Load()
		{
			_spriteFont = _contentManager.Load<SpriteFont>("SimpleText");
		}

		public override void Draw()
		{
			using var _ = _spriteBatch.BeginScope();

			var position = Position;
			position.X -= _spriteFont.Texture.Width / 2;

			_spriteBatch.DrawString(_spriteFont, Text, position, Color);
		}
	}
}
