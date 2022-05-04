using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public CardInfo cardInfo;
    public Text cardNameText;
    public Image cardTemplate;
    public GameMaster gM;

    public int handIndex;
    public int deckIndexRecord;
    public bool ifTouched = false;

    private void Start()
    {
        gM = FindObjectOfType<GameMaster>();
        UpdateUI();
    }

    public void OnMouseEnter()
    {
        transform.position += Vector3.up * 50;
        ifTouched = true;
    }

    public void OnMouseExit()
    {
        transform.position += Vector3.down * 50;
        ifTouched = false;
    }

    public void OnMouseClick()
    {
        if (gameObject.transform.parent.name != "CardLayout") //确认不是弃抽牌堆的卡再使用
        {
            CardFuntion();
            if (cardInfo.specialFunctions.Contains(SpecialFunctionType.Exhausted) == false)
            {
                DiscardHandCard();
            }
            gM.handM.OrganizeHand();
        }
        if (gameObject.transform.parent.name == "CardLayout" && gM.cardFunctionM.cardCanbeGetFromDrawPile == true)
        {
            gM.cardFunctionM.GetCardFromDrawPile(deckIndexRecord);
        }
        gM.cardFunctionM.FunctionEffectApply();
    }

    //丢弃手牌：在打出卡牌与回合结束时调用
    public void DiscardHandCard()
    {
        gM.cardRepoM.discardPile.Add(deckIndexRecord, cardInfo);
        gM.buttonM.SynchronizeCardsCountInPileButton("Discard"); //同步弃牌堆卡牌数量展示Text
        gM.handM.handCardList.RemoveAt(handIndex - 1);
        Destroy(this.gameObject);
    }

    public void UpdateUI()
    {
        cardNameText.text = cardInfo.cardName;
        cardTemplate.color = new Color(Random.Range(0.2f,0.7f), Random.Range(0.2f, 0.7f), Random.Range(0.2f, 0.7f));
        transform.Find("CardDescription").GetComponent<Text>().text = "Description:" +"\n" +cardInfo.description;
        for (int i = 0; i < cardInfo.baseFunctions.Count; i++)
        {
            switch (cardInfo.baseFunctions[i].functionType)
            {
                case BaseFunctionType.ArtEnergy:
                    transform.Find("ArtEnergy").Find("Value").GetComponent<Text>().text = "+" + cardInfo.baseFunctions[i].value.ToString();
                    break;
                case BaseFunctionType.DsgnEnergy:
                    transform.Find("DsgnEnergy").Find("Value").GetComponent<Text>().text = "+" + cardInfo.baseFunctions[i].value.ToString();
                    break;
                default:
                    break;
            }
        }
        if (cardInfo is CardInfoPro)
        {
            CardInfoPro cardPro = (CardInfoPro)cardInfo;
            transform.Find("Redundancy").GetComponent<Text>().text = "Redundancy:"+ cardPro.codeRedundancy.ToString();
            if (cardPro.cardType == CardTypePro.Debug)
            {
                transform.Find("Redundancy").GetComponent<Text>().text = transform.Find("Redundancy").GetComponent<Text>().text + "\n" + cardPro.debugType;
            }
        }
    }

    public void CardFuntion()
    {
        // 卡牌通用功能
        for (int i = 0; i < cardInfo.baseFunctions.Count; i++)
        {
            switch (cardInfo.baseFunctions[i].functionType)
            {
                case BaseFunctionType.Damage:
                    gM.enM.currentTarget.TakeDamage(cardInfo.baseFunctions[i].value);
                    break;
                case BaseFunctionType.Shield:
                    //gM.characterM.programmerPl.shieldPoint += cardInfo.baseFunctions[i].value;
                    gM.aiM.pro.shieldPoint += cardInfo.baseFunctions[i].value;
                    break;
                case BaseFunctionType.Heal:
                    break;
                case BaseFunctionType.ArtEnergy:
                    gM.aiM.artAI.energyPoint += cardInfo.baseFunctions[i].value;
                    gM.aiM.artAI.FillTheEnergyUI();
                    break;
                case BaseFunctionType.DsgnEnergy:
                    gM.aiM.desAI.energyPoint += cardInfo.baseFunctions[i].value;
                    gM.aiM.desAI.FillTheEnergyUI();
                    break;
                case BaseFunctionType.ProEnergy:
                    gM.aiM.proAI.energyPoint += cardInfo.baseFunctions[i].value;
                    gM.aiM.proAI.FillTheEnergyUI();
                    break;
                case BaseFunctionType.ArtSlot:
                    gM.aiM.artAI.AddTheEnergySlot();
                    break;
                case BaseFunctionType.DsgnSlot:
                    //gM.aiM.desAI.AddTheEnergySlot();
                    break;
                case BaseFunctionType.ProSlot:
                    gM.aiM.proAI.AddTheEnergySlot();
                    break;
                case BaseFunctionType.DrawCard:
                    gM.deckM.DrawCardFromDeckRandomly(cardInfo.baseFunctions[i].value);
                    break;
                default:
                    break;
            }
        }
        // 卡牌特殊功能
        for (int i = 0; i < cardInfo.specialFunctions.Count; i++)
        {
            switch (cardInfo.specialFunctions[i])
            {
                case SpecialFunctionType.None:
                    break;
                case SpecialFunctionType.ArtIntentionChange:
                    gM.aiM.artAI.ChangeIntention();
                    break;
                case SpecialFunctionType.DsgnIntentionChange:
                    gM.aiM.desAI.ChangeIntention();
                    break;
                case SpecialFunctionType.ProIntentionChange:
                    gM.aiM.proAI.ChangeIntention();
                    break;
                case SpecialFunctionType.DrawSpecificCard:
                    gM.cardFunctionM.FindCardInDrawPile();
                    break;
                case SpecialFunctionType.Exhausted:
                    gM.deckM.cardInDeck.Remove(handIndex);
                    gM.handM.handCardList.RemoveAt(handIndex - 1);
                    Destroy(this.gameObject);
                    break;
                default:
                    break;
            }
        }
  

        // 射击师 卡牌相关功能
        if (cardInfo is CardInfoDsgn)
        {
            if (gM.characterType == CharacterType.Designer)
            {
                return;
            }

            CardInfoDsgn cardDsgn = (CardInfoDsgn)cardInfo;

            if (cardDsgn.isArtEnergy)
            {
                gM.aiM.artAI.energyPoint += cardDsgn.eneryPoint;
                gM.aiM.artAI.FillTheEnergyUI();
            }
            if (cardDsgn.isProEnergy)
            {
                gM.aiM.proAI.energyPoint += cardDsgn.eneryPoint;
                gM.aiM.proAI.FillTheEnergyUI();
            }
            if (cardDsgn.isAddCubeSlot)
            {
                gM.aiM.proAI.AddTheEnergySlot();
            }
            if (cardDsgn.isAddCircleSlot)
            {
                gM.aiM.artAI.AddTheEnergySlot();
            }
            if (cardDsgn.isChangeProIntention)
            {
                gM.aiM.proAI.ChangeIntention();
            }
            if (cardDsgn.isChangeArtIntention)
            {
                gM.aiM.artAI.ChangeIntention();
            }
            if (cardDsgn.isAddChallenge)
            {
                //gM.characterM.designerPl.challengeInt += 1;
                gM.aiM.des.challengeInt += 1;
            }

        }

        // 程序猿 卡牌相关功能
        //if (cardInfo is CardInfoPro)
        if (cardInfo is CardInfoPro)
        {
            if (gM.characterType != CharacterType.Programmmer)
            {
                return;
            }
            CardInfoPro cardPro = (CardInfoPro)cardInfo;

            gM.aiM.pro.CheckCardDebug(cardPro.debugType);
            gM.aiM.pro.AddRedundantCode(cardPro.codeRedundancy);

            switch (cardPro.proSpecialFunction)
            {
                case SpecialFunctionPro.None:
                    break;
                case SpecialFunctionPro.DamageEqualsShield:
                    gM.enM.currentTarget.TakeDamage(gM.aiM.pro.shieldPoint);
                    break;
                case SpecialFunctionPro.DoubleShield:
                    gM.aiM.pro.shieldPoint += gM.aiM.pro.shieldPoint;
                    break;
                case SpecialFunctionPro.UseHandCardsGainShield:
                    gM.cardFunctionM.isUseCardGainShield = true;
                    break;
                case SpecialFunctionPro.DiscardAllHandCard:
                    if (gM.handM.handCardList.Count > 0)
                    {
                        for (int i = gM.handM.handCardList.Count; i > 0; i--)
                        {
                            if (gM.handM.handCardList[i - 1].GetComponent<CardManager>().handIndex != handIndex)
                            {
                                gM.handM.handCardList[i - 1].GetComponent<CardManager>().DiscardHandCard();
                                gM.aiM.pro.shieldPoint += 5;
                            }
                            handIndex = 1;
                        }
                    }
                    break;
                case SpecialFunctionPro.Vengeance:
                    gM.cardFunctionM.isVengeance = true;
                    break;
                case SpecialFunctionPro.ConsumeShieldDoubleDamage:
                    gM.enM.currentTarget.TakeDamage(gM.aiM.pro.shieldPoint * 2);
                    gM.aiM.pro.shieldPoint = 0;
                    break;
                default:
                    break;
            }
        }
    }
}
