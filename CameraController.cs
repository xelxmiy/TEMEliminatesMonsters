﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Diagnostics;
using System.DirectoryServices;
using TEMEliminatesMonsters.KeyEvents;
using TEMEliminatesMonsters.Updateables;

namespace TEMEliminatesMonsters
{
    internal class CameraController : Updateables.IUpdateable
    {
        private const float MovementSpeed = 50f;

        private int _previousMouseX, _previousMouseY;

        private int _previousScrollValue;

        private OrthographicCamera _camera;

        public static MouseState state;
        public static Vector2 MousePosition { get { return new Vector2(state.X, state.Y); } }

        /// <summary>
        /// Creates a new CameraController
        /// </summary>
        /// <param name="camera">Camera object</param>
        /// <param name="minZoom">Maximum screen zoom for this camera</param>
        /// <param name="maxZoom">Minimum screen zoom for this camera</param>
        public CameraController(OrthographicCamera camera, int minZoom = 1, int maxZoom = 10)
        {
            _camera = camera;
            _camera.MaximumZoom = maxZoom;
            _camera.MinimumZoom = minZoom;
            _previousMouseX = Mouse.GetState().X;
            _previousMouseY = Mouse.GetState().Y;
            (this as Updateables.IUpdateable).AddSelfToUpdateables();
            KeyboardEventManager.GetEvent(Keys.F) += () =>
            {
                _camera.LookAt(TEM.Instance._zombiePosition);
            };
        }

        /// <summary>
        /// Updates the cameras position based on the mouse movement
        /// </summary>
        /// <param name="gameTime">The current gametime</param>
        public void Update(GameTime gameTime)
        {
            state = Mouse.GetState();

            if (state.RightButton == ButtonState.Pressed)
            {
                _camera.Move(GetMovementDirection() * MovementSpeed * gameTime.GetElapsedSeconds());
            }
            if (state.ScrollWheelValue != _previousScrollValue)
            {
                Zoom(state.ScrollWheelValue - _previousScrollValue);
            }
            _previousMouseX = (int)MousePosition.X;
            _previousMouseY = (int)MousePosition.Y;
            _previousScrollValue = state.ScrollWheelValue;

        }

        /// <summary>
        /// Zooms in/out
        /// </summary>
        /// <param name="value">The Amount to zoom in</param>
        private void Zoom(float value)
        {
            value = value / 360f + 1;
            if (value >= _camera.MinimumZoom && value <= _camera.MaximumZoom)
            {
                _camera.Zoom = value;
            }
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
