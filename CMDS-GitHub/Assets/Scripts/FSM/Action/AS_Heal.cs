using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AS_Heal : ActionBaseState
{
    private EnemyBuff[] eBuffs = { };
    private CharacterBuff[] cBuffs = { };
    public override void EnterState(GameMaster gM, int value)
    {
        gM.buffSM.valueToCalculate = value;
        gM.buffSM.SetBuffList(cBuffs);
        gM.buffSM.BuffEffectsApply();
    }

    public override void BeforeUpdate(GameMaster gM, int value)
    {
        gM.actionSM.changedValue = value;
        gM.characterM.mainCharacter.healthPoint += gM.actionSM.changedValue;
        if (gM.characterM.mainCharacter.healthPoint >= gM.characterM.mainCharacter.maxHp)
        {
            gM.characterM.mainCharacter.healthPoint = gM.characterM.mainCharacter.maxHp;
        }
        gM.actionSM.isUpdate = true;
    }

    public override void UpdateState(GameMaster gM, int value)
    {
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

    public override void AfterUpdate(GameMaster gM, int value)
    {
        throw new System.NotImplementedException();
    }

    public override void EndState(GameMaster gM, int value)
    {
        gM.combatSM.isUpdate = true;
    }

    //private Transform CurrentAITarget(GameMaster gM)
    //{
    //    if (gM.combatSM.currentState == gM.combatSM.ai1State)
    //    {
    //        switch (gM.characterM.mainCharacterType)
    //        {
    //            case CharacterType.Designer:
    //                return gM.aiM.proAI.transform;
    //            case CharacterType.Programmmer:
    //                return gM.aiM.desAI.transform;
    //            case CharacterType.Artist:
    //                return gM.aiM.desAI.transform;
    //            default:
    //                return gM.aiM.des.transform;
    //        }
    //    }
    //    else if (gM.combatSM.currentState == gM.combatSM.ai2State)
    //    {
    //        switch (gM.characterM.mainCharacterType)
    //        {
    //            case CharacterType.Designer:
    //                return gM.aiM.artAI.transform;
    //            case CharacterType.Programmmer:
    //                return gM.aiM.artAI.transform;
    //            case CharacterType.Artist:
    //                return gM.aiM.proAI.transform;
    //            default:
    //                return gM.aiM.des.transform;
    //        }
    //    }
    //    else
    //    {
    //        return gM.aiM.des.transform;
    //    }
    //}
}
