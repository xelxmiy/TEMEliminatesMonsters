using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;

namespace TEMEliminatesMonsters
{
    public class TEM : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private CameraController _cameraController;
        private OrthographicCamera _camera;
        private Texture2D _zombie;

        public static Vector2 _zombiePosition;

        public TEM()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
            //_zombiePosition.X += 1;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);
            var transformMatrix = _camera.GetViewMatrix();
            _spriteBatch.Begin(transformMatrix: transformMatrix);
            _spriteBatch.DrawCircle(new(new(), 5f),64,Color.Black, 1);
            _spriteBatch.Draw(_zombie, _zombiePosition , Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}