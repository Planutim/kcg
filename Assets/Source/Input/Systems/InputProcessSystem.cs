using KMath;
using UnityEngine;
using Agent;

namespace ECSInput
{
    public class InputProcessSystem
    {
        public void Update(ref Planet.PlanetState planet)
        {
            Contexts contexts = planet.EntitasContext;

            var AgentsWithXY = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.ECSInput, AgentMatcher.ECSInputXY));

            bool jump = Input.GetKeyDown(KeyCode.UpArrow);
            bool dash = Input.GetKeyDown(KeyCode.Space);
            bool running = Input.GetKey(KeyCode.LeftAlt);
            bool flying = Input.GetKey(KeyCode.F);

            float x = 0.0f;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                x = 1.0f;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                x = -1.0f;
            }

            foreach (var entity in AgentsWithXY)
            {
                entity.ReplaceECSInputXY(new Vec2f(x, 0.0f), jump, dash);

                var pos = entity.physicsPosition2D;
                var input = entity.eCSInputXY;
                var movable = entity.physicsMovable;
                var stats = entity.agentStats;

                var movementState = entity.agentMovementState;
                movementState.Running = running;


                // handling horizontal movement (left/right)
                if (movementState.Running)
                {
                    movable.Acceleration.X = input.Value.X * movable.Speed * 100.0f * 2;
                }
                else
                {
                    movable.Acceleration.X = input.Value.X * movable.Speed * 100.0f;
                }         

                // decrease the dash cooldown
                movementState.DashCooldown -= Time.deltaTime;

                // dash
                if (dash && movementState.DashCooldown <= 0.0f)
                {
                    movable.Acceleration.X += 500.0f * x;
                    movable.Velocity.X = 90.0f * x;

                    movable.Velocity.Y = 0.0f;
                    movable.Acceleration.Y = 0.0f;

                    movable.Invulnerable = true;
                    movable.AffectedByGravity = false;
                    movementState.MovementState = MovementState.Dashing;
                    movementState.DashCooldown = 1.0f;
                }


                // we can start jumping only if the jump counter is 0
                if (movementState.JumpCounter == 0)
                {
    
                    // first jump
                    if (jump && movementState.MovementState != MovementState.Dashing)
                    {
                        // if we are sticking to a wall 
                        // throw the agent in the opposite direction
                        if (movable.SlidingLeft)
                        {
                            movable.SlidingLeft = false;
                            movable.Acceleration.X = 1.0f * movable.Speed * 400.0f * 2;
                            movable.Acceleration.Y = -1.0f * movable.Speed * 400.0f * 2;
                        }
                        else if (movable.SlidingRight)
                        {
                            movable.SlidingRight = false;
                            movable.Acceleration.X = -1.0f * movable.Speed * 400.0f * 2;
                            movable.Acceleration.Y = -1.0f * movable.Speed * 400.0f * 2;
                        }


                        // jumping
                        movable.Landed = false;
                        movable.Acceleration.Y = 100.0f;
                        movable.Velocity.Y = 11.5f;
                        movable.AffectedByGroundFriction = false;
                        movementState.JumpCounter++;
                    }

                }
                else
                {
                    // double jump
                    if (jump && movementState.JumpCounter <= 1)
                    {
                        movable.Acceleration.Y = 100.0f;
                        movable.Velocity.Y = 8.5f;
                        movementState.JumpCounter++;
                    }
                }

                // if the fly button is pressed
                if (flying && stats.Fuel > 0.0f)
                {
                    movementState.MovementState = MovementState.Flying;
                }
                else if (movementState.MovementState == MovementState.Flying)
                {
                    // if no fuel is left we change to movement state to none
                    movementState.MovementState = MovementState.None;
                }

                // if we are using the jetpack
                // set the Y velocity to a given value
                if (movementState.MovementState == MovementState.Flying)
                {
                    movable.Acceleration.Y = 0;
                    movable.Velocity.Y = 3.5f;
                }

                // the end of dashing
                // we can do this using a fixed amount of time
                if (System.Math.Abs(movable.Velocity.X) <= 6.0f && 
                movementState.MovementState == MovementState.Dashing)
                {

                    movementState.MovementState = MovementState.None;    
                }

                // if the agent is dashing it becomes invulnerable to damage
                movable.Invulnerable = movementState.MovementState == MovementState.Dashing;
                // if the agent is dashing the gravity will not affect him
                movable.AffectedByGravity = !(movementState.MovementState == MovementState.Dashing);


                if (x == 1.0f)
                {
                    // if we move to the right
                    // that means we are no longer sliding down on the left
                    movable.SlidingLeft = false;
                }
                else if (x == -1.0f)
                {
                    // if we move to the left
                    // that means we are no longer sliding down on the right
                    movable.SlidingRight = false;
                }


                // if we are on the ground we reset the jump counter
                if (movable.Landed)
                {
                    movementState.JumpCounter = 0;
                    movable.AffectedByGroundFriction = true;

                    movable.SlidingRight = false;
                    movable.SlidingLeft = false;
                }

                
                // if we are sliding
                // spawn some particles and limit vertical movement
                if (movable.SlidingLeft)
                {
                    movementState.JumpCounter = 0;
                    movable.Acceleration.Y = 0.0f;
                    movable.Velocity.Y = -1.75f;
                    planet.AddParticleEmitter(pos.Value + new Vec2f(0.0f, -0.5f), Particle.ParticleEmitterType.DustEmitter);
                }
                else if (movable.SlidingRight)
                {
                    movementState.JumpCounter = 0;
                    movable.Acceleration.Y = 0.0f;
                    movable.Velocity.Y = -1.75f;
                    planet.AddParticleEmitter(pos.Value + new Vec2f(0.5f, -0.5f), Particle.ParticleEmitterType.DustEmitter);
                }

                // if we are flying, reduce the fuel and spawn particles
                if (movementState.MovementState == MovementState.Flying)
                {
                    stats.Fuel -= 1.0f;
                    if (stats.Fuel <= 1.0f)
                    {
                        stats.Fuel -= 20.0f;
                    }
                    planet.AddParticleEmitter(pos.Value, Particle.ParticleEmitterType.DustEmitter);
                }
                else
                {
                    // if we are not flying, add fuel to the tank
                    stats.Fuel += 1.0f;
                }

                // make sure the fuel never goes up more than it should
                if (stats.Fuel > 100) 
                {
                    stats.Fuel = 100;
                }

                // if we are dashing we add some particles
                if (movementState.MovementState == MovementState.Dashing)
                {
                    planet.AddParticleEmitter(pos.Value, Particle.ParticleEmitterType.DustEmitter);
                }

            }


            // Recharge Weapon.
            if (Input.GetKeyDown(KeyCode.E))
            {
                var players = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
                foreach (var player in players) 
                    GameState.ActionCreationSystem.CreateAction(planet.EntitasContext,Enums.ActionType.ChargeAction, player.agentID.ID);
            }

            // Reload Weapon.
            if (Input.GetKeyDown(KeyCode.R))
            {
                var players = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
                foreach (var player in players)
                    GameState.ActionCreationSystem.CreateAction(planet.EntitasContext, Enums.ActionType.ReloadAction, player.agentID.ID);
            }

            //  Open Inventory with Tab.
            var PlayerWithInventory = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentInventory, AgentMatcher.AgentPlayer));
            foreach (var entity in PlayerWithInventory)
            {
                int inventoryID = entity.agentInventory.InventoryID;
                InventoryEntity inventoryEntity = contexts.inventory.GetEntityWithInventoryID(inventoryID);

                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    inventoryEntity.isInventoryDrawable = !inventoryEntity.isInventoryDrawable;
                }
            }

            // Change Item Selection with nums.
            var PlayerWithToolBar = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer, AgentMatcher.AgentToolBar));
            foreach (var entity in PlayerWithInventory)
            {
                int inventoryID = entity.agentToolBar.ToolBarID;
                InventoryEntity inventoryEntity = contexts.inventory.GetEntityWithInventoryID(inventoryID);
                var SlotComponent = inventoryEntity.inventorySlots;

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    inventoryEntity.ReplaceInventorySlots(SlotComponent.Values, 0);

                    var item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext.itemInventory, inventoryID, 0);

                    var entities = planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));

                    // Todo: Create a agent colision system?
                    foreach (var entityP in entities)
                    {
                        if (entityP.isAgentPlayer)
                        {
                            planet.AddFloatingText(item.itemType.Type.ToString(), 2.0f, Vec2f.Zero, new Vec2f(entityP.physicsPosition2D.Value.X + 0.4f,
                                entityP.physicsPosition2D.Value.Y));
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    inventoryEntity.ReplaceInventorySlots(SlotComponent.Values, 1);

                    var item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext.itemInventory, inventoryID, 1);

                    var entities = planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));

                    // Todo: Create a agent colision system?
                    foreach (var entityP in entities)
                    {
                        if (entityP.isAgentPlayer)
                        {
                            planet.AddFloatingText(item.itemType.Type.ToString(), 2.0f, Vec2f.Zero, new Vec2f(entityP.physicsPosition2D.Value.X + 0.4f,
                                entityP.physicsPosition2D.Value.Y));
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    inventoryEntity.ReplaceInventorySlots(SlotComponent.Values, 2);

                    var item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext.itemInventory, inventoryID, 2);

                    var entities = planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));

                    // Todo: Create a agent colision system?
                    foreach (var entityP in entities)
                    {
                        if (entityP.isAgentPlayer)
                        {
                            planet.AddFloatingText(item.itemType.Type.ToString(), 2.0f, Vec2f.Zero, new Vec2f(entityP.physicsPosition2D.Value.X + 0.4f,
                                entityP.physicsPosition2D.Value.Y));
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    inventoryEntity.ReplaceInventorySlots(SlotComponent.Values, 3);

                    var item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext.itemInventory, inventoryID, 3);

                    var entities = planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));

                    // Todo: Create a agent colision system?
                    foreach (var entityP in entities)
                    {
                        if (entityP.isAgentPlayer)
                        {
                            planet.AddFloatingText(item.itemType.Type.ToString(), 2.0f, Vec2f.Zero, new Vec2f(entityP.physicsPosition2D.Value.X + 0.4f,
                                entityP.physicsPosition2D.Value.Y));
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    inventoryEntity.ReplaceInventorySlots(SlotComponent.Values, 4);

                    var item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext.itemInventory, inventoryID, 4);

                    var entities = planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));

                    // Todo: Create a agent colision system?
                    foreach (var entityP in entities)
                    {
                        if (entityP.isAgentPlayer)
                        {
                            planet.AddFloatingText(item.itemType.Type.ToString(), 2.0f, Vec2f.Zero, new Vec2f(entityP.physicsPosition2D.Value.X + 0.4f,
                                entityP.physicsPosition2D.Value.Y));
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    inventoryEntity.ReplaceInventorySlots(SlotComponent.Values, 5);

                    var item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext.itemInventory, inventoryID, 5);

                    var entities = planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));

                    // Todo: Create a agent colision system?
                    foreach (var entityP in entities)
                    {
                        if (entityP.isAgentPlayer)
                        {
                            planet.AddFloatingText(item.itemType.Type.ToString(), 2.0f, Vec2f.Zero, new Vec2f(entityP.physicsPosition2D.Value.X + 0.4f,
                                entityP.physicsPosition2D.Value.Y));
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha7))
                {
                    inventoryEntity.ReplaceInventorySlots(SlotComponent.Values, 6);

                    var item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext.itemInventory, inventoryID, 6);

                    var entities = planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));

                    // Todo: Create a agent colision system?
                    foreach (var entityP in entities)
                    {
                        if (entityP.isAgentPlayer)
                        {
                            planet.AddFloatingText(item.itemType.Type.ToString(), 2.0f, Vec2f.Zero, new Vec2f(entityP.physicsPosition2D.Value.X + 0.4f,
                                entityP.physicsPosition2D.Value.Y));
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha8))
                {
                    inventoryEntity.ReplaceInventorySlots(SlotComponent.Values, 7);

                    var item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext.itemInventory, inventoryID, 7);

                    var entities = planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));

                    // Todo: Create a agent colision system?
                    foreach (var entityP in entities)
                    {
                        if (entityP.isAgentPlayer)
                        {
                            planet.AddFloatingText(item.itemType.Type.ToString(), 2.0f, Vec2f.Zero, new Vec2f(entityP.physicsPosition2D.Value.X + 0.4f,
                                entityP.physicsPosition2D.Value.Y));
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha9))
                {
                    inventoryEntity.ReplaceInventorySlots(SlotComponent.Values, 8);

                    var item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext.itemInventory, inventoryID, 8);

                    var entities = planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));

                    // Todo: Create a agent colision system?
                    foreach (var entityP in entities)
                    {
                        if (entityP.isAgentPlayer)
                        {
                            planet.AddFloatingText(item.itemType.Type.ToString(), 2.0f, Vec2f.Zero, new Vec2f(entityP.physicsPosition2D.Value.X + 0.4f,
                                entityP.physicsPosition2D.Value.Y));
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha0))
                {
                    inventoryEntity.ReplaceInventorySlots(SlotComponent.Values, 9);

                    var entities = planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));

                    // Todo: Create a agent colision system?
                    foreach (var entityP in entities)
                    {
                        if(entityP.isAgentPlayer)
                        {
                            var item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext.itemInventory, inventoryID, 9);

                            planet.AddFloatingText(item.itemType.Type.ToString(), 2.0f, Vec2f.Zero, new Vec2f(entityP.physicsPosition2D.Value.X + 0.4f,
                                entityP.physicsPosition2D.Value.Y));
                        }
                    }
                }
            }
        }
    }
}