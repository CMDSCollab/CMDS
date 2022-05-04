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
        //�Ƚ���Ѫ���ж�����ϵ������Ѫ�ߣ�����Ѫ���һ�Ѫ��ͼΪtrueʱ�����ػ�Ѫstring��
        if(enemy.healthPoint < enemy.maxHp * 0.3 && enemy.isHealing)
        {
            return "Healing";
        }

        //������Ѫ�ж���Ϊtrueʱ���������幥����ͼ��
        if (SmellBlood())
        {
            return "SingleA";
        }

        enemy.GenerateInitialIntentionList();

        //�������ڰ�ȫѪ��ʱ���������healing����ͼ
        if (enemy.healthPoint > enemy.maxHp - enemy.healAmount)
        {
            RemoveIntention("Healing");
        }

        return GenerateRandomIntention();



        

    }

    //��Ѫ��������ɫ������ֵ�����н�ɫ��Ѫ�����Ա�һ�ε��幥����ɱʱ������true
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

    //�������˵�intentionList��ɾ��ָ��intention
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
