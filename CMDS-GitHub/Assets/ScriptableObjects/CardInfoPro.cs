using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardTypePro {Normal, Debug}
public enum SpecialFunctionPro
{
    None,
    DamageEqualsShield,
    DoubleShield,
    UseHandCardsGainShield,
    DiscardAllHandCard,
    Vengeance,
    ConsumeShieldDoubleDamage
}
public enum DebugType
{
    None,
    Repass,
    TryParse,
    GarbageCollect,
    NewThread,
    DefaultValue,
    GetSet
}

[CreateAssetMenu(fileName = "New Programmer Card", menuName = "Scriptable Object/New Programmer Card")]
public class CardInfoPro : CardInfo
{
    [Header("Programmer Area")]
    public CardTypePro cardType;
    public DebugType debugType;
    [Tooltip("卡牌对冗余代码的影响（例：3表示增加3点，-2表示消除2点）")]
    public int codeRedundancy;
    public SpecialFunctionPro proSpecialFunction;
}


