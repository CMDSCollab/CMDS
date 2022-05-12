using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EB_JosefFames : BasicEnemy
{
    public float maxHealth1P;
    public float maxHealth2P;
    public int damage1P = 10;
    public int damage2P = 5;
    public int respawnTurn;
    private int enemySequence = 1;
    private int respawnCount;
    private int defaultSkill = 2;

    public override void TakeAction()
    {
        switch (currentIntention)
        {
            case EnemyIntention.Attack:
                gM.characterM.mainCharacter.TakeDamage(gM.buffM.EnemyAttack(damage1P));
                break;
            case EnemyIntention.Defence:
                gM.buffM.SetBuff(EnemyBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.AddValue, 10);
                //recordShieldP += defaultShieldP;
                //gM.buffM.SetEnemyBuff(EnemyBuff.Defence, true, recordShieldP);
                break;
            case EnemyIntention.Skill:
                skillLv += defaultSkill;
                //gM.buffM.SetEnemyBuff(EnemyBuff.Skill, true, skillLv);
                gM.buffM.SetBuff(EnemyBuff.Skill, BuffTimeType.Permanent, 999, BuffValueType.AddValue, defaultSkill);
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
                transform.Find("Intention").Find("Value").GetComponent<Text>().text = 10.ToString();
                break;
            case EnemyIntention.Defence:
                transform.Find("Intention").Find("Value").gameObject.SetActive(true);
                transform.Find("Intention").Find("Value").GetComponent<Text>().text = 10.ToString();
                break;
            case EnemyIntention.Skill:
                transform.Find("Intention").Find("Value").gameObject.SetActive(true);
                transform.Find("Intention").Find("Value").GetComponent<Text>().text = defaultSkill.ToString();
                break;
        }
    }

    public override void EnemyDefeated()
    {

        base.EnemyDefeated();
    }
}
