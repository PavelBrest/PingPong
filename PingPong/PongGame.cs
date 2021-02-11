using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PingPong.Core.Commands;
using PingPong.Core.Entities;
using PingPong.Core.Entities.Primitives;
using PingPong.Core.Managers;
using PingPong.Core.Managers.Abstractions;
using PingPong.Core.Providers;
using PingPong.Core.Providers.Abstraction;
using PingPong.Core.TriggerEventHandlers;

namespace PingPong
{
	public class PongGame : Game
    {
	    private readonly IServiceProvider _serviceProvider;

        private IEntityManager _entityManager;
        private ICommandManager _commandManager;
        private GraphicsDeviceManager _graphicsDeviceManager;

        private Player _player;
        private SpriteBatch _spriteBatch;

        public PongGame()
        {
	        _serviceProvider = ServiceProvider.Instance;

	        _serviceProvider.Register(Window);

            _serviceProvider.Register<PlayerTriggerHandler>();
            _serviceProvider.Register<OpponentTriggerHandler>();

	        _serviceProvider.Register<IEntityManager, EntityManager>();
	        _serviceProvider.Register<ICommandManager, CommandManager>();
            _serviceProvider.Register(new GraphicsDeviceManager(this));
            _serviceProvider.Register(Content);

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _serviceProvider.Register(_spriteBatch);

            _entityManager = _serviceProvider.Resolve<IEntityManager>();
            _commandManager = _serviceProvider.Resolve<ICommandManager>();
            _graphicsDeviceManager = _serviceProvider.Resolve<GraphicsDeviceManager>();

            AddBall();
            AddScore();
            AddBoard();
            AddPlayer();
            AddOpponent();
            AddPlayerTrigger();
            AddOpponentTrigger();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            foreach (var entity in _entityManager.GetEntities())
                entity.Load();
        }

        protected override void Update(GameTime gameTime)
        {
            _entityManager.Update();
            _entityManager.CheckCollisions();

            _commandManager.ExecuteCommands();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphicsDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (var entity in _entityManager.GetEntities()) 
                entity.Draw();

            base.Draw(gameTime);
        }

        private void AddPlayerTrigger()
        {
	        var trigger = new Trigger(Constants.TriggerIds.PlayerId)
	        {
		        Position = new Vector2(Window.ClientBounds.Width - 4, 0),
		        Width = 4,
		        Height = Window.ClientBounds.Height,
		        Color = Color.Red,
		        TriggerHandler = _serviceProvider.Resolve<PlayerTriggerHandler>()
	        };

	        _entityManager.AddEntity(trigger);
        }

        private void AddOpponentTrigger()
        {
	        var trigger = new Trigger(Constants.TriggerIds.OpponentId)
	        {
		        Position = new Vector2(0, 0),
		        Width = 4,
		        Height = Window.ClientBounds.Height,
		        Color = Color.Red,
		        TriggerHandler = _serviceProvider.Resolve<OpponentTriggerHandler>()
	        };

	        _entityManager.AddEntity(trigger);
        }

        private void AddBall()
        {
	        var ball = new Ball
	        {
		        Width = 20,
		        Height = 20,
		        Position = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2),
		        Color = Color.Black
	        };

            _entityManager.AddEntity(ball);
        }

        private void AddBoard()
        {
	        var border = new Board
	        {
		        Height = Window.ClientBounds.Height - 1,
		        Width = Window.ClientBounds.Width - 1,
		        Position = Vector2.Zero
	        };

	        _entityManager.AddEntity(border);
        }

        private void AddScore()
        {
	        var scoreEntity = new TextBox(Constants.EntityIds.ScoreId)
	        {
		        Color = Color.Black,
		        Position = new Vector2(Window.ClientBounds.Width / 2, 20)
	        };

	        _entityManager.AddEntity(scoreEntity);
        }

        private void AddOpponent()
        {
	        var opponent = new Opponent();
	        opponent.Position = new Vector2(2, Window.ClientBounds.Height / 2);

	        _entityManager.AddEntity(opponent);
        }

        private void AddPlayer()
        {
	        _player = new Player();
	        _player.Position = new Vector2(Window.ClientBounds.Width - _player.Width - 2, Window.ClientBounds.Height / 2);

	        _entityManager.AddEntity(_player);
        }
    }
}