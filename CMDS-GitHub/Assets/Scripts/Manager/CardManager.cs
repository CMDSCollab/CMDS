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
                case BaseFunctionType.ProEnergy:
                    transform.Find("ProEnergy").Find("Value").GetComponent<Text>().text = "+" + cardInfo.baseFunctions[i].value.ToString();
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
                    gM.cardSM.EnterCardState(gM.cardSM.attackState, cardInfo.baseFunctions[i].value);
                    //gM.enM.enemyTarget.TakeDamage(gM.buffM.CharacterAttack(cardInfo.baseFunctions[i].value));
                    break;
                case BaseFunctionType.Shield:
                    //gM.aiM.pro.shieldPoint += cardInfo.baseFunctions[i].value;
                    //gM.buffM.SetCharacterBuff(CharacterBuff.Defence, true, gM.aiM.pro.shieldPoint);
                    gM.buffM.SetBuff(CharacterBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.AddValue, cardInfo.baseFunctions[i].value, BuffSource.Character);
                    break;
                case BaseFunctionType.Heal:
                    break;
                case BaseFunctionType.ArtEnergy:
                    gM.aiM.artAI.EnergyValueChange(cardInfo.baseFunctions[i].value);
                    break;
                case BaseFunctionType.DsgnEnergy:
                    gM.aiM.desAI.EnergyValueChange(cardInfo.baseFunctions[i].value);
                    break;
                case BaseFunctionType.ProEnergy:
                    gM.aiM.proAI.EnergyValueChange(cardInfo.baseFunctions[i].value);
                    break;
                case BaseFunctionType.ArtSlot:
                    gM.aiM.artAI.energySlotAmount++;
                    gM.aiM.artAI.AddTheEnergySlot();
                    if (gM.characterM.mainCharacterType == CharacterType.Designer && gM.aiM.des.isSycn)
                    {
                        gM.aiM.artAI.EnergyValueChange(1);
                    }
                    break;
                case BaseFunctionType.DsgnSlot:
                    gM.aiM.desAI.energySlotAmount++;
                    gM.aiM.desAI.AddTheEnergySlot();
                    break;
                case BaseFunctionType.ProSlot:
                    gM.aiM.proAI.energySlotAmount++;
                    gM.aiM.proAI.AddTheEnergySlot();
                    if (gM.characterM.mainCharacterType == CharacterType.Designer && gM.aiM.des.isSycn)
                    {
                        gM.aiM.proAI.EnergyValueChange(1);
                    }
                    break;
                case BaseFunctionType.DrawCard:
                    gM.deckM.DrawCardFromDeckRandomly(cardInfo.baseFunctions[i].value);
                    if (gM.characterM.mainCharacterType == CharacterType.Designer && gM.aiM.des.isTeamWork) { gM.aiM.des.GoTeamWork(cardInfo.baseFunctions[i].value); }
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
            if (gM.characterM.mainCharacterType != CharacterType.Designer)
            {
                return;
            }

            CardInfoDsgn cardDsgn = (CardInfoDsgn)cardInfo;


            for (int i = 0; i < cardDsgn.desSpecialFunctions.Count; i++)
            {
                switch (cardDsgn.desSpecialFunctions[i].desFunctionType)
                {
                    case SpecialDesFunctionType.None:
                        break;
                    case SpecialDesFunctionType.ChangeChallenge:
                        gM.aiM.des.challengeLv += cardDsgn.desSpecialFunctions[i].value;
                        if (gM.aiM.des.challengeLv<0)
                        {
                            gM.aiM.des.challengeLv = 0;
                        }
                        gM.enM.enemyTarget.MainChaMCChange();
                        break;
                    case SpecialDesFunctionType.ChangeSkill:
                        gM.enM.enemyTarget.skillLv += cardDsgn.desSpecialFunctions[i].value;
                        if (gM.enM.enemyTarget.skillLv < 0)
                        {
                            gM.enM.enemyTarget.skillLv = 0;
                        }
                        gM.enM.enemyTarget.MainChaMCChange();
                        break;
                }
            }

            for (int i = 0; i < cardDsgn.desPassiveEffects.Count; i++)
            {
                switch (cardDsgn.desPassiveEffects[i].desPassiveEType)
                {
                    case SpecialPassiveEffectType.None:
                        break;
                    case SpecialPassiveEffectType.IsTeamWork:
                        gM.aiM.des.isTeamWork = true;
                        gM.buffM.SetBuff(CharacterBuff.IsTeamWork, BuffTimeType.Temporary, 1, BuffValueType.SetValue, 1,BuffSource.Character);
                        break;
                    case SpecialPassiveEffectType.IsSycn:
                        gM.aiM.des.isSycn = true;
                        gM.buffM.SetBuff(CharacterBuff.IsSycn, BuffTimeType.Temporary, 1, BuffValueType.SetValue, 1, BuffSource.Character);
                        break;
                }

            }
        }

        // 程序猿 卡牌相关功能
        //if (cardInfo is CardInfoPro)
        if (cardInfo is CardInfoPro)
        {
            if (gM.characterM.mainCharacterType != CharacterType.Programmmer)
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
                    gM.enM.enemyTarget.TakeDamage(gM.buffM.CharacterAttack(gM.aiM.pro.shieldPoint));
                    break;
                case SpecialFunctionPro.DoubleShield:
                    if (gM.buffM.FindBuff(CharacterBuff.Defence)!=null)
                    {
                        gM.buffM.SetBuff(CharacterBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.AddValue, gM.buffM.FindBuff(CharacterBuff.Defence).value, BuffSource.Character);
                    }
                    //gM.aiM.pro.shieldPoint += gM.aiM.pro.shieldPoint;
                    //gM.buffM.SetCharacterBuff(CharacterBuff.Defence, true, gM.aiM.pro.shieldPoint);
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
                                gM.buffM.SetBuff(CharacterBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.AddValue, 5, BuffSource.Character);
                                //gM.aiM.pro.shieldPoint += 5;
                                //gM.buffM.SetCharacterBuff(CharacterBuff.Defence, true, gM.aiM.pro.shieldPoint);
                            }
                        }
                        gM.handM.OrganizeHand();
                    }
                    break;
                case SpecialFunctionPro.Vengeance:
                    //gM.cardFunctionM.isVengeance = true;
                    //gM.buffM.SetCharacterBuff(CharacterBuff.Vengeance, true, 4);
                    gM.buffM.SetBuff(CharacterBuff.Vengeance, BuffTimeType.Temporary, 1, BuffValueType.SetValue, 4,BuffSource.Character);
                    break;
                case SpecialFunctionPro.ConsumeShieldDoubleDamage:
                    //gM.enM.enemyTarget.TakeDamage(gM.buffM.CharacterAttack(gM.aiM.pro.shieldPoint * 2));
                    //gM.aiM.pro.shieldPoint = 0;
                    //gM.buffM.SetCharacterBuff(CharacterBuff.Defence, true, gM.aiM.pro.shieldPoint);
                    gM.enM.enemyTarget.TakeDamage(gM.buffM.CharacterAttack(gM.buffM.FindBuff(CharacterBuff.Defence).value * 2));
                    gM.buffM.SetBuff(CharacterBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.AddValue, -gM.buffM.FindBuff(CharacterBuff.Defence).value, BuffSource.Character);
                    break;
                default:
                    break;
            }
        }
    }
}
