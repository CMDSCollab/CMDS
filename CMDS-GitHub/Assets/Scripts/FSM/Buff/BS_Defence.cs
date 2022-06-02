using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Defence : BuffBaseState
{
    public int changeAmount;
    public int defenceValue;

    public override void EnterState(GameMaster gM)
    {
        changeAmount = gM.buffSM.valueToCalculate;
        switch (gM.buffSM.buffUsage)
        {
            case BuffUsage.AddNew:
                defenceValue = changeAmount;
                if (gM.combatSM.currentState == gM.combatSM.enemyState)
                {
                    gM.buffSM.AddNewBuff(EnemyBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.AddValue, defenceValue, BuffSource.Enemy);
                    gM.buffSM.buffTrans = gM.buffSM.GetBuffRectTrans(EnemyBuff.Defence);
                    gM.buffSM.buffTrans.localScale = new Vector3(0, 0, 0);
                }
                else
                {
                    gM.buffSM.AddNewBuff(CharacterBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.AddValue, defenceValue, BuffSource.Character);
                    gM.buffSM.buffTrans = gM.buffSM.GetBuffRectTrans(CharacterBuff.Defence);
                    gM.buffSM.buffTrans.localScale = new Vector3(0, 0, 0);
                }
                break;
            case BuffUsage.Adjust:
                if (gM.combatSM.currentState == gM.combatSM.enemyState)
                {
                    defenceValue = gM.buffM.FindBuff(EnemyBuff.Defence).value;
                    defenceValue += changeAmount;
                }
                else
                {
                    defenceValue = gM.buffM.FindBuff(CharacterBuff.Defence).value;
                    defenceValue += changeAmount;
                }
                break;
            case BuffUsage.EffectApply:
                if (gM.combatSM.currentState == gM.combatSM.enemyState)
                {
                    defenceValue = gM.buffM.FindBuff(CharacterBuff.Defence).value;
                    if (defenceValue > changeAmount)
                    {
                        gM.buffM.FindBuff(CharacterBuff.Defence).value -= changeAmount;
                        gM.buffSM.valueToCalculate = 0;
                        defenceValue -= changeAmount;

                    }
                    else
                    {
                        gM.buffM.FindBuff(CharacterBuff.Defence).value -= changeAmount;
                        gM.buffSM.valueToCalculate -= defenceValue;
                        defenceValue = 0;
                    }
                }
                else
                {
                    defenceValue = gM.buffM.FindBuff(EnemyBuff.Defence).value;
                    if (defenceValue > changeAmount)
                    {
                        gM.buffM.FindBuff(EnemyBuff.Defence).value -= changeAmount;
                        gM.buffSM.valueToCalculate = 0;
                        defenceValue -= changeAmount;
                    }
                    else
                    {
                        gM.buffM.FindBuff(EnemyBuff.Defence).value -= changeAmount;
                        gM.buffSM.valueToCalculate -= defenceValue;
                        defenceValue = 0;
                    }
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
                    gM.animC.SetBuffValueChangeValue(gM.buffSM.buffTrans, 1f, 1.2f, 1f, defenceValue);
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
            case BuffUsage.AddNew:
                if (gM.animC.isAnimEntered == false)
                {
                    gM.animC.SetBuffValueChangeValue(gM.buffSM.buffTrans, 0f, 1.2f, 1f, defenceValue);
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
            case BuffUsage.Adjust:
                if (gM.animC.isAnimEntered == false)
                {
                    gM.animC.SetBuffValueChangeValue(gM.buffSM.buffTrans, 1f, 1.2f, 1f, defenceValue);
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
            case BuffUsage.Adjust:
                gM.actionSM.currentState.EndState(gM, gM.buffSM.valueToCalculate);
                break;
            case BuffUsage.EffectApply:
                gM.buffSM.BuffEffectsApply();
                break;
        }
    }
}
