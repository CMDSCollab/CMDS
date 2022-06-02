using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardS_Attack : CardBaseState
{
    float playerAtkTime = 5;
    public override void EnterState(GameMaster gM, int value)
    {
        gM.cardSM.changedValue = value;
        isUpdate = true;
    }

    public override void UpdateState(GameMaster gM, int value)
    {
        Debug.Log("PlayerAttack");
        playerAtkTime -= Time.deltaTime;
        if (playerAtkTime <= 0)
        {
            playerAtkTime = 5;
            isUpdate = false;
            EndState(gM, value);
        }
    }

    public override void EndState(GameMaster gM, int value)
    {
        gM.cardSM.EnterCardState(gM.cardSM.takeDmgState, value);
    }
}
