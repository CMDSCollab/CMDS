using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EM_ESPlayerMature : BasicEnemy
{
    private int defaultShieldP = 10;
    private int recordShieldP;
    private int defaultDmg = 10;

    void Start()
    {
        
    }

    public override void TakeDamage(int dmgValue)
    {
        if (recordShieldP > 0)
        {
            recordShieldP -= dmgValue;
            if (recordShieldP > 0)
            {
                gM.buffM.SetEnemyBuff(EnemyBuff.Defence, true, recordShieldP);
            }
            if (recordShieldP < 0)
            {
                healthPoint += recordShieldP;
                gM.buffM.SetEnemyBuff(EnemyBuff.Defence, true, 0);
            }
            if (recordShieldP == 0)
            {
                gM.buffM.SetEnemyBuff(EnemyBuff.Defence, true, 0);
            }
        }
        else
        {
            healthPoint -= dmgValue;
        }
    }

    public override void TakeAction()
    {
        switch (currentIntention)
        {
            case EnemyIntention.Attack:
                gM.characterM.mainCharacter.TakeDamage(defaultDmg);
                break;
            case EnemyIntention.Defence:
                recordShieldP = defaultShieldP;
                gM.buffM.SetEnemyBuff(EnemyBuff.Defence, true, recordShieldP);
                break;
            case EnemyIntention.Taunt:
                gM.buffM.SetCharacterBuff(CharacterBuff.Weak, false, 1);
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
        }
    }
}
