using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_Hatchling : BasicEnemy
{
    private int defaultDmg = 8;
    private int sheildOnLowHP = 10;

    private bool hasTaunted = false;

    public override void GenerateEnemyIntention()
    {
        if (!hasTaunted)
        {
            currentIntention = EnemyIntention.Taunt;
        }
        else if (healthPoint < 0.5*maxHp)
        {
            currentIntention = EnemyIntention.Defence;
        }
        else
        {
            base.GenerateEnemyIntention();
        }
    }

    public override void TakeAction()
    {
        switch (currentIntention)
        {
            case EnemyIntention.Attack:
                gM.characterM.mainCharacter.TakeDamage(gM.buffM.EnemyAttack(defaultDmg));
                break;
            case EnemyIntention.Defence:
                gM.buffM.SetBuff(EnemyBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.SetValue, sheildOnLowHP, BuffSource.Enemy);
                break;
            case EnemyIntention.Taunt:
                gM.buffM.SetBuff(CharacterBuff.Vulnerable, BuffTimeType.Temporary, 2, BuffValueType.NoValue, 1, BuffSource.Enemy);
                hasTaunted = true;
                break;
        }
    }
}
