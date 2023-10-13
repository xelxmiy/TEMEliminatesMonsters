using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System.Diagnostics;
using System.DirectoryServices;

namespace TEMEliminatesMonsters
{
    internal class CameraController
    {
        private const float MovementSpeed = 50f;

        private int _previousMouseX, _previousMouseY;

        private OrthographicCamera _camera;

        public static MouseState state;
        public static Vector2 MousePosition { get { return new Vector2(state.X, state.Y); } }


        public CameraController(OrthographicCamera camera, int mouseX, int mouseY)
        {
            _camera = camera;
            _previousMouseX = mouseX;
            _previousMouseY = mouseY;
        }

        public void Update(GameTime gameTime)
        {
            state = Mouse.GetState();

            Debug.WriteLine($"Camera Pos {_camera.Center}");

            if (state.RightButton == ButtonState.Pressed)
            {
                _camera.Move(GetMovementDirection() * MovementSpeed * gameTime.GetElapsedSeconds());
            }

            _previousMouseX = (int)MousePosition.X;
            _previousMouseY = (int)MousePosition.Y;
        }

        /// <summary>
        /// Returns the movement direction for the camera if holding the mouse
        /// </summary>
        /// <returns></returns>
        private Vector2 GetMovementDirection()
        {
            Vector2 Difference = new Vector2(_previousMouseX, _previousMouseY) - MousePosition;

            float MouseSpeed = Vector2.Distance(new Vector2(_previousMouseX, _previousMouseY), MousePosition);

            if (Difference == Vector2.Zero) return Vector2.Zero; //can't normalize the zero vector be :(

            Vector2 movementDirection = Vector2.Normalize(Difference);

            return movementDirection * MouseSpeed;
        }
    }
}
