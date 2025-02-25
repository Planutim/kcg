using UnityEngine;
using Enums;

namespace Node
{
    public class ToolActionRemoveMech : NodeBase
    {
        public override NodeType Type => NodeType.ToolActionRemoveMech;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            ref var planet = ref GameState.Planet;
            var itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;

            var mech = planet.GetMechFromPosition(new KMath.Vec2f(x, y));

            if (mech != null)
            {
                planet.Player.UseTool(0.2f);

                planet.RemoveMech(mech.mechID.Index);
            }

            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
