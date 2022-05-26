using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardS_TakeDmg : CardBaseState
{
    public override void EnterState(GameMaster gM, int value)
    {
        gM.enM.enemyTarget.healthPoint -= value;
        isUpdate = true;
    }

    public override void UpdateState(GameMaster gM, int value)
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
            isUpdate = false;
            EndState(gM, value);
        }
    }

    public override void EndState(GameMaster gM, int value)
    {
        Debug.Log("takeDmgEnd");
    }
}
