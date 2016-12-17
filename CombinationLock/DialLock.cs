using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CombinationLock
{
    public class DialLock
    {
        private Sprite sprite;
        public Vector2 Position
        {
            get
            {
                return sprite.position;
            }
            set
            {
                sprite.position = value;
            }
        }
        public int Rotation { get; private set; }
        public int TargetRotation { get; private set; }

        public DialLock(Texture2D tex)
        {
            sprite = new Sprite(tex);
            Rotation = 0;
            //TargetRotation = World.random.Next(0, 180);
            TargetRotation = 90;

            World.dial.RotationChanged += Dial_RotationChanged;
        }

        private void Dial_RotationChanged(Windows.UI.Input.RadialController sender, Windows.UI.Input.RadialControllerRotationChangedEventArgs args)
        {
            sprite.rotation += (float)args.RotationDeltaInDegrees;
            if (sprite.rotation >= 360 || sprite.rotation <= -360)
            {
                sprite.rotation = 0;
            }

            if (sprite.rotation == 0)
            {
                Rotation = 0;
            }
            else if (sprite.rotation < 0)
            {
                Rotation = (int)Math.Abs(sprite.rotation / 2);
            }
            else if (sprite.rotation > 0)
            {
                Rotation = (int)Math.Abs((sprite.rotation - 360) / 2);
            }
            if (Rotation >= TargetRotation - 10 && Rotation <= TargetRotation + 10)
            {
                World.dial.UseAutomaticHapticFeedback = true;
            }
            else
            {
                World.dial.UseAutomaticHapticFeedback = false;
            }
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }
    }
}
