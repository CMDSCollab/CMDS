using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_Hoarder : BasicEnemy
{
    private int defaultDmg = 12;

    private int goldBonus = 50;
    private int sheildOnSleep = 30;
    private bool hasBeenAwaken = false;

    public override void TakeDamage(int dmgValue)
    {
        base.TakeDamage(dmgValue);
        if (!hasBeenAwaken)
        {
            if (gM.buffM.FindBuff(EnemyBuff.Defence) != null)
            {
                if (gM.buffM.FindBuff(EnemyBuff.Defence).value <= 0)
                {
                    this.SetAwaken();
                }
            }
            else
            {
                this.SetAwaken();
            }

        }

    }

    private void SetAwaken()
    {
        hasBeenAwaken = true;
        GenerateEnemyIntention();
        Debug.Log("Ï²+1µ³ ÒÑËÕÐÑ£¡");
    }

    public override void GenerateEnemyIntention()
    {
        if (!hasBeenAwaken)
        {
            currentIntention = EnemyIntention.Sleep;
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
            case EnemyIntention.Sleep:
                gM.buffM.SetBuff(EnemyBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.AddValue, sheildOnSleep, BuffSource.Enemy);
                sheildOnSleep -= 2;
                goldBonus -= 5;
                break;
            case EnemyIntention.Attack:
                gM.characterM.mainCharacter.TakeDamage(gM.buffM.EnemyAttack(defaultDmg));
                break;
            case EnemyIntention.Taunt:
                gM.buffM.SetBuff(CharacterBuff.Vulnerable, BuffTimeType.Temporary, 2, BuffValueType.NoValue, 1, BuffSource.Enemy);
                break;
        }
    }
}
