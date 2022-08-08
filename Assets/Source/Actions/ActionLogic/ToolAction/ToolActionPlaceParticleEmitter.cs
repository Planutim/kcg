using Entitas;
using UnityEngine;
using KMath;
using Enums.Tile;
using Action;

namespace Action
{
    public class ToolActionPlaceParticleEmitter : ActionBase
    {
        // Todo create methods to instatiate Agents.
        // Data should only have something like:
        // struct Data
        // {
        //      Enums.enemyType type
        // }

        public struct Data
        {
            public Material Material;
        }

        Data data;

        public ToolActionPlaceParticleEmitter(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
            data = (Data)ActionPropertyEntity.actionPropertyData.Data;
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;
            int t = System.Math.Abs((int)KMath.Random.Mt19937.genrand_int32() % System.Enum.GetNames(typeof(Particle.ParticleType)).Length);

            planet.AddDebris(new Vec2f(x, y), GameResources.ChestIconParticle, 1.5f, 1.0f);

            ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
        }
    }

    // Factory Method
    public class ToolActionPlaceParticleCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ToolActionPlaceParticleEmitter(entitasContext, actionID);
        }
    }
}
