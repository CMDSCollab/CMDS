using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EM_Granny : BasicEnemy
{
    [SerializeField] private Sprite intentUnknownImg;

    private int defaultDmg = 11;
    private int healAmount = 6;

    public override void GenerateEnemyIntention()
    {
        List<int> tendencyValues = new List<int>();
        foreach (EnemyIntentionRatio ratio in enemyInfo.basicIntentions)
        {
            tendencyValues.Add(ratio.tendency);
        }
        int random = Random.Range(0, 100);
        for (int i = 0; i < tendencyValues.Count; i++)
        {
            if (random < tendencyValues[i])
            {
                currentIntention = enemyInfo.basicIntentions[i].intention;
                break;
            }
            else
            {
                random -= tendencyValues[i];
            }
        }

        SetIntentionImage();
    }

    private void SetIntentionImage()
    {
        transform.Find("Intention").Find("Name").GetComponent<Text>().text = "Unknown";
        transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = intentUnknownImg; 
    }

    public override void TakeAction()
    {
        TryEscapeMC();

        switch (currentIntention)
        {
            case EnemyIntention.Attack:
                gM.characterM.mainCharacter.TakeDamage(gM.buffM.EnemyAttack(defaultDmg));
                break;
            case EnemyIntention.Heal:
                gM.enM.enemyTarget.Heal(healAmount);
                break;
        }
    }

    private void TryEscapeMC()
    {
        if (magicCircleState == MagicCircleState.In)
        {
            MagicCircleDropOut(50);
            if (magicCircleState == MagicCircleState.Out)
            {
                Debug.Log("老奶奶玩不明白！Magic Circle已掉出");
            }
        }
    }
}
