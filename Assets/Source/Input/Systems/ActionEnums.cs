using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECSInput
{
    public enum ActionEnums
    {
        PLAYER_LEFT,
        PLAYER_RIGHT,
        PLAYER_JUMP,
        PLAYER_DASH,
        PLAYER_ROLL,
        PLAYER_RUN,
        PLAYER_CROUCH,
        PLAYER_JETPACK,
        PLAYER_WALK,
        PLAYER_DOWN,

        UNKNOWN_ACTION,
        RECHARGE_WEAPON,
        DROP_ACTION,
        RELOAD_WEAPON,
        SHIELD_ACTION,

        TOGGLE_STAT,
        REMOVE_TILE_FRONT_AT_CURSOR_POSITION,
        REMOVE_TILE_BACK_AT_CURSOR_POSITION,
        ENABLE_TILE_COLLISION_ISOTYPE_RENDERING,
        OPEN_INVENTORY,
        CHANGE_PULSE_WEAPON_MODE,
    }
}
