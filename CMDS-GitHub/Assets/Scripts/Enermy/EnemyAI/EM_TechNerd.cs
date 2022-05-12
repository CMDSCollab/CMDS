using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EM_TechNerd : BasicEnemy
{
    private int defaultShieldP = 10;
    private int recordShieldP;
    private int defaultDmg = 10;
    private int defaultMultAttackTimes = 10;
    private int defaultSkill = 1;
    private bool isBlocked = false;
    public bool isCharged = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void TakeDamage(int dmgValue)
    {
        if (isBlocked)
        {
            return;
        }

        //if (recordShieldP > 0)
        //{
        //    recordShieldP -= dmgValue;
        //    if (recordShieldP > 0)
        //    {
        //        gM.buffM.SetEnemyBuff(EnemyBuff.Defence, true, recordShieldP);
        //    }
        //    if (recordShieldP < 0)
        //    {
        //        healthPoint += recordShieldP;
        //        gM.buffM.SetEnemyBuff(EnemyBuff.Defence, true, 0);
        //    }
        //    if (recordShieldP == 0)
        //    {
        //        gM.buffM.SetEnemyBuff(EnemyBuff.Defence, true, 0);
        //    }
        //}
        //else
        //{
        //    healthPoint -= dmgValue;
        //}

        healthPoint -= gM.buffM.EnemyTakeDamage(dmgValue);
    }

    public override void TakeAction()
    {
        isBlocked = false;
        //gM.buffM.SetEnemyBuff(EnemyBuff.Block, false, 0);
        gM.buffM.SetBuff(EnemyBuff.Block, BuffTimeType.Temporary, 1, BuffValueType.NoValue, 0, BuffSource.Enemy);

        switch (currentIntention)
        {
            case EnemyIntention.Attack:
                if (isCharged)
                {
                    gM.characterM.mainCharacter.TakeDamage(gM.buffM.EnemyAttack(defaultDmg * 2));
                    isCharged = false;
                    //gM.buffM.SetEnemyBuff(EnemyBuff.Charge, false, 0);
                    gM.buffM.SetBuff(EnemyBuff.Charge, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 0, BuffSource.Enemy);
                }
                else
                {
                    gM.characterM.mainCharacter.TakeDamage(gM.buffM.EnemyAttack(defaultDmg));
                }
                break;
            case EnemyIntention.Defence:
                //recordShieldP = defaultShieldP;
                //gM.buffM.SetEnemyBuff(EnemyBuff.Defence, true, recordShieldP);
                gM.buffM.SetBuff(EnemyBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.SetValue, defaultShieldP, BuffSource.Enemy);
                break;
            case EnemyIntention.MultiAttack:

                int slightDmg = (int)(defaultDmg * 0.1f);
                if (isCharged)
                {
                    slightDmg *= 2;
                }

                for (int i = 0; i < defaultMultAttackTimes; i++)
                {
                    gM.characterM.mainCharacter.TakeDamage(gM.buffM.EnemyAttack( slightDmg));
                }
                isCharged = false;
                gM.buffM.SetBuff(EnemyBuff.Charge, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 0, BuffSource.Enemy);
                //gM.buffM.SetEnemyBuff(EnemyBuff.Charge, false, 0);
                break;
            case EnemyIntention.Skill:
                skillLv += 1;
                //gM.buffM.SetEnemyBuff(EnemyBuff.Skill, true, skillLv);
                MainChaMCChange();
                break;
            case EnemyIntention.Charge:
                isCharged = true;
                //gM.buffM.SetEnemyBuff(EnemyBuff.Charge, false, 1);
                gM.buffM.SetBuff(EnemyBuff.Charge, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 1, BuffSource.Enemy);
                break;
            case EnemyIntention.Block:
                isBlocked = true;
                //gM.buffM.SetEnemyBuff(EnemyBuff.Block, false, 1);
                gM.buffM.SetBuff(EnemyBuff.Block, BuffTimeType.Temporary, 1, BuffValueType.NoValue, 1, BuffSource.Enemy);
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
            case EnemyIntention.MultiAttack:
                transform.Find("Intention").Find("Value").gameObject.SetActive(false);
                break;
            case EnemyIntention.Charge:
                transform.Find("Intention").Find("Value").gameObject.SetActive(false);
                transform.Find("Intention").Find("Value").GetComponent<Text>().text = defaultMultAttackTimes.ToString();
                break;
            case EnemyIntention.Skill:
                transform.Find("Intention").Find("Value").gameObject.SetActive(true);
                transform.Find("Intention").Find("Value").GetComponent<Text>().text = defaultSkill.ToString();
                break;
            case EnemyIntention.Block:
                transform.Find("Intention").Find("Value").gameObject.SetActive(false);
                break;
        }
    }
}
