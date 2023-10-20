using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using TEMEliminatesMonsters.KeyEvents;
using TEMEliminatesMonsters.Updateables;

namespace TEMEliminatesMonsters
{
    public class TEM : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private CameraController _cameraController;
        private OrthographicCamera _camera;
        private Fullscreener _fullscreener;

        public Texture2D _zombie;
        public KeyboardEventChecker _keyEventChecker;
        public TileMap.TileMap _map;
        public Vector2 _zombiePosition;
        public static TEM Instance { get; private set; }

        public TEM()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            IsMouseVisible = true;
            Instance = this;
        }

        public void GoFullScreen()
        {
            _fullscreener.ToggleFullscreen();
        }

        protected override void Initialize()
        {
            base.Initialize();

            _keyEventChecker = new();
            _fullscreener = new(_graphics, Window);
            _zombiePosition = new();
            BoxingViewportAdapter viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 1920, 1080);

            _camera = new OrthographicCamera(viewportAdapter);
            _cameraController = new(_camera);

            InitializeKeyEvents();
        }

        public void InitializeKeyEvents() 
        {
            KeyboardEventManager.GetEvent(Keys.F11) += GoFullScreen;
        }

        protected override void LoadContent()
        {
            _zombie = Content.Load<Texture2D>("zombie");

            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UpdateableManager.UpdateAll(gameTime);
            _zombiePosition.X += 1;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);
            var transformMatrix = _camera.GetViewMatrix();
            _spriteBatch.Begin(transformMatrix: transformMatrix);
            _spriteBatch.DrawCircle(new(new(), 5f), 64, Color.Black, 1);
            _spriteBatch.Draw(_zombie, _zombiePosition, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}