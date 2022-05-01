using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BaseFunctionType
{Damage,Shield,Heal,ArtEnergy,DsgnEnergy,ProEnergy,ArtSlot,DsgnSlot,ProSlot,DrawCard}

public enum SpecialFunctionType 
{
    None,
    ArtIntentionChange,
    DsgnIntentionChange,
    ProIntentionChange,
    DrawSpecificCard,
    Exhausted
}

[System.Serializable]
public struct BaseFunction
{
    public BaseFunctionType functionType;
    public int value;
}

public abstract class CardInfo : ScriptableObject
{
    #region 卡牌基本信息
    [Header("Basic Info")]
    public string cardName;
    [TextArea]
    public string description;
    public List<BaseFunction> baseFunctions;
    public List<SpecialFunctionType> specialFunctions;
    #endregion

    //#region 攻击牌部分
    //[Header("Function - Attack")]
    //[Tooltip("攻击的次数（0则不具有攻击功能）")]
    //public int attackTimes;
    //[Tooltip("单次攻击造成的伤害")]
    //public int damage;
    //#endregion

    //#region 技能牌部分
    //[Header("Function - Skill")]
    //[Tooltip("给自己加盾 0则不具有加盾功能")]
    //public int shieldPoint;
    //[Tooltip("抽牌数量 0则不具有过牌功能")]
    //public int drawAmount;
    //[Tooltip("是否可以“发现”卡牌")]
    //public bool isFindCardInDrawPile;
    //#endregion

    //#region 能力牌部分
    ////[Header("Function - Power")]
    ////public int buff;
    ////public int applyDebuff;
    //#endregion

}
