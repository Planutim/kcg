﻿using Entitas;

namespace Item.FireWeapon
{
    [ItemParticle, ItemInventory]
    public class ClipComponent : IComponent
    {
        public int NumOfBullets;
    }
}

