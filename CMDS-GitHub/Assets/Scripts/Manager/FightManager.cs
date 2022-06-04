using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public GameMaster gM;

    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    public void FightProcessManager()
    {
        // 对玩家角色进行回合末结算
        if (gM.characterM.mainCharacterType == CharacterType.Programmmer)
        {
            gM.aiM.pro.OnPlayerTurnEnded();
        }
        else if(gM.characterM.mainCharacterType == CharacterType.Designer)
        {
            gM.aiM.des.ChallengeDMG();
        }
        else if(gM.characterM.mainCharacterType == CharacterType.Artist)
        {
            gM.aiM.art.StyleEffect();
        }

        //手牌丢弃

        if (gM.handM.handCardList.Count > 0)
        {
            for (int i = gM.handM.handCardList.Count; i > 0; i--)
            {
                gM.handM.handCardList[i - 1].GetComponent<CardManager>().DiscardHandCard();
            }
        }

        // AI队友行动
        switch (gM.characterM.mainCharacterType)
        {
            case CharacterType.Designer:
                gM.aiM.proAI.TakeAction();
                gM.aiM.artAI.TakeAction();
                break;
            case CharacterType.Programmmer:
                gM.aiM.desAI.TakeAction();
                gM.aiM.artAI.TakeAction();
                break;
            case CharacterType.Artist:
                gM.aiM.desAI.TakeAction();
                gM.aiM.proAI.TakeAction();
                break;
        }

        gM.buffM.LastTimeDecrease("Character", "Enemy");
        gM.buffM.LastTimeDecrease("Enemy", "Enemy");

        // 执行行动
        gM.enM.enemyTarget.TakeAction();

        gM.buffM.LastTimeDecrease("Enemy", "Character");
        gM.buffM.LastTimeDecrease("Character", "Character");


        gM.deckM.DrawCardFromDeckRandomly(gM.deckM.drawCardAmount);

        if (gM.deckM.cardInDeckCopy.Count < 1)
        {
            gM.deckM.GetNewCopyDeck();
            gM.cardRepoM.discardPile.Clear(); //清空弃牌堆内卡牌
        }

        gM.buttonM.SynchronizeCardsCountInPileButton("Discard"); //同步弃牌堆卡牌数量展示Text
        gM.buttonM.SynchronizeCardsCountInPileButton("Draw");
    }
}
