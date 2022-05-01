using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    //public List<CardInfo> cardInDeck;
    //public List<CardInfo> cardInDeckCopy;
    public List<CardInfo> designerBaseCard;
    public List<CardInfo> programmerBaseCard;
    public Dictionary<int,CardInfo> cardInDeck = new Dictionary<int, CardInfo>(); //��Ϊ������ܻ���ֿ��ƿ���ǿ���������ͬ��һ�ſ����ܳ�������һ��������Ч����һ���������������Ҫ��������
    public Dictionary<int,CardInfo> cardInDeckCopy = new Dictionary<int, CardInfo>(); //����Ϊscriptable obj����Ŀ�ļ�������ֻ����һ��ʵ����������Ҫ�ڳ����ڽ�һ������ÿ��ʵ����������
    private GameObject cardPrefab;
    public List<GameObject> cardPrefabs;

    public GameMaster gM;
    public int initialCardAmount;
    public int drawCardAmount;

    private void Awake()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    public void PrepareDeckAndHand()
    {
        InitializeDicCardInDeck();
        GetNewCopyDeck();
        DrawCardFromDeckRandomly(initialCardAmount);
    }

    public void InitializeDicCardInDeck()
    {
        if (gM.characterM.chosenCharacter == "Designer")
        {
            for (int i = 0; i < designerBaseCard.Count; i++)
            {
                cardInDeck[i] = designerBaseCard[i];
            }
        }
        else if (gM.characterM.chosenCharacter == "Programmer")
        {
            cardPrefab = cardPrefabs[1];
            for (int i = 0; i < programmerBaseCard.Count; i++)
            {
                cardInDeck[i] = programmerBaseCard[i];
            }
        }
    }

    public void GetNewCopyDeck()
    {
        cardInDeckCopy = new Dictionary<int, CardInfo>(cardInDeck);
    }

    public void DrawCardFromDeckRandomly (int amount)
    {
        if(amount < cardInDeckCopy.Count)
        {
            for (int i = 0; i < amount; i++)
            {
                DrawRandomSingleCard();
            }
        }
        else
        {
            int extra = amount - cardInDeckCopy.Count;
            for(int i  = cardInDeckCopy.Count; i > 0; i = cardInDeckCopy.Count)
            {
                DrawRandomSingleCard();
            }

            ShuffleDiscardPileToDeck();

            for(int i = 0; i < extra; i++)
            {
                DrawRandomSingleCard();
            }

        }
    }

    public void DrawRandomSingleCard()
    {
        //int index = Random.Range(0, cardInDeckCopy.Count);
        List<int> residueKeys = new List<int>(); //��Ϊ����dic�����Բ���ֱ��random��������6��Ӧ��ֵ��ȡ����Ȼ����0-8���ֳ�ȡ��6�ͻ�ȡ������Ӧֵ
        foreach (int key in cardInDeckCopy.Keys)
        {
            residueKeys.Add(key);
        }
        int residueKeysIndex = Random.Range(0, residueKeys.Count);

        GameObject drawCard = Instantiate(cardPrefab);
        drawCard.gameObject.transform.SetParent(gM.handM.transform);
        drawCard.GetComponent<CardManager>().cardInfo = cardInDeckCopy[residueKeys[residueKeysIndex]];

        drawCard.GetComponent<CardManager>().handIndex = gM.handM.handCardList.Count + 1;
        drawCard.GetComponent<CardManager>().deckIndexRecord = residueKeys[residueKeysIndex];
        gM.handM.handCardList.Add(drawCard.gameObject);
        gM.handM.OrganizeHand();
        cardInDeckCopy.Remove(residueKeys[residueKeysIndex]);

        //cardInDeckCopy.RemoveAt(index);
        gM.buttonM.SynchronizeCardsCountInPileButton("Draw"); //ͬ�����ƶѿ�������չʾText
    }

    public void ShuffleDiscardPileToDeck()
    {
        Debug.Log("TestXXXX");

        cardInDeckCopy = new Dictionary<int, CardInfo>(gM.cardRepoM.discardPile);
        gM.cardRepoM.discardPile.Clear();
        gM.buttonM.SynchronizeCardsCountInPileButton("Discard"); //ͬ�����ƶѿ�������չʾText
        gM.buttonM.SynchronizeCardsCountInPileButton("Draw");
    }

    public void DrawSpecificSingleCard(int deckIndexRecord, Dictionary<int,CardInfo> targetCardList) //���ض�CardList��ȡ�ض���
    {
        GameObject drawCard = Instantiate(cardPrefab);
        drawCard.gameObject.transform.SetParent(gM.handM.transform);
        drawCard.GetComponent<CardManager>().cardInfo = cardInDeckCopy[deckIndexRecord];

        drawCard.GetComponent<CardManager>().handIndex = gM.handM.handCardList.Count + 1;
        drawCard.GetComponent<CardManager>().deckIndexRecord = deckIndexRecord;
        gM.handM.handCardList.Add(drawCard.gameObject);
        gM.handM.OrganizeHand();
        cardInDeckCopy.Remove(deckIndexRecord);
        //cardInDeckCopy.RemoveAt(index);

        gM.buttonM.SynchronizeCardsCountInPileButton("Draw"); //ͬ�����ƶѿ�������չʾText
    }
}
