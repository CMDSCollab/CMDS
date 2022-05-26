using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Enemy : CombatBaseState
{
    public override void EnterState(GameMaster gM)
    {
        Debug.Log("EnemyCSEntered");
        gM.enM.enemyTarget.TakeAction();
    }

    public override void UpdateState(GameMaster gM)
    {
        if (gM.combatSM.isUpdate == true)
        {
            gM.combatSM.isUpdate = false;
            EndState(gM);
        }
    }

    public override void EndState(GameMaster gM)
    {
        gM.buffM.LastTimeDecrease("Enemy", "Character");
        gM.buffM.LastTimeDecrease("Character", "Character");
        gM.combatSM.SwitchCombatState(gM.combatSM.endState);
    }
}
