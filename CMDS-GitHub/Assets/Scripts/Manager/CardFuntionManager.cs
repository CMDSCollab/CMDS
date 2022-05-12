using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFuntionManager : MonoBehaviour
{
    public GameMaster gM;
    public bool cardCanbeGetFromDrawPile = false;
    public bool isUseCardGainShield = false;
    public bool isVengeance = false;

    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    public void FindCardInDrawPile()
    {
        gM.cardRepoM.PresentDrawPile();
        cardCanbeGetFromDrawPile = true;
    }

    public void GetCardFromDrawPile(int deckIndexRecord)
    {
        gM.deckM.DrawSpecificSingleCard(deckIndexRecord, gM.deckM.cardInDeckCopy);
        gM.cardRepoM.RemoveCardsFromLayout();
        gM.cardRepoM.gameObject.SetActive(false);
        cardCanbeGetFromDrawPile = false;
    }

    public void FunctionBoolValueReset()
    {
        isUseCardGainShield = false;
        isVengeance = false;
    }

    public void FunctionEffectApply()
    {
        if (isUseCardGainShield == true)
        {
            gM.buffM.SetBuff(CharacterBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.AddValue, 3, BuffSource.Character);
            //gM.aiM.pro.shieldPoint += 3;
            //gM.buffM.SetCharacterBuff(CharacterBuff.Defence, true, gM.aiM.pro.shieldPoint);
        }
    }
}
