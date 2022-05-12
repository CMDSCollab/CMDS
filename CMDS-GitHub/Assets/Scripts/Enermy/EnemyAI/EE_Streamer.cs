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
        // 当自身有部分无敌（也就是正在评价的时候）
        if (gM.buffM.FindBuff(EnemyBuff.PartialInvincibility) != null)
        {
            if (true)
            //如果伤害来源是指定角色，正常受到伤害
            {
                base.TakeDamage(dmgValue);
                // 如果破盾，则设置hasBeenImpressed
                if (gM.buffM.FindBuff(EnemyBuff.Defence) != null)
                {
                    if (gM.buffM.FindBuff(EnemyBuff.Defence).value <= 0)
                    {
                        hasBeenImpressed = true;
                    }
                }
            }
            else
            // 否则，伤害为0
            {
                base.TakeDamage(0);
                gM.buffM.EnemyTakeDamage(0); 

            }
        }
        // 其余情况正常受到伤害
        else
        {
            base.TakeDamage(dmgValue);
        }
    }

    public override void TakeAction()
    {
        if (!charToScoreDic.ContainsValue(0))
        {
            TakeDamage(999);
            Debug.Log("所有评价已完成，战斗结束");
        }


        switch (currentIntention)
        {
            case EnemyIntention.Attack:
                gM.characterM.mainCharacter.TakeDamage(defaultDmg);
                break;

            case EnemyIntention.ToComment:

                gM.buffM.SetBuff(EnemyBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.AddValue, sheildOnComment,BuffSource.Enemy);
                sheildOnComment += 10;
                
                hasBeenImpressed = false;
                gM.buffM.SetBuff(EnemyBuff.PartialInvincibility, BuffTimeType.Temporary, 1, BuffValueType.NoValue, 1, BuffSource.Enemy);
                break;
                
            case EnemyIntention.Comment:
                if (hasBeenImpressed)
                {
                    gM.characterM.mainCharacter.HealSelf(10);
                    charToScoreDic[characterToComment] = 2;
                    Debug.Log(characterToComment.ToString()+"获得了好评");
                }
                else
                {
                    gM.buffM.SetCharacterBuff(CharacterBuff.Weak, false, 1);
                    charToScoreDic[characterToComment] = 1;
                    Debug.Log(characterToComment.ToString() + "获得了中评");
                }

                break;
        }
        this.GenerateEnemyIntention();
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
                transform.Find("Intention").Find("Value").gameObject.SetActive(false);
                break;
            case EnemyIntention.Comment:
                transform.Find("Intention").Find("Value").gameObject.SetActive(false);
                break;
        }
        SetIntentionUI();
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
