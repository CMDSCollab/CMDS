using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Vulnerable : BuffBaseState
{
    public int changeAmount = 3;

    public override void EnterState(GameMaster gM)
    {
        gM.buffSM.valueToCalculate += changeAmount;
        gM.buffSM.isUpdate = true;
    }

    public override void UpdateState(GameMaster gM)
    {
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
    }

    public override void EndState(GameMaster gM)
    {
        gM.buffSM.BuffEffectsApply();
    }
}
