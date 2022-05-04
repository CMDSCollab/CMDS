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
        // ����ҽ�ɫ���лغ�ĩ����
        if (gM.characterType == CharacterType.Programmmer)
        {
            gM.aiM.pro.OnPlayerTurnEnded();
        }

        //���ƶ���

        if (gM.handM.handCardList.Count > 0)
        {
            for(int i = gM.handM.handCardList.Count; i >0; i-- )
            {
                gM.handM.handCardList[i - 1].GetComponent<CardManager>().DiscardHandCard();
            }
        }

        // AI�����ж�
        gM.aiM.proAI.TakeAction();
        gM.aiM.artAI.TakeAction();

        //���˻غϿ�ʼ - �ж�MC
        if(gM.characterType == CharacterType.Designer)
        {
            
        }
        //ִ���ж�
        gM.enM.EnemiesActions();

        gM.deckM.DrawCardFromDeckRandomly(gM.deckM.drawCardAmount);

        if (gM.deckM.cardInDeckCopy.Count < 1)
        {
            gM.deckM.GetNewCopyDeck();
            gM.cardRepoM.discardPile.Clear(); //������ƶ��ڿ���
        }

        gM.buttonM.SynchronizeCardsCountInPileButton("Discard"); //ͬ�����ƶѿ�������չʾText
        gM.buttonM.SynchronizeCardsCountInPileButton("Draw");
    }


    public void OnClickDesigner()
    {
        //gM.characterM.chosenCharacter = "Designer";
        globalM.characterType = CharacterType.Designer;
        SceneManager.LoadScene("FightScene");
        //gM.sceneM.LoadThisScene("FightScene");
    }

    public void OnClickProgrammer()
    {
        //gM.characterM.chosenCharacter = "Programmer";
        globalM.characterType = CharacterType.Programmmer;
        SceneManager.LoadScene("FightScene");
        //gM.sceneM.LoadThisScene("FightScene");
    }

    public void OnClickArtist()
    {
        //gM.characterM.chosenCharacter = "Artist";
        SceneManager.LoadScene("FightScene");
        //gM.sceneM.LoadThisScene("FightScene");

    }
}
