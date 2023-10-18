using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;
using TEMEliminatesMonsters.TileMap;

namespace TEMEliminatesMonsters
{
    public class TEM : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private CameraController _cameraController;
        private OrthographicCamera _camera;
        public Texture2D _zombie;

        public TileMap.TileMap _map;

        public Vector2 _zombiePosition;

        public static TEM Instance { get; private set; }

        public TEM()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //_map = new(10,10);
            Instance = this;
        }

        protected override void Initialize()
        {
            base.Initialize();

            _zombiePosition = new();

            BoxingViewportAdapter viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 480);
            
            _camera = new OrthographicCamera(viewportAdapter);
            _cameraController = new(_camera);
        }
        protected override void LoadContent()
        {
            _zombie = Content.Load<Texture2D>("zombie");
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _cameraController.Update(gameTime);
            _zombiePosition.X += 1;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);
            var transformMatrix = _camera.GetViewMatrix();
            _spriteBatch.Begin(transformMatrix: transformMatrix);
            //foreach (Tile tile in _map._tileMap) 
            //{
            //    _spriteBatch.Draw(tile._texture, tile._position, Color.White);
            //}
            _spriteBatch.DrawCircle(new(new(), 5f),64,Color.Black, 1);
            _spriteBatch.Draw(_zombie, _zombiePosition , Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}