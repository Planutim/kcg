//imports UnityEngine

using Entitas;
using KMath;

namespace Vehicle.Pod
{
    [Pod]
    public class PhysicsState2DComponent : IComponent
    {
        public Vec2f Position = Vec2f.Zero;
        public Vec2f TempPosition = Vec2f.Zero;

        public Vec2f Scale = Vec2f.Zero;
        public Vec2f TempScale = Vec2f.Zero;

        public float Rotation = 0.0f;

        [UnityEngine.Range(-1.0f, 1.0f)]
        public Vec2f angularVelocity = Vec2f.Zero;

        public float angularMass = 1.0f;
        public float angularAcceleration = 3.0f;

        public float centerOfGravity = 0.0f;
        public Vec2f centerOfRotation = Vec2f.One;

        public bool AffectedByGravity;
    }
}

