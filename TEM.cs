using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Sprites;
using System.IO;
using System;
using System.Windows.Forms;

namespace TEMEliminatesMonsters
{
    public class TEM : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private CameraController _cameraController;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private OrthographicCamera _camera;
        private Texture2D _smile;
        private Sprite _smileSprite;

        public TEM()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            BoxingViewportAdapter viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 480);
            _camera = new OrthographicCamera(viewportAdapter);
            _cameraController = new(_camera, Mouse.GetState().X, Mouse.GetState().Y);
        }
        protected override void LoadContent()
        {
            //_tiledMap = Content.Load<TiledMap>("samplemap");
            _smile = Texture2D.FromStream(_graphics.GraphicsDevice, File.Open("C:\\Users\\a.belski\\source\\repos\\TEM\\Content\\bin\\Windows\\Content\\smile.png", FileMode.Open));
            
            //_tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime) // Note, do NOT draw stuff in here
        {

            base.Update(gameTime);
            _cameraController.Update(gameTime);
            //_tiledMapRenderer.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime) // Note, do NOT move gameobjects in here 
        {
            GraphicsDevice.Clear(Color.LightBlue);
            //_tiledMapRenderer.Draw();

            var transformMatrix = _camera.GetViewMatrix();
            _spriteBatch.Begin(transformMatrix: transformMatrix);
            _spriteBatch.Draw(_smile, new Vector2(150, 150), Color.Red);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}