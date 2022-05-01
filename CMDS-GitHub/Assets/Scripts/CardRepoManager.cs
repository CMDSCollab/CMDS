using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardRepoManager : MonoBehaviour
{
    private GameObject cardPrefab;
    public List<GameObject> cardPrefabs;
    public GameObject parentLayout;

    public Dictionary<int, CardInfo> drawPile = new Dictionary<int, CardInfo>();
    public Dictionary<int, CardInfo> discardPile = new Dictionary<int, CardInfo>();
    public Dictionary<int, CardInfo> cardsOnPresent = new Dictionary<int, CardInfo>();

    public DeckManager deckM;

    public void InstantiateCard(Dictionary<int,CardInfo> pile)
    {
        List<int> residueKeys = new List<int>();
        foreach (int key in pile.Keys)
        {
            residueKeys.Add(key);
        }
        for (int i = 0; i < residueKeys.Count; i++)
        {
            if (pile[residueKeys[i]] is CardInfoDsgn)
            {
                cardPrefab = cardPrefabs[0];
            }
            else if (pile[residueKeys[i]] is CardInfoPro)
            {
                cardPrefab = cardPrefabs[1];
            }
            GameObject drawCard = Instantiate(cardPrefab);
            drawCard.gameObject.transform.SetParent(parentLayout.transform);
            drawCard.GetComponent<CardManager>().cardInfo = pile[residueKeys[i]];
            drawCard.GetComponent<CardManager>().deckIndexRecord = residueKeys[i];
        }
    }

    public void PresentDrawPile()
    {
        gameObject.SetActive(true);
        drawPile = new Dictionary<int, CardInfo>(deckM.cardInDeckCopy);
        InstantiateCard(drawPile);
    }

    public void PresentDiscardPile()
    {
        gameObject.SetActive(true);
        InstantiateCard(discardPile);
    }

    public void RemoveCardsFromLayout()
    {
        for (int i = 0; i < parentLayout.transform.childCount; i++)
        {
            Destroy(parentLayout.transform.GetChild(i).gameObject);
        }
    }
}
