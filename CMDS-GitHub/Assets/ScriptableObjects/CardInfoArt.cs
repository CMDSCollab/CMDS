using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ArtStyle 
{
    None,
    Pixel,
    LowPoly,
    ACG,
    LoveCraft,
    LaiZi
}


public enum SpecialArtFunctionType
{
    None,
    AddCardToHand,
    TrueDamage,
    GetIncome,
    CostConsistency,
    StyleEffect//Á¬»÷½áËã
}

public enum SpecialArtPassiveEffectType
{
    None,
    ImmuneConsistency
}


[System.Serializable]

public struct specialArtFunction
{
    public SpecialArtFunctionType artFunctionType;
    public int value;
}

[System.Serializable]

public struct specialArtPassiveEffect
{
    public SpecialArtPassiveEffectType artPassiveEType;


}

[CreateAssetMenu(fileName = "New Artist Card", menuName = "Scriptable Object/New Artist Card")]

public class CardInfoArt : CardInfo
{

    [Header("Artist Area")]

    public ArtStyle style;
    public List<specialArtFunction> artSpecialFunctions;
    public List<specialArtPassiveEffect> artPassiveEffects;
}
