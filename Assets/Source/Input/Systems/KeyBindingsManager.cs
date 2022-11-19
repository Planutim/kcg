using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECSInput
{
    public class KeyBindingsManager
    {
        private Dictionary<ActionEnums, KeyCode> actionToKey;
        private Dictionary<KeyCode, ActionEnums> keyToAction;

        public static KeyBindingsManager instance;

        public KeyBindingsManager()
        {
            actionToKey = new Dictionary<ActionEnums, KeyCode>();
            keyToAction = new Dictionary<KeyCode, ActionEnums>();
            SetDefaultBindings();
        }

        public  void SetDefaultBindings()
        {
            AssignKeyToAction(KeyCode.A, ActionEnums.PLAYER_LEFT);
            AssignKeyToAction(KeyCode.D, ActionEnums.PLAYER_RIGHT);
            AssignKeyToAction(KeyCode.W, ActionEnums.PLAYER_JUMP);
            AssignKeyToAction(KeyCode.Space, ActionEnums.PLAYER_DASH);
            AssignKeyToAction(KeyCode.K, ActionEnums.PLAYER_ROLL);
            AssignKeyToAction(KeyCode.LeftAlt, ActionEnums.PLAYER_RUN);
            AssignKeyToAction(KeyCode.LeftControl, ActionEnums.PLAYER_CROUCH);
            AssignKeyToAction(KeyCode.F, ActionEnums.PLAYER_JETPACK);
            AssignKeyToAction(KeyCode.S, ActionEnums.PLAYER_WALK);
            AssignKeyToAction(KeyCode.DownArrow, ActionEnums.PLAYER_DOWN);
            AssignKeyToAction(KeyCode.E, ActionEnums.UNKNOWN_ACTION);
            AssignKeyToAction(KeyCode.Q, ActionEnums.RECHARGE_WEAPON);
            AssignKeyToAction(KeyCode.T, ActionEnums.DROP_ACTION);
            AssignKeyToAction(KeyCode.R, ActionEnums.RELOAD_WEAPON);
            AssignKeyToAction(KeyCode.Mouse1, ActionEnums.SHIELD_ACTION);
            AssignKeyToAction(KeyCode.F1, ActionEnums.TOGGLE_STAT);
            AssignKeyToAction(KeyCode.F2, ActionEnums.REMOVE_TILE_FRONT_AT_CURSOR_POSITION);
            AssignKeyToAction(KeyCode.F3, ActionEnums.REMOVE_TILE_BACK_AT_CURSOR_POSITION);
            AssignKeyToAction(KeyCode.F4, ActionEnums.ENABLE_TILE_COLLISION_ISOTYPE_RENDERING);
            AssignKeyToAction(KeyCode.Tab, ActionEnums.OPEN_INVENTORY);
            AssignKeyToAction(KeyCode.N, ActionEnums.CHANGE_PULSE_WEAPON_MODE);
        }

        public void AssignKeyToAction(KeyCode keyCode, ActionEnums action)
        {
            //check if binding exists
            if (keyToAction.ContainsKey(keyCode))
            {
                var existingBinding = keyToAction[keyCode];
                actionToKey[existingBinding] = KeyCode.None;
            }
            actionToKey.Add(action, keyCode);
            keyToAction.Add(keyCode, action);
        }

        public KeyCode GetKeyForAction(ActionEnums action)
        {
            if (actionToKey[action] != KeyCode.None)
            {
                return actionToKey[action];
            }
            return KeyCode.None;
        }
    }



}