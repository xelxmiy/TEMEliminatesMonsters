using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMEliminatesMonsters
{
    internal class CameraController
    {
        private const float MovementSpeed = 400;

        private int _previousMouseX, _previousMouseY;

        private OrthographicCamera _camera;

        public CameraController(OrthographicCamera camera, int mouseX, int mouseY) 
        {
            _camera = camera;
            _previousMouseX = mouseX;
            _previousMouseY = mouseY;
        }

        public void Update(GameTime gameTime) 
        {
            MouseState MouseState = Mouse.GetState();
            if (MouseState.RightButton == ButtonState.Pressed)
            {
                _camera.Move(GetMovementDirection() * MovementSpeed * gameTime.GetElapsedSeconds());
            }

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

            if (Difference == Vector2.Zero) return Vector2.Zero;

            movementDirection = Vector2.Normalize(Difference);

            return movementDirection;
        }
    }
}
