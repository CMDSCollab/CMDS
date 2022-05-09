using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameMaster gM;
    public GlobalMaster globalM;
    public CardRepoManager cardRepoM;

    public Button drawPileButton;
    public Button discardPileButton;
  

    void Start()
    {
        gM = FindObjectOfType<GameMaster>();

    }

    void Update()
    {
        
    }

    public void OnDrawPileClicked()
    {
        cardRepoM.PresentDrawPile();
    }

    public void OnDicardPileClicked()
    {
        cardRepoM.PresentDiscardPile();
    }

    public void OnCardLayoutReturnClicked()
    {
        cardRepoM.RemoveCardsFromLayout();
        cardRepoM.gameObject.SetActive(false);
    }

    public void SynchronizeCardsCountInPileButton(string whichPileNumber)
    {
        if (whichPileNumber == "Draw")
        {

            drawPileButton.transform.Find("DrawPileNumber").GetComponent<Text>().text = gM.deckM.cardInDeckCopy.Count.ToString();
        }
        else if (whichPileNumber == "Discard")
        {
            discardPileButton.transform.Find("DiscardPileNumber").GetComponent<Text>().text = cardRepoM.discardPile.Count.ToString();
        }
       
    }

    public void OnClickNextTurn()
    {
        // 对玩家角色进行回合末结算
        if (gM.characterM.mainCharacterType == CharacterType.Programmmer)
        {
            gM.aiM.pro.OnPlayerTurnEnded();
        }

        //手牌丢弃

        if (gM.handM.handCardList.Count > 0)
        {
            for(int i = gM.handM.handCardList.Count; i >0; i-- )
            {
                gM.handM.handCardList[i - 1].GetComponent<CardManager>().DiscardHandCard();
            }
        }

        // AI队友行动
        switch (gM.characterM.mainCharacterType)
        {
            case CharacterType.Designer:
                if (gM.aiM.proAI.energyPoint == gM.aiM.proAI.energySlotAmount)
                {
                    gM.aiM.proAI.TakeAction();
                }
                if (gM.aiM.artAI.energyPoint == gM.aiM.artAI.energySlotAmount)
                {
                    gM.aiM.artAI.TakeAction();
                }
                break;
            case CharacterType.Programmmer:
                if (gM.aiM.desAI.energyPoint == gM.aiM.desAI.energySlotAmount)
                {
                    gM.aiM.desAI.TakeAction();
                }
                if (gM.aiM.artAI.energyPoint == gM.aiM.artAI.energySlotAmount)
                {
                    gM.aiM.artAI.TakeAction();
                }
                break;
            case CharacterType.Artist:
                if (gM.aiM.desAI.energyPoint == gM.aiM.desAI.energySlotAmount)
                {
                    gM.aiM.desAI.TakeAction();
                }
                if (gM.aiM.proAI.energyPoint == gM.aiM.proAI.energySlotAmount)
                {
                    gM.aiM.proAI.TakeAction();
                }
                break;
        }


        //敌人回合开始 - 判定MC
        if(gM.characterM.mainCharacterType == CharacterType.Designer)
        {
            
        }
        //执行行动
        gM.enM.enemyTarget.TakeAction();

        gM.deckM.DrawCardFromDeckRandomly(gM.deckM.drawCardAmount);

        if (gM.deckM.cardInDeckCopy.Count < 1)
        {
            gM.deckM.GetNewCopyDeck();
            gM.cardRepoM.discardPile.Clear(); //清空弃牌堆内卡牌
        }

        gM.buttonM.SynchronizeCardsCountInPileButton("Discard"); //同步弃牌堆卡牌数量展示Text
        gM.buttonM.SynchronizeCardsCountInPileButton("Draw");
    }


    public void OnClickDesigner()
    {
        globalM.characterType = CharacterType.Designer;
        SceneManager.LoadScene("FightScene");
    }

    public void OnClickProgrammer()
    {
        globalM.characterType = CharacterType.Programmmer;
        SceneManager.LoadScene("FightScene");
    }

    public void OnClickArtist()
    {
        globalM.characterType = CharacterType.Artist;
        SceneManager.LoadScene("FightScene");
    }

    public void EndFightBackToMap()
    {
        switch (gM.characterM.mainCharacterType)
        {
            case CharacterType.Designer:
                Destroy(gM.aiM.proAI.gameObject);
                Destroy(gM.aiM.artAI.gameObject);
                Destroy(gM.aiM.des.gameObject);
                break;
            case CharacterType.Programmmer:
                break;
            case CharacterType.Artist:
                break;
        }
        Destroy(gM.enM.enemyTarget.gameObject);
        gM.handM.handCardList.Clear();
        GameObject handObj = gM.uiCanvas.transform.Find("Hand").gameObject;
        for (int i = 0; i < handObj.transform.childCount; i++)
        {
            Destroy(handObj.transform.GetChild(i).gameObject);
        }
        gM.mapM.gameObject.SetActive(true);
        gM.uiCanvas.gameObject.SetActive(false);
    }
}
