using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct ExpandArtistCards {public string cardname; public CardInfoArt card;}

public class DeckManager : MonoBehaviour
{
    public List<CardInfo> designerBaseCard;
    public List<CardInfo> programmerBaseCard;
    public List<CardInfo> artistBaseCard;
    public List<ExpandArtistCards> artistExpandCard;
    public Dictionary<string,CardInfoArt> artistExpandCardDic;
    public Dictionary<int,CardInfo> cardInDeck = new Dictionary<int, CardInfo>(); //因为后面可能会出现卡牌可以强化的情况，同样一张卡可能出现名字一样，但是效果不一样的情况，所以需要做出区分
    public Dictionary<int,CardInfo> cardInDeckCopy = new Dictionary<int, CardInfo>(); //且因为scriptable obj是项目文件，所以只能有一个实例，所以需要在程序内进一步对于每个实例进行区分
    private GameObject cardPrefab;
    public List<GameObject> cardPrefabs;

    public GameMaster gM;
    public int initialCardAmount;
    public int drawCardAmount;

    private void Awake()
    {
        gM = FindObjectOfType<GameMaster>();
        MakeArtistExpandCardDic();
    }

    public void MakeArtistExpandCardDic()
    {
        artistExpandCardDic = new Dictionary<string, CardInfoArt>();

        foreach(ExpandArtistCards expandCard in artistExpandCard)
        {
            artistExpandCardDic.Add(expandCard.cardname, expandCard.card);
        }
    }

    public void PrepareDeckAndHand()
    {
        InitializeDicCardInDeck();
        GetNewCopyDeck();
        DrawCardFromDeckRandomly(initialCardAmount);
    }

    public void InitializeDicCardInDeck()
    {
        if (gM.characterM.mainCharacterType == CharacterType.Designer)
        {
            cardPrefab = cardPrefabs[0];

            for (int i = 0; i < designerBaseCard.Count; i++)
            {
                cardInDeck[i] = designerBaseCard[i];
            }
        }
        else if (gM.characterM.mainCharacterType == CharacterType.Programmmer)
        {
            cardPrefab = cardPrefabs[1];
            for (int i = 0; i < programmerBaseCard.Count; i++)
            {
                cardInDeck[i] = programmerBaseCard[i];
            }
        }
        else if(gM.characterM.mainCharacterType == CharacterType.Artist)
        {
            cardPrefab = cardPrefabs[2];
            for (int i = 0; i < artistBaseCard.Count; i++)
            {
                cardInDeck[i] = artistBaseCard[i];
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
        List<int> residueKeys = new List<int>(); //因为改用dic，所以不能直接random，举例当6对应的值被取出，然后在0-8中又抽取到6就获取不到对应值
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

        gM.buttonM.SynchronizeCardsCountInPileButton("Draw"); //同步抽牌堆卡牌数量展示Text
    }

    public void ShuffleDiscardPileToDeck()
    {
        cardInDeckCopy = new Dictionary<int, CardInfo>(gM.cardRepoM.discardPile);
        gM.cardRepoM.discardPile.Clear();
        gM.buttonM.SynchronizeCardsCountInPileButton("Discard"); //同步弃牌堆卡牌数量展示Text
        gM.buttonM.SynchronizeCardsCountInPileButton("Draw");
    }

    public void DrawSpecificSingleCard(int deckIndexRecord, Dictionary<int,CardInfo> targetCardList) //从特定CardList抽取特定卡
    {
        GameObject drawCard = Instantiate(cardPrefab);
        drawCard.gameObject.transform.SetParent(gM.handM.transform);
        drawCard.GetComponent<CardManager>().cardInfo = cardInDeckCopy[deckIndexRecord];

        drawCard.GetComponent<CardManager>().handIndex = gM.handM.handCardList.Count + 1;
        drawCard.GetComponent<CardManager>().deckIndexRecord = deckIndexRecord;
        gM.handM.handCardList.Add(drawCard.gameObject);
        gM.handM.OrganizeHand();
        cardInDeckCopy.Remove(deckIndexRecord);

        gM.buttonM.SynchronizeCardsCountInPileButton("Draw"); //同步抽牌堆卡牌数量展示Text
    }

    public void DrawSpecificSingleArtistExpandCard(string cardName)
    {
        GameObject drawCard = Instantiate(cardPrefab);
        drawCard.gameObject.transform.SetParent(gM.handM.transform);
        drawCard.GetComponent<CardManager>().cardInfo = artistExpandCardDic[cardName];

        drawCard.GetComponent<CardManager>().handIndex = gM.handM.handCardList.Count + 1;
        //drawCard.GetComponent<CardManager>().deckIndexRecord = index;
        gM.handM.handCardList.Add(drawCard.gameObject);
        gM.handM.OrganizeHand();

    }
}
