using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AS_Attack : ActionBaseState
{
    private EnemyBuff[] eBuffs = { EnemyBuff.Weak };
    private CharacterBuff[] cBuffs = { CharacterBuff.Weak };

    public override void EnterState(GameMaster gM, int value)
    {
        gM.buffSM.valueToCalculate = value;
        if (gM.combatSM.currentState == gM.combatSM.enemyState)
        {
            gM.buffSM.SetBuffList(eBuffs);
        }
        else
        {
            Debug.Log("entered");
            gM.buffSM.SetBuffList(cBuffs);
        }
        gM.buffSM.BuffEffectsApply();
    }

    public override void BeforeUpdate(GameMaster gM, int value)
    {
        gM.actionSM.changedValue = value;
        gM.actionSM.isUpdate = true;
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
                EndState(gM, value);
                gM.animC.isAnimEnd = false;
                gM.animC.isAnimEntered = false;
            }
        }
        else
        {
            //AI IntentionÖ´ÐÐµÄ¶¯»­
            if (gM.animC.isAnimEntered == false)
            {
                gM.animC.SetScaleBounceValue(CurrentAITarget(gM).Find("IntentionPos").Find("Image").GetComponent<RectTransform>(), 1f, 0.6f, 1f);
                gM.animC.currentChoice = AnimChoice.ScaleBounce;
            }
            if (gM.animC.isAnimEnd == true)
            {
                gM.actionSM.isUpdate = false;
                EndState(gM, value);
                gM.animC.isAnimEnd = false;
                gM.animC.isAnimEntered = false;
            }
        }
    }

    public override void AfterUpdate(GameMaster gM, int value)
    {
        throw new System.NotImplementedException();
    }

    public override void EndState(GameMaster gM, int value)
    {
        gM.actionSM.EnterActionState(gM.actionSM.takeDmgState, value);
    }
}
