using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PingPong.Core.Entities.Abstractions;
using PingPong.Core.Utils;

namespace PingPong.Core.Entities.Primitives
{
	public class Rectangle : Entity
    {
        private Texture2D _texture;

        public Rectangle(SpriteBatch spriteBatch) : base(spriteBatch)
        { }

        public Rectangle(SpriteBatch spriteBatch, string id) : base(spriteBatch, id)
        {
        }

        public override void Load()
        {
            _texture = new Texture2D(_spriteBatch.GraphicsDevice, Width, Height);
            var length = Width * Height;

            var data = length <= 1024
                ? stackalloc Color[length]
                : new Color[length];

            data.Fill(this.Color);
            _texture.SetData(data.ToArray());
        }

        public override void Draw()
        {
            using var _ = _spriteBatch.BeginScope();

            _spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}
