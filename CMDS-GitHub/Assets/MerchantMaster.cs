using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantMaster : MonoBehaviour
{
    public GameMaster gM;
    public List<CardInfo> DesignerCardsForSell;

    public List<CardInfo> CurrentCardsForSell; //Private ���������ѡ���ְҵ���ڳ�ʼ��ʱ���и�ֵ��
    public List<Transform> slotsTrans;

    public GameObject cardForSellPrefab;//Private ���Ƶ�ģ�� 

    [Tooltip("���Ƽ۸񸡶���Χ")]
    public int priceRange;

    private void Awake()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    private void Start()
    {
        PrepareCardsForSellByCharacter();
        PrepareCardsForSellByRandom();
    }

    public void PrepareCardsForSellByRandom()
    {
        for(int i = 0; i < slotsTrans.Count; i++)
        {
            PrepareOneCardForSellByRandom(slotsTrans[i]);
        }
    }

    public void PrepareOneCardForSellByRandom(Transform slotTran)
    {
        GameObject newCard = Instantiate(cardForSellPrefab);
        newCard.GetComponent<CardManager>().isOnSell = true;
        newCard.GetComponent<CardManager>().cardInfo = PickOneCardByRandom();
        newCard.transform.SetParent(slotTran);
        newCard.transform.position = slotTran.position;
        slotTran.gameObject.GetComponentInChildren<Text>().text = MakePriceForOneCard(newCard).ToString();
    }


    public int MakePriceForOneCard(GameObject card)
    {
        int basePrice = card.GetComponent<CardManager>().cardInfo.merchantBaseValue;
        int change = Random.Range(-priceRange, priceRange);
        card.GetComponent<CardManager>().cardInfo.realValue = basePrice + change;

        return basePrice + change;
    }


    public CardInfo PickOneCardByRandom()
    {
        int index = Random.Range(0, CurrentCardsForSell.Count);
        return CurrentCardsForSell[index];
    }






    //�������ѡ���ְҵ���� CurrentCardsForSell���и�ֵ
    public void PrepareCardsForSellByCharacter()
    {
        CurrentCardsForSell = new List<CardInfo>();

        switch (gM.characterM.mainCharacterType.ToString())
        {
            case "Designer":
                CurrentCardsForSell = DesignerCardsForSell;
                cardForSellPrefab = gM.deckM.cardPrefabs[0];
                break;
        }
    }


    public void OnClickExitButton()
    {
        Destroy(this.gameObject);
    }
}
