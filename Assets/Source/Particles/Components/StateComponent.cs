using Entitas;
using KMath;

namespace Particle
{
    [Particle]
    public class StateComponent : IComponent
    {
        public float StartingHealth;
        public float Health;
        public float DecayRate;

        public float DeltaRotation;
        public float DeltaScale;

        public Vec4f Color;

        public  Vec2f Size;
    }
}