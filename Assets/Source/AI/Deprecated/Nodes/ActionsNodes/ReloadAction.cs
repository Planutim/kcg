﻿using Item;
using UnityEngine;
using Enums;

namespace Node.Action
{
    public class ReloadAction : NodeBase
    {
        public override NodeType Type => NodeType.ReloadAction;
        public override NodeGroup NodeGroup => NodeGroup.ActionNode;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            ItemInventoryEntity item = nodeEntity.GetItem();

            if (item != null)
            {
                if (item.hasItemFireWeaponClip)
                {
                    nodeEntity.nodeExecution.State = NodeState.Running;
                    return;
                }
            }
            nodeEntity.nodeExecution.State = NodeState.Fail;
        }

        public override void OnUpdate(NodeEntity nodeEntity)
        {
            ItemInventoryEntity item = nodeEntity.GetItem();
            FireWeaponPropreties WeaponPropreties = GameState.ItemCreationApi.GetWeapon(item.itemType.Type);

            float runningTime = Time.realtimeSinceStartup - nodeEntity.nodeTime.StartTime;
            if (runningTime >= WeaponPropreties.ReloadTime)
            {
                if(item.hasItemFireWeaponClip)
                    item.itemFireWeaponClip.NumOfBullets = WeaponPropreties.ClipSize;

                nodeEntity.nodeExecution.State =  NodeState.Success;
            }
        }

        public override void OnExit(NodeEntity nodeEntity)
        {
            ItemInventoryEntity item = nodeEntity.GetItem();

            if (item.hasItemFireWeaponClip)
                Debug.Log("Weapon Reloaded." + item.itemFireWeaponClip.NumOfBullets.ToString() + " Ammo in the clip.");
        }

        public override void OnFail(NodeEntity nodeEntity)
        {
            Debug.Log("Reload Failed.");
        }
    }
}
