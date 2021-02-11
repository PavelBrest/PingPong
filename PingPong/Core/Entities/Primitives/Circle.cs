using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PingPong.Core.Entities.Abstractions;

namespace PingPong.Core.Entities.Primitives
{
    public class Circle : Entity
    {
        private Texture2D _texture;

        public Circle(SpriteBatch spriteBatch) : base(spriteBatch)
        { }

        public Circle(SpriteBatch spriteBatch, string id) : base(spriteBatch, id)
        {
        }

        public override void Load()
        {
            _texture = new Texture2D(_spriteBatch.GraphicsDevice, Width, Height);
            var length = Width * Height;

            var data = length <= 1024
                ? stackalloc Color[length]
                : new Color[length];

            var radius = Width / 2;

            for (int i = 0; i < length; i++)
            {
                var x = i % Width;
                var y = i / Width;
                var distance = (x - radius) * (x - radius) + (y - radius) * (y - radius);

                if (distance <= radius * radius)
                {
                    data[i] = this.Color;
                }
                else
                {
                    data[i] = Color.Transparent;
                }

            }

            _texture.SetData(data.ToArray());
        }

        public override void Draw()
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture, Position, Color.White);
            _spriteBatch.End();
        }
    }
}
