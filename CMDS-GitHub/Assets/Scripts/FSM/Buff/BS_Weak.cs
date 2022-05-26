using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Weak : BuffBaseState
{
    public int changeAmount = -3;

    public override void EnterState(GameMaster gM)
    {
        switch (gM.buffSM.buffUsage)
        {
            case BuffUsage.EffectApply:
                gM.buffSM.valueToCalculate += changeAmount;
                break;
            case BuffUsage.AddNew:
                if (gM.actionSM.currentState == gM.actionSM.tauntState)
                {
                    gM.buffSM.AddNewBuff(CharacterBuff.Weak, BuffTimeType.Temporary, 1, BuffValueType.NoValue, 1, BuffSource.Enemy);
                    gM.buffSM.buffTrans = gM.buffSM.GetBuffRectTrans(CharacterBuff.Weak);
                    gM.buffSM.buffTrans.localScale = new Vector3(0, 0, 0);
                }
                else
                {
                    gM.buffSM.AddNewBuff(EnemyBuff.Weak, BuffTimeType.Temporary, 1, BuffValueType.NoValue, 1, BuffSource.Enemy);
                    gM.buffSM.buffTrans = gM.buffSM.GetBuffRectTrans(EnemyBuff.Weak);
                    gM.buffSM.buffTrans.localScale = new Vector3(0, 0, 0);
                }
                break;
        }
        gM.buffSM.isUpdate = true;
    }

    public override void UpdateState(GameMaster gM)
    {
        switch (gM.buffSM.buffUsage)
        {
            case BuffUsage.EffectApply:
                if (gM.animC.isAnimEntered == false)
                {
                    gM.animC.SetScaleBounceValue(gM.buffSM.buffTrans, 1f, 0.6f, 1f);
                    gM.animC.currentChoice = AnimChoice.ScaleBounce;
                }
                if (gM.animC.isAnimEnd == true)
                {
                    gM.buffSM.isUpdate = false;
                    EndState(gM);
                    gM.animC.isAnimEnd = false;
                    gM.animC.isAnimEntered = false;
                }
                break;
            case BuffUsage.AddNew:
                if (gM.animC.isAnimEntered == false)
                {
                    gM.animC.SetBuffValueChangeValue(gM.buffSM.buffTrans, 0f, 1.2f, 1f, 1);
                    gM.animC.currentChoice = AnimChoice.BuffValueChange;
                }
                if (gM.animC.isAnimEnd == true)
                {
                    gM.buffSM.isUpdate = false;
                    EndState(gM);
                    gM.animC.isAnimEnd = false;
                    gM.animC.isAnimEntered = false;
                }
                break;
        }
    }

    public override void EndState(GameMaster gM)
    {
        switch (gM.buffSM.buffUsage)
        {
            case BuffUsage.AddNew:
                gM.actionSM.currentState.EndState(gM, gM.buffSM.valueToCalculate);
                break;
            case BuffUsage.EffectApply:
                gM.buffSM.BuffEffectsApply();
                break;
        }
    }
}
