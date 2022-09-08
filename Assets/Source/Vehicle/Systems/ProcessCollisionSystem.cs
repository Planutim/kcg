using Collisions;
using KMath;
using UnityEngine;

namespace Vehicle
{
    public class ProcessCollisionSystem
    {
        public void Update(ref Planet.PlanetState planet)
        {
            // Get Delta Time
            float deltaTime = Time.deltaTime;

            // Get Vehicle Physics Entity
            var entities = planet.EntitasContext.vehicle.GetGroup(VehicleMatcher.AllOf(VehicleMatcher.PhysicsBox2DCollider, VehicleMatcher.VehiclePhysicsState2D));

            foreach (var entity in entities)
            {
                // Set Vehicle Physics to variable
                var pos = entity.vehiclePhysicsState2D;

                var size = entity.physicsBox2DCollider.Size;

                // Create Box Borders
                var entityBoxBorders = new AABox2D(new Vec2f(pos.TempPosition.X, pos.Position.Y), size);

                // If is colliding bottom-top stop y movement
                if (entityBoxBorders.IsCollidingBottom(planet.TileMap, pos.angularVelocity))
                {
                    entity.ReplaceVehiclePhysicsState2D(new Vec2f(pos.Position.X, pos.TempPosition.Y), pos.TempPosition, pos.Scale, pos.TempScale, 
                        new Vec2f(pos.angularVelocity.X, 0.0f), pos.angularMass, pos.angularAcceleration, pos.centerOfGravity, pos.centerOfRotation);
                }
                else if (entityBoxBorders.IsCollidingTop(planet.TileMap, pos.angularVelocity))
                {
                    entity.ReplaceVehiclePhysicsState2D(new Vec2f(pos.Position.X, pos.TempPosition.Y), pos.TempPosition, pos.Scale, pos.TempScale,
                      new Vec2f(pos.angularVelocity.X, 0.0f), pos.angularMass, pos.angularAcceleration, pos.centerOfGravity, pos.centerOfRotation);
                }

                pos = entity.vehiclePhysicsState2D;
                size = entity.physicsBox2DCollider.Size;
                entityBoxBorders = new AABox2D(new Vec2f(pos.Position.X, pos.TempPosition.Y), size);

                // If is colliding left-right stop x movement
                if (entityBoxBorders.IsCollidingLeft(planet.TileMap, pos.angularVelocity))
                {
                    entity.ReplaceVehiclePhysicsState2D(new Vec2f(pos.Position.X, pos.TempPosition.Y), pos.TempPosition, pos.Scale, pos.TempScale,
                      new Vec2f(0.0f, pos.angularVelocity.Y), pos.angularMass, pos.angularAcceleration, pos.centerOfGravity, pos.centerOfRotation);
                }
                else if (entityBoxBorders.IsCollidingRight(planet.TileMap, pos.angularVelocity))
                {
                    entity.ReplaceVehiclePhysicsState2D(new Vec2f(pos.Position.X, pos.TempPosition.Y), pos.TempPosition, pos.Scale, pos.TempScale,
                      new Vec2f(0.0f, pos.angularVelocity.Y), pos.angularMass, pos.angularAcceleration, pos.centerOfGravity, pos.centerOfRotation);
                }
            }
        }
    }
}

