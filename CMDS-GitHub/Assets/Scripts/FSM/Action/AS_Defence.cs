using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AS_Defence : ActionBaseState
{
    private EnemyBuff[] eBuffs = { };
    private CharacterBuff[] cBuffs = { };
    public override void EnterState(GameMaster gM, int value)
    {
        gM.actionSM.changedValue = value;
        gM.actionSM.isUpdate = true;
    }

    public override void BeforeUpdate(GameMaster gM, int value)
    {

    }

    public override void UpdateState(GameMaster gM, int value)
    {
        if (gM.combatSM.currentState == gM.combatSM.enemyState)
        {
            if (gM.animC.isAnimEntered == false)
            {
                gM.animC.SetScaleBounceValue(gM.enM.enemyTarget.transform.Find("Intention").Find("Image").GetComponent<RectTransform>(), 1f, 0.6f, 1f);
                gM.animC.currentChoice = AnimChoice.ScaleBounce;
            }
            if (gM.animC.isAnimEnd == true)
            {
                gM.actionSM.isUpdate = false;
                gM.animC.isAnimEnd = false;
                gM.animC.isAnimEntered = false;
                AfterUpdate(gM, value);
            }
        }
        else
        {
            if (gM.animC.isAnimEntered == false)
            {
                gM.animC.SetScaleBounceValue(CurrentAITarget(gM).Find("IntentionPos").Find("Image").GetComponent<RectTransform>(), 1f, 0.6f, 1f);
                gM.animC.currentChoice = AnimChoice.ScaleBounce;
            }
            if (gM.animC.isAnimEnd == true)
            {
                gM.actionSM.isUpdate = false;
                gM.animC.isAnimEnd = false;
                gM.animC.isAnimEntered = false;
                AfterUpdate(gM, value);
            }
        }
    }

    public override void AfterUpdate(GameMaster gM, int value)
    {
        gM.buffSM.valueToCalculate = value;
        if (gM.combatSM.currentState == gM.combatSM.enemyState)
        {
            gM.buffSM.AddOrAdjustBuff(EnemyBuff.Defence);
        }
        else
        {
            gM.buffSM.AddOrAdjustBuff(CharacterBuff.Defence);
        }
        //if (gM.buffSM.isUpdate == false)
        //{
        //    Debug.Log("entered");
        //    EndState(gM, value);
        //}
    }

    public override void EndState(GameMaster gM, int value)
    {
        gM.combatSM.isUpdate = true;
    }
}
