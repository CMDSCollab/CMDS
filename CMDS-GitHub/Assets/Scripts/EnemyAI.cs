using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Enemy enemy;

    public bool isSmart;

    public void GenerateIntention()
    {
        if (isSmart)
        {
            enemy.currentIntention = GenerateSmartIntention();
        }
        else
        {
            enemy.currentIntention = GenerateRandomIntention();
        }

        enemy.ShowIntention();
    }


    public string GenerateRandomIntention()
    {
        int index = Random.Range(0, enemy.intentionList.Count);
        return enemy.intentionList[index];
    }

    public string GenerateSmartIntention()
    {
        //先进行血量判定，由系数控制血线，低于血线且回血意图为true时，返回回血string。
        if(enemy.healthPoint < enemy.maxHp * 0.3 && enemy.isHealing)
        {
            return "Healing";
        }

        //进行嗅血判定，为true时，产生单体攻击意图。
        if (SmellBlood())
        {
            return "SingleA";
        }

        enemy.GenerateInitialIntentionList();

        //当敌人在安全血线时，不会产生healing的意图
        if (enemy.healthPoint > enemy.maxHp - enemy.healAmount)
        {
            RemoveIntention("Healing");
        }

        return GenerateRandomIntention();



        

    }

    //嗅血：遍历角色的生命值，当有角色的血量可以被一次单体攻击击杀时，返回true
    public bool SmellBlood()
    {
        foreach(BasicCharacter cha in enemy.gM.characterM.charactersList)
        {
            if(cha.healthPoint < enemy.singleDmg)
            {
                return true;
            }
        }

        return false;
    }

    //遍历敌人的intentionList，删除指定intention
    public void RemoveIntention(string intention)
    {
        for(int i = 0; i < enemy.intentionList.Count; i++)
        {
            if(enemy.intentionList[i] == intention)
            {
                enemy.intentionList.RemoveAt(i);
            }
        }
    }
}
