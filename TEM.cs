﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TEMEliminatesMonsters.KeyEvents;
using TEMEliminatesMonsters.Updateables;

namespace TEMEliminatesMonsters
{
    public class TEM : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private CameraController _cameraController;
        private OrthographicCamera _camera;
        private Fullscreener _fullscreener;
        private TileMap.TileMap _map;

        public Texture2D _zombie;
        public Vector2 _zombiePosition;
        public Dictionary<string, Texture2D> Tiles = new();
        public int _tileMapSize = 64;
       

        public static KeyboardEventChecker _keyEventChecker;

        public static TEM Instance { get; private set; }

        /// <summary>
        /// Creates the game and initializes core objects
        /// </summary>
        public TEM()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            IsMouseVisible = true;
            Instance = this;
        }

        /// <summary>
        /// Makes the game cover the whole screen
        /// </summary>
        public void GoFullScreen()
        {
            _fullscreener.ToggleFullscreen();
        }

        /// <summary>
        /// initializes non-core objects
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            _keyEventChecker = new();
            _fullscreener = new(_graphics, Window);
            _zombiePosition = new();
            BoxingViewportAdapter viewportAdapter = new(Window, GraphicsDevice, 1920, 1080);

            _camera = new OrthographicCamera(viewportAdapter);
            _cameraController = new(_camera);

            InitializeKeyEvents();

            _map = new(Tiles["Metal-1-1"], 3, _tileMapSize, _tileMapSize);
        }

        /// <summary>
        /// Adds methods to key press events 
        /// </summary>
        public void InitializeKeyEvents()
        {
            KeyboardEventManager.GetEvent(Keys.F11) += GoFullScreen;
        }

        /// <summary>
        /// loads assets
        /// </summary>
        protected override void LoadContent()
        {
            _zombie = Content.Load<Texture2D>("zombie");

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (string file in Directory.GetFiles("Content\\Tiles\\").Select(Path.GetFileNameWithoutExtension))
            {
                Debug.WriteLine(file);
                Texture2D texture = Content.Load<Texture2D>(file);
                Tiles.Add(file, texture);
            }
        }

        /// <summary>
        /// Runs every frame, updates objects
        /// </summary>
        /// <param name="gameTime">Game uptime</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UpdateableManager.UpdateAll(gameTime);
            _zombiePosition.X += 1;
        }

        /// <summary>
        /// draws/renders objects to the screen
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);
            var transformMatrix = _camera.GetViewMatrix();
            _spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);

            // render the TileMap
            _map.Render(_spriteBatch);

            //render the particles

            //render the entities
            _spriteBatch.Draw(_zombie, _zombiePosition, Color.White);

            //render the items
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}