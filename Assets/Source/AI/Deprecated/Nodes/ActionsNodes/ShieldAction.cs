using Inventory;
using Enums;

namespace Node.Action
{
    public class ShieldAction : NodeBase
    {
        public override NodeType Type => NodeType.ShieldAction;
        public override NodeGroup NodeGroup => NodeGroup.ActionNode;


        public override void OnEnter(NodeEntity nodeEntity) 
        {
            ref var planet = ref GameState.Planet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);

            if (!agentEntity.hasAgentInventory)
                return;

            int inventoryID = agentEntity.agentInventory.InventoryID;
            EntityComponent inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryEntity;
            ref InventoryModel inventoryModel = ref GameState.InventoryCreationApi.Get(inventory.InventoryModelID);

            if (inventoryModel.HasToolBar)
            {
                int selectedSlot = inventory.SelectedSlotID;
                ItemInventoryEntity itemEntity = GameState.InventoryManager.GetItemInSlot(inventoryID, selectedSlot);
                if(itemEntity != null)
                {
                    if(itemEntity.itemType.Type is ItemType.Sword or ItemType.StunBaton)
                    {
                        if(agentEntity.hasAgentPhysicsState)
                            agentEntity.agentPhysicsState.Invulnerable = !agentEntity.agentPhysicsState.Invulnerable;
                    }
                }

                nodeEntity.nodeExecution.State = NodeState.Success;
            }

            nodeEntity.nodeExecution.State = NodeState.Fail;
        }

        public override void OnExit(NodeEntity nodeEntity)
        {
            base.OnExit(nodeEntity);
        }
    }
}
