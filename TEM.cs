using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Diagnostics;

namespace TEMEliminatesMonsters
{
    public class TEM : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        int _previousMouseX, _previousMouseY;

        private OrthographicCamera _camera;

        public TEM()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            _previousMouseX = Mouse.GetState().X;
            _previousMouseY = Mouse.GetState().Y;

            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 480);
            _camera = new OrthographicCamera(viewportAdapter);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) // Note, do NOT draw stuff in here
        {

            const float movementSpeed = 400;
            var MouseState = Mouse.GetState();
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                _camera.Move(GetMovementDirection() * movementSpeed * gameTime.GetElapsedSeconds());
            }

            base.Update(gameTime);

            _previousMouseX = MouseState.X;
            _previousMouseY = MouseState.Y;
        }
        /// <summary>
        /// Returns the movement direction for the camera if holding the mouse
        /// </summary>
        /// <returns></returns>
        private Vector2 GetMovementDirection()
        {
            Vector2 movementDirection = Vector2.Zero;
            int MouseX = Mouse.GetState().X;
            int MouseY = Mouse.GetState().Y;
            Vector2 Difference = Vector2.Subtract(new Vector2(_previousMouseX, _previousMouseY), new Vector2(MouseX, MouseY));

            if(Difference == Vector2.Zero) return Vector2.Zero;

            Debug.WriteLine(Difference.ToString());

            movementDirection = Vector2.Normalize(Difference);

            Debug.WriteLine(movementDirection.ToString());

            return movementDirection;
        }
        protected override void Draw(GameTime gameTime) // Note, do NOT move gameobjects in here 
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var transformMatrix = _camera.GetViewMatrix();
            _spriteBatch.Begin(transformMatrix: transformMatrix);
            _spriteBatch.DrawRectangle(new RectangleF(300, 300, 25, 25), Color.Black, 12.5f);
            _spriteBatch.End(); 

            base.Draw(gameTime);
        }
    }
}