//imports UnityEngine

using KMath;
using Utility;

namespace Particle
{
    public class ParticleProcessCollisionSystem
    {

        public void Update()
        {
            float deltaTime = UnityEngine.Time.deltaTime;
            var entitiesWithBox = GameState.Planet.EntitasContext.particle.GetGroup(ParticleMatcher.AllOf(ParticleMatcher.ParticleBox2DCollider));

            foreach (var entity in entitiesWithBox)
            {
                var physicsState = entity.particlePhysicsState;
                var box2DCollider = entity.particleBox2DCollider;

                // Collising with terrain with raycasting
                var rayCastingResult =
                Collisions.Collisions.RayCastAgainstTileMapBox2d(new Line2D(physicsState.PreviousPosition, physicsState.Position), box2DCollider.Size.X, box2DCollider.Size.Y);
                Vec2f oppositeDirection = (physicsState.PreviousPosition - physicsState.Position).Normalized;

                if (rayCastingResult.Intersect)
                {
                    physicsState.Position = rayCastingResult.Point;
                    if (physicsState.Bounce)
                    {
                        physicsState.Velocity = physicsState.Velocity * physicsState.BounceFactor;
                        if (System.Math.Abs(rayCastingResult.Normal.X) > 0)
                        {
                            physicsState.Velocity.X = -physicsState.Velocity.X;
                        }
                        else if (System.Math.Abs(rayCastingResult.Normal.Y) > 0)
                        {
                            physicsState.Velocity.Y = -physicsState.Velocity.Y;
                        }
                    }
                    else
                    {    
                        physicsState.Velocity = new Vec2f();
                    }
                }

                
                //UnityEngine.Debug.Log(box2DCollider.Size.X + " " + box2DCollider.Size.Y);

                 var entityBoxBorders = new AABox2D(physicsState.Position + box2DCollider.Offset, box2DCollider.Size);



                /*if (entityBoxBorders.IsCollidingBottom(tileMap, physicsState.Velocity))
                {
                    var tile = tileMap.GetTile((int)physicsState.Position.X, (int)physicsState.Position.Y);
                    var property = GameState.TileCreationApi.GetTileProperty(tile.FrontTileID);

                    physicsState.Position = new Vec2f(physicsState.Position.X, physicsState.PreviousPosition.Y);
                    physicsState.Velocity.Y = 0.0f;
                    physicsState.Acceleration.Y = 0.0f;
                    physicsState.Velocity.X = 0.0f;
                    physicsState.Acceleration.X = 0.0f;

                }
                else if (entityBoxBorders.IsCollidingTop(tileMap, physicsState.Velocity))
                {   
                    physicsState.Position = new Vec2f(physicsState.Position.X, physicsState.PreviousPosition.Y);
                    physicsState.Velocity.Y = 0.0f;
                    physicsState.Acceleration.Y = 0.0f;
                }

                entityBoxBorders = new AABox2D(new Vec2f(physicsState.Position.X, physicsState.PreviousPosition.Y) + box2DCollider.Offset, box2DCollider.Size);

                if (entityBoxBorders.IsCollidingLeft(tileMap, physicsState.Velocity))
                {
                    physicsState.Position = new Vec2f(physicsState.PreviousPosition.X, physicsState.Position.Y);
                    physicsState.Velocity.X = 0.0f;
                    physicsState.Acceleration.X = 0.0f;
                }
                else if (entityBoxBorders.IsCollidingRight(tileMap, physicsState.Velocity))
                {
                    physicsState.Position = new Vec2f(physicsState.PreviousPosition.X, physicsState.Position.Y);
                    physicsState.Velocity.X = 0.0f;
                    physicsState.Acceleration.X = 0.0f;
                }*/

                entityBoxBorders.DrawBox();
            }
        }
    }
}
