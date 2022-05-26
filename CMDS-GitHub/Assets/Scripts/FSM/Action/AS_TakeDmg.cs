using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AS_TakeDmg : ActionBaseState
{
    private EnemyBuff[] eBuffs = { EnemyBuff.Vulnerable, EnemyBuff.Block, EnemyBuff.Defence };
    private CharacterBuff[] cBuffs = { CharacterBuff.Defence, CharacterBuff.Vengeance };

    public override void EnterState(GameMaster gM, int value)
    {
        gM.buffSM.valueToCalculate = value;
        if (gM.combatSM.currentState == gM.combatSM.enemyState)
        {
            gM.buffSM.SetBuffList(cBuffs);
            Debug.Log("CTakeDmgEntered");
        }
        else
        {
            gM.buffSM.SetBuffList(eBuffs);
            Debug.Log("ETakeDmgEntered");
        }
        //Debug.Log("cCount:"+gM.buffSM.characterBuffs.Count);
        //Debug.Log("eCount:" + gM.buffSM.enemyBuffs.Count);
        gM.buffSM.BuffEffectsApply();
    }

    public override void BeforeUpdate(GameMaster gM, int value)
    {
        gM.actionSM.changedValue = value;
        if (gM.combatSM.currentState == gM.combatSM.enemyState)
        {
            gM.characterM.mainCharacter.healthPoint -= gM.actionSM.changedValue;
        }
        else
        {
            gM.enM.enemyTarget.healthPoint -= gM.actionSM.changedValue;
        }
        gM.actionSM.isUpdate = true;
    }

    public override void UpdateState(GameMaster gM, int value)
    {
        if (gM.combatSM.currentState == gM.combatSM.enemyState)
        {
            float hpValue = Mathf.Lerp(gM.characterM.mainCharacter.hpBar.value, gM.characterM.mainCharacter.healthPoint, 0.005f);
            if ((hpValue - gM.characterM.mainCharacter.healthPoint) <= 0.5)
            {
                hpValue = gM.characterM.mainCharacter.healthPoint;
            }
            gM.characterM.mainCharacter.hpBar.value = hpValue;
            gM.characterM.mainCharacter.hpRatio.text = gM.characterM.mainCharacter.healthPoint.ToString() + "/" + gM.characterM.mainCharacter.maxHp.ToString();


            if (gM.characterM.mainCharacter.hpBar.value == gM.characterM.mainCharacter.healthPoint)
            {
                gM.actionSM.isUpdate = false;
                EndState(gM, value);
            }
        }
        else
        {
            float hpValue = Mathf.Lerp(gM.enM.enemyTarget.hpBar.value, gM.enM.enemyTarget.healthPoint, 0.005f);
            if ((hpValue - gM.enM.enemyTarget.healthPoint) <= 0.5)
            {
                hpValue = gM.enM.enemyTarget.healthPoint;
            }
            gM.enM.enemyTarget.hpBar.value = hpValue;
            gM.enM.enemyTarget.hpRatio.text = gM.enM.enemyTarget.healthPoint.ToString() + "/" + gM.enM.enemyTarget.maxHp.ToString();


            if (gM.enM.enemyTarget.hpBar.value == gM.enM.enemyTarget.healthPoint)
            {
                gM.actionSM.isUpdate = false;
                EndState(gM, value);
            }
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

}
