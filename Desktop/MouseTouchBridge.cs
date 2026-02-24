using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace ctre_wp7.Desktop
{
    internal sealed class MouseTouchBridge
    {
        public List<TouchLocation> BuildTouches()
        {
            List<TouchLocation> touches = [];
            foreach (TouchLocation touch in TouchPanel.GetState())
            {
                touches.Add(touch);
            }

            MouseState mouse = Mouse.GetState();
            bool leftPressed = mouse.LeftButton == ButtonState.Pressed;
            bool wasLeftPressed = _previous.LeftButton == ButtonState.Pressed;
            Vector2 mousePos = new(mouse.X, mouse.Y);

            if (leftPressed)
            {
                if (!wasLeftPressed)
                {
                    _activeTouchId++;
                    touches.Add(new TouchLocation(_activeTouchId, TouchLocationState.Pressed, mousePos));
                }
                else
                {
                    touches.Add(new TouchLocation(_activeTouchId, TouchLocationState.Moved, mousePos));
                }
            }
            else if (wasLeftPressed)
            {
                Vector2 releasePos = new(_previous.X, _previous.Y);
                touches.Add(new TouchLocation(_activeTouchId, TouchLocationState.Released, releasePos));
            }

            _previous = mouse;
            return touches;
        }

        private MouseState _previous;
        private int _activeTouchId;
    }
}
