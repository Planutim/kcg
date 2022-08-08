﻿using Entitas;
using System.Collections.Generic;
using System.Collections;
using KMath;
using UnityEngine;

namespace Projectile
{
    public class MovementSystem
    {
        ProjectileCreationApi ProjectileCreationApi;

        float Gravity = 0.1f;

        public bool fixedGravity = false;

        public MovementSystem(ProjectileCreationApi projectileCreationApi)
        {
            ProjectileCreationApi = projectileCreationApi;
        }

        public void Update(ProjectileContext gameContext)
        {
            float deltaTime = Time.deltaTime;
            var projectiles = gameContext.GetGroup(ProjectileMatcher.ProjectilePhysicsState);
            foreach (var projectile in projectiles)
            {
                // Get Projectile Type
                var type = projectile.projectileType.Type;

                // Get Projectile Properties
                ProjectileProperties projectileProperties =
                                    ProjectileCreationApi.GetRef((int)type);

                // Get Projectile Position
                var pos = projectile.projectilePhysicsState;

                // Get Movable Component
                var movable = projectile.projectilePhysicsState;

                // Set Gravity
                projectileProperties.GravityScale = 0.1f;
                Gravity = projectileProperties.GravityScale;

                // Get Projectile Can Ramp Condition
                var canRamp = projectile.projectileRamp.canRamp;

                // Get Linear Drag
                var linearDrag = projectile.projectileLinearDrag.Drag;

                // Get Linear Drag Cutoff
                var linearCutoff = projectile.projectileLinearDrag.CutOff;

                // Calculate Displacement
                Vec2f displacement =
                    0.5f * movable.Acceleration * (deltaTime * deltaTime) + movable.Velocity * deltaTime;

                // New Calculated Velocity
                Vec2f newVelocity = new Vec2f(0f, 0f);

                if(projectileProperties.AffectedByGravity)
                    projectile.projectilePhysicsState.Velocity.Y -= Gravity;

                // If Ramp is On
                if(canRamp)
                {
                    // Get Start Speed for Ramp
                    var startSpeed = projectile.projectileRamp.startVelocity;

                    // Get Max Speed for Ramp
                    var maxSpeed = projectile.projectileRamp.maxVelocity;

                    // Get Ramp Time for Ramp
                    var rampTime = projectile.projectileRamp.rampTime; 

                    // Elapsed time
                    float elapsed = 0.0f;

                    // Smoothly Increasing velocity
                    while (elapsed < rampTime)
                    {
                        // If ramp is on
                        if (canRamp)
                        {
                            // Increase projectile speed smoothly
                            projectileProperties.Speed = Mathf.Lerp(startSpeed, maxSpeed, elapsed / rampTime);

                            // If linear drag is on
                            if (projectileProperties.dragType == Enums.DragType.Linear)
                            {
                                // Apply linear drag to speed
                                projectileProperties.Speed = (1 - projectileProperties.Speed / (linearDrag + linearCutoff));

                                // Calculate Drag Force Magnitude
                                var dragForceMag = movable.Velocity.Magnitude / 2 * linearDrag;

                                // Calculate Force Vector
                                var dragForceVector = dragForceMag *  new Vec2f(-movable.Velocity.Normalized.X, -movable.Velocity.Normalized.Y);

                                // Calculate New Velocity
                                newVelocity = movable.Acceleration * deltaTime + (movable.Velocity * projectileProperties.Speed);

                                // Add drag force to velocity vector
                                newVelocity += dragForceVector;
                            }
                            else if (projectileProperties.dragType == Enums.DragType.Off) // If linear drag is off
                            {
                                // Set New velocity without adding any drag
                                newVelocity = movable.Acceleration * deltaTime + (movable.Velocity * projectileProperties.Speed);
                            }
                            
                            // Increase Time
                            elapsed += Time.deltaTime;
                        }
                    }
                    // Set Speed to Maxmium Velocity
                    projectileProperties.Speed = projectileProperties.MaxVelocity;
                }
                else
                {
                    // If linear drag is on
                    if(projectileProperties.dragType == Enums.DragType.Linear)
                    {
                        // Calculate Speed
                        projectileProperties.Speed = (1 - projectileProperties.Speed / (linearDrag + linearCutoff));


                        // Calculate Drag Force Magnitude
                        var dragForceMag = movable.Velocity.Magnitude / 2 * linearDrag;


                        // Calculate Drag Force Vector
                        var dragForceVector = dragForceMag * new Vec2f(-movable.Velocity.Normalized.X, -movable.Velocity.Normalized.Y);


                        // Calculate New Velocity
                        newVelocity = movable.Acceleration * deltaTime + movable.Velocity / linearDrag;

                        // Add drag force to new velocity
                        newVelocity += dragForceVector;


                    }
                    else if (projectileProperties.dragType == Enums.DragType.Off)
                    {
                        newVelocity = movable.Acceleration * deltaTime + movable.Velocity;
                    }
                }

                float newRotation = pos.Rotation + projectileProperties.DeltaRotation * deltaTime;  

                projectile.ReplaceProjectilePhysicsState(pos.Position + displacement, pos.Position, newRotation,
                    newVelocity, movable.Acceleration, movable.AffectedByGravity, newVelocity, projectile.projectilePhysicsState.angularMass,
                    projectile.projectilePhysicsState.angularAcceleration, projectile.projectilePhysicsState.centerOfGravity,
                    projectile.projectilePhysicsState.centerOfRotation);
            }
        }
    }
}
