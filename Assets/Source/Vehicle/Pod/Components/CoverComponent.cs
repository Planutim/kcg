using Entitas;
using KMath;
using System.Collections.Generic;


namespace Vehicle.Pod
{
    [Pod]
    public class CoverComponent : IComponent
    {
        public List<Vec2f> CoverPositions;
        public List<Vec2f> FirePositions;
    }
}