using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EE_Streamer : BasicEnemy
{
    private int recordShieldP;
    private int defaultDmg = 15;
    private int sheildOnComment = 20;

    // 存储三位角色的分数 0代表未评价 1代表中评 2代表好评
    private Dictionary<CharacterType, int> charToScoreDic;
    private CharacterType characterToComment;
    private bool hasBeenImpressed = false;
    private bool hasFinishedComments = false;


    private void Awake()
    {
        base.Awake();

        sheildOnComment = 20;

        charToScoreDic = new Dictionary<CharacterType, int>();
        charToScoreDic.Add(CharacterType.Designer, 0);
        charToScoreDic.Add(CharacterType.Artist, 0);
        charToScoreDic.Add(CharacterType.Programmmer, 0);
    }

    public override void TakeDamage(int dmgValue)
    {
        if (gM.buffM.activeEnemyBuffs.ContainsKey(EnemyBuff.PartialInvincibility))
        {
            if (!true)//对伤害来源进行判定
            {
                healthPoint -= 0;// 使用伤害为0
                return;
            }
        }


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
                hasBeenImpressed = true;
            }
            if (recordShieldP == 0)
            {
                gM.buffM.SetEnemyBuff(EnemyBuff.Defence, true, 0);
                hasBeenImpressed = true;
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

            case EnemyIntention.ToComment:
                
                gM.buffM.SetEnemyBuff(EnemyBuff.Defence, true, sheildOnComment);
                sheildOnComment += 10;
                
                hasBeenImpressed = false;
                gM.buffM.SetEnemyBuff(EnemyBuff.PartialInvincibility, false, 1);
                break;
                
            case EnemyIntention.Comment:
                if (hasBeenImpressed)
                {
                    gM.characterM.mainCharacter.HealSelf(10);
                }
                else
                {
                    gM.buffM.SetCharacterBuff(CharacterBuff.Weak, false, 1);
                }

                break;
        }
    }

    public override void GenerateEnemyIntention()
    {
        // 如果本回合已经准备评价了，则下回合评价
        if (currentIntention == EnemyIntention.ToComment)
        {
            currentIntention = EnemyIntention.Comment;
        }
        else
        {
            // 否则正常选择下回合Intention
            base.GenerateEnemyIntention();
            // 如果下回合是准备评价，则确定好评价的对象
            if (currentIntention == EnemyIntention.ToComment)
            {
                SelectCharToComment();
            }
        }

        switch (currentIntention)
        {
            case EnemyIntention.Attack:
                transform.Find("Intention").Find("Value").gameObject.SetActive(true);
                transform.Find("Intention").Find("Value").GetComponent<Text>().text = defaultDmg.ToString();
                break;
            case EnemyIntention.ToComment:
                transform.Find("Intention").Find("Value").gameObject.SetActive(true);
                break;
            case EnemyIntention.Comment:
                transform.Find("Intention").Find("Value").gameObject.SetActive(false);
                break;
        }
    }

    private void SelectCharToComment()
    {
        if (!charToScoreDic.ContainsValue(0))
        {
            return;
        }

        bool hasResult = false;
        switch (UnityEngine.Random.Range(0, 3))
        {
            case 0:
                if (charToScoreDic[CharacterType.Artist] == 0)
                {
                    characterToComment = CharacterType.Artist;
                    hasResult = true;
                }
                break;
            case 1:
                if (charToScoreDic[CharacterType.Designer] == 0)
                {
                    characterToComment = CharacterType.Designer;
                    hasResult = true;
                }
                break;
            case 2:
                if (charToScoreDic[CharacterType.Programmmer] == 0)
                {
                    characterToComment = CharacterType.Programmmer;
                    hasResult = true;
                }
                break;
        }

        if (!hasResult)
        {
            SelectCharToComment();
        }
    }
}
