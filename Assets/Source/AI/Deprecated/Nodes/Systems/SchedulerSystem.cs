﻿using AI;
using UnityEngine;

namespace Node
{
    public class SchedulerSystem
    {
        public void Update()
        {
            NodeEntity[] nodes = GameState.Planet.EntitasContext.node.GetEntities();

            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i].isNodeBT)
                    continue;

                int index = (int)nodes[i].nodeID.TypeID;
                switch (nodes[i].nodeExecution.State)
                {
                    case Enums.NodeState.Entry:
                        AISystemState.Nodes[index].OnEnter(nodes[i]);
                        break;
                    case Enums.NodeState.Running:
                        AISystemState.Nodes[index].OnUpdate(nodes[i]);
                        break;
                    case Enums.NodeState.Success:
                        AISystemState.Nodes[index].OnExit(nodes[i]);
                        nodes[i].Destroy();
                        break;
                    case Enums.NodeState.Fail:
                        AISystemState.Nodes[index].OnExit(nodes[i]);
                        nodes[i].Destroy();
                        break;
                    default:
                        Debug.Log("Not valid Action state.");
                        break;
                }
            }
        }
    }
}
