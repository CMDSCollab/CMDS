using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_AI1 : CombatBaseState
{
    public override void EnterState(GameMaster gM)
    {
        //Debug.Log("AI1CSEntered");
        switch (gM.characterM.mainCharacterType)
        {
            case CharacterType.Designer:
                gM.aiM.proAI.TakeAction();
                break;
            case CharacterType.Programmmer:
                gM.aiM.desAI.TakeAction();
                break;
            case CharacterType.Artist:
                gM.aiM.desAI.TakeAction();
                break;
        }
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
        gM.combatSM.SwitchCombatState(gM.combatSM.ai2State);
    }
}
