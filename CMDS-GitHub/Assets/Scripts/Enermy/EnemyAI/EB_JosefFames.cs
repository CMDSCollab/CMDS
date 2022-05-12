using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EB_JosefFames : BasicEnemy
{
    public int maxHealth1P = 90;
    public int maxHealth2P = 100;
    public int damage1P = 10;
    public int damage2P = 5;
    private int enemySequence = 1;
    private int defaultSkill = 2;

    public override void TakeAction()
    {
        switch (currentIntention)
        {
            case EnemyIntention.Defence:
                gM.buffM.SetBuff(EnemyBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.AddValue, 10,BuffSource.Enemy);
                break;
            case EnemyIntention.Skill:
                skillLv += defaultSkill;
                //gM.buffM.SetBuff(EnemyBuff.Skill, BuffTimeType.Permanent, 999, BuffValueType.AddValue, defaultSkill, BuffSource.Enemy);
                MainChaMCChange();
                break;
            case EnemyIntention.FireShoot:
          
                if (gM.buffM.FindBuff(CharacterBuff.Inflammable) != null)
                {
                    gM.characterM.mainCharacter.TakeDamage(gM.buffM.EnemyAttack(damage1P*2));
                }
                else
                {
                    gM.characterM.mainCharacter.TakeDamage(gM.buffM.EnemyAttack(damage1P));
                }
                break;
            case EnemyIntention.HoneyShoot:
                gM.characterM.mainCharacter.TakeDamage(gM.buffM.EnemyAttack(damage2P));
                gM.buffM.SetBuff(CharacterBuff.Inflammable, BuffTimeType.Temporary, 3, BuffValueType.NoValue, 1, BuffSource.Enemy);
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
                if (enemySequence == 1)
                {
                    currentIntention = EnemyIntention.FireShoot;
                    transform.Find("Intention").Find("Value").GetComponent<Text>().text = damage1P.ToString();
                }
                else
                {
                    currentIntention = EnemyIntention.HoneyShoot;
                    transform.Find("Intention").Find("Value").GetComponent<Text>().text = damage2P.ToString();
                }             
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
        SetIntentionUI();
    }

    public override void InitializeEnemyUI()
    {
        enemyName = transform.Find("Name").GetComponent<Text>();
        portrait = transform.Find("Portrait").GetComponent<Image>();
        hpBar = transform.Find("HpBar").GetComponent<Slider>();
        hpRatio = transform.Find("HpBar").Find("HpRatio").GetComponent<Text>();

        if (enemySequence == 1)
        {
            portrait.sprite = enemyInfo.images[0];
            enemyName.text = "Josef Fames 1P";
            maxHp = maxHealth1P;
        }
        else
        {
            portrait.sprite = enemyInfo.images[1];
            enemyName.text = "Josef Fames 2P";
            maxHp = maxHealth2P;
        }
        healthPoint = maxHp;
        hpBar.maxValue = maxHp;
        hpBar.value = healthPoint;
        hpRatio.text = healthPoint.ToString() + "/" + maxHp.ToString();
        skillLv = enemyInfo.defaultSkill;

        GenerateEnemyIntention();
        MainChaMCChange();
    }

    public override void EnemyDefeated()
    {
        if (gM.buffM.FindBuff(EnemyBuff.Revive) == null)
        {
            if (enemySequence == 1)
            {
                enemySequence = 2;
            }
            else
            {
                enemySequence = 1;
            }
            InitializeEnemyUI();
            gM.buffM.SetBuff(EnemyBuff.Revive, BuffTimeType.Temporary, 4, BuffValueType.NoValue, 1, BuffSource.Enemy);
        }
    }
}
