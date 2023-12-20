using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TEMEliminatesMonsters.src.KeyEvents;
using TEMEliminatesMonsters.src.Updateables;
using MonoGame.Extended.Entities;
using TEMEliminatesMonsters.src.Entities.ResourceNodes.Spawners;
using TEMEliminatesMonsters.src.Map;
using TEMEliminatesMonsters.src.Map.Tiles;
using TEMEliminatesMonsters.src.Controllers;
using TEMEliminatesMonsters.src.Entities.Resource_Nodes.Systems;
using System;

namespace TEMEliminatesMonsters.src
{
    public class TEM : Game
    {
        private CameraController _cameraController;
        private Fullscreener _fullscreener;
        private World _world;
        private Texture2D _zombie;
        private HuskFactory _huskFactory;
        private readonly GraphicsDeviceManager _graphics;
        private readonly int _TileMapSize = 256;

        public TileMap Map;
        public OrthographicCamera Camera;
        public Dictionary<string, Texture2D> Tiles = new();
        public SpriteBatch SpriteBatch;

        public static KeyboardEventChecker KeyEventChecker { get; private set; }
        public static TEM Instance { get; private set; }

        /// <summary>
        /// Creates the game and initializes core objects
        /// </summary>
        public TEM()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowAltF4 = true;
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

            SpriteBatch = new SpriteBatch(GraphicsDevice);

            KeyEventChecker = new();
            _fullscreener = new(_graphics, Window);
            BoxingViewportAdapter viewportAdapter = new(Window, GraphicsDevice, 1920, 1080);

            Camera = new OrthographicCamera(viewportAdapter);
            _cameraController = new(Camera);

            InitializeKeyEvents();

            Map = new(Tiles[$"{TileTexture.Metal_MiddleMiddle}"], 2, _TileMapSize, _TileMapSize);

            // doodz would absolutely kill me (he hates dots chains)
            // doodz i know you have this repo starred on github, if you see this i'm very sorry 
            _world = new WorldBuilder()
            .AddSystem(new WorldUpdateSystem())
            .AddSystem(new WorldRenderSystem())
            // .AddSystem(Isystem system) 
            .Build();

            //this won't be done like this in reality, this is just for testing
            Map.AddTile(new GroundTile(Tiles[$"{(TileTexture)13}"], 0, 0, 00100000), 1);
            Map.AddTile(new GroundTile(Tiles[$"{(TileTexture)13}"], 1, 0, 00100100), 1);
            _huskFactory = new HuskFactory(_world, _zombie, SpriteBatch);
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

            foreach (string file in Directory.GetFiles("Content\\Tiles\\").Select(Path.GetFileNameWithoutExtension))
            {
                Debug.WriteLine(file);
                string s = "Tiles\\" + file;
                Texture2D texture = Content.Load<Texture2D>(s);
                Tiles.Add(file, texture);
            }
        }

        FastRandom fr = new FastRandom();
        /// <summary>
        /// Runs every frame, updates objects
        /// </summary>
        /// <param name="gameTime">Game uptime</param>
        protected override void Update(GameTime gameTime)
        {
            _world.Update(gameTime);
            UpdateableManager.UpdateAll(gameTime);
            base.Update(gameTime);
            if (fr.Next(0, (int)Math.Pow(2, 16)) % 128 == 0)
            {
                Vector2 pos = new(fr.Next(32, 2048), fr.Next(32, 2048));
                Debug.WriteLine($"Making New Husk at {pos}");
                _huskFactory.Create(pos);
            }
        }

        /// <summary>
        /// draws/renders objects to the screen
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            //clear previously drawn stuff, if you see bright magenta, you're out of bounds!
            GraphicsDevice.Clear(Color.Magenta);

            //begin drawing
            var transformMatrix = Camera.GetViewMatrix();
            SpriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);

            // render the TileMap
            Map.Render(SpriteBatch);

            //render the particles

            //render the entities
            _world.Draw(gameTime);

            //render the items

            //finish drawing
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}