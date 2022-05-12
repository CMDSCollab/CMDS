using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EM_ESPlayerMature : BasicEnemy
{
    private int defaultShieldP = 10;
    //public int recordShieldP;
    private int defaultDmg = 10;
    private int defaultSkill = 1;

    void Start()
    {

    }

    public override void TakeAction()
    {
        switch (currentIntention)
        {
            case EnemyIntention.Attack:
                gM.characterM.mainCharacter.TakeDamage(gM.buffM.EnemyAttack(defaultDmg));
                break;
            case EnemyIntention.Defence:
                gM.buffM.SetBuff(EnemyBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.SetValue, defaultShieldP,BuffSource.Enemy);
                //recordShieldP += defaultShieldP;
                //gM.buffM.SetEnemyBuff(EnemyBuff.Defence, true, recordShieldP);
                break;
            case EnemyIntention.Taunt:
                gM.buffM.SetBuff(CharacterBuff.Weak, BuffTimeType.Temporary, 1, BuffValueType.NoValue, 1, BuffSource.Enemy);
                //gM.buffM.SetCharacterBuff(CharacterBuff.Weak, false, 1);
                break;
            case EnemyIntention.Skill:
                skillLv += defaultSkill;
                //gM.buffM.SetEnemyBuff(EnemyBuff.Skill, true, skillLv);
                //gM.buffM.SetBuff(EnemyBuff.Skill, BuffTimeType.Permanent, 999, BuffValueType.AddValue, defaultSkill, BuffSource.Enemy);
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
