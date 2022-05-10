using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EM_ESPlayerMature : BasicEnemy
{
    private int defaultShieldP = 10;
    public int recordShieldP;
    private int defaultDmg = 10;
    private int defaultSkill = 1;

    void Start()
    {
        gM.buffM.SetEnemyBuff(EnemyBuff.Skill, true, skillLv);
    }

    public override int DmgValueCalculation(int dmgValue)
    {
        if (recordShieldP > 0)
        {
            recordShieldP -= dmgValue;
            if (recordShieldP >= 0)
            {
                if (recordShieldP>0)
                {
                    gM.buffM.SetEnemyBuff(EnemyBuff.Defence, true, recordShieldP);
                    return 0;
                }
                else
                {
                    gM.buffM.SetEnemyBuff(EnemyBuff.Defence, true, 0);
                    return 0;
                }
            }
            else
            {
                gM.buffM.SetEnemyBuff(EnemyBuff.Defence, true, 0);
                return dmgValue -= recordShieldP;
            }
        }
        else
        {
            return dmgValue;
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
                recordShieldP += defaultShieldP;
                gM.buffM.SetEnemyBuff(EnemyBuff.Defence, true, recordShieldP);
                break;
            case EnemyIntention.Taunt:
                gM.buffM.SetCharacterBuff(CharacterBuff.Weak, false, 1);
                break;
            case EnemyIntention.Skill:
                skillLv += 1;
                gM.buffM.SetEnemyBuff(EnemyBuff.Skill, true, skillLv);
                MainChaMCChange();
                break;
        }
        GenerateEnemyIntention();
    }

    public override void GenerateEnemyIntention()
    {
        base.GenerateEnemyIntention();
        switch (currentIntention)
        {
            case EnemyIntention.Attack:
                transform.Find("Intention").Find("Value").gameObject.SetActive(true);
                transform.Find("Intention").Find("Value").GetComponent<Text>().text = defaultDmg.ToString();
                break;
            case EnemyIntention.Defence:
                transform.Find("Intention").Find("Value").gameObject.SetActive(true);
                transform.Find("Intention").Find("Value").GetComponent<Text>().text = defaultShieldP.ToString();
                break;
            case EnemyIntention.Taunt:
                transform.Find("Intention").Find("Value").gameObject.SetActive(false);
                break;
            case EnemyIntention.Skill:
                transform.Find("Intention").Find("Value").gameObject.SetActive(true);
                transform.Find("Intention").Find("Value").GetComponent<Text>().text = defaultSkill.ToString();
                break;
        }
    }
}
