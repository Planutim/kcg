using Entitas;
using KMath;

namespace Particle
{
    [Particle]
    public class PhysicsStateComponent : IComponent
    {
        public Vec2f Position;
        public Vec2f PreviousPosition;
        public Vec2f Acceleration;  // Instantaneous, reset to zero at the end of the frame.
        public Vec2f Velocity;
        public float Rotation;
        public bool Bounce;
        public Vec2f BounceFactor;
    }
}